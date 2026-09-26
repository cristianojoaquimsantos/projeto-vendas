using Microsoft.AspNetCore.Mvc;
using ProjetoVendas.Application.DTOs;
using ProjetoVendas.Application.Services.Interface;

namespace ProjetoVendas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoriasController : ControllerBase
    {
        private readonly ISubCategoriaServico _subCategoriaServico;

        public SubCategoriasController(ISubCategoriaServico subCategoriaServico)
        {
            _subCategoriaServico = subCategoriaServico;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoriaDto>>> ObterTodos()
        {
            var subCategorias = await _subCategoriaServico.ObterTodosAsync();
            return Ok(subCategorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoriaDto>> ObterPorId(int id)
        {
            var subCategoria = await _subCategoriaServico.ObterPorIdAsync(id);
            if (subCategoria == null)
                return NotFound();

            return Ok(subCategoria);
        }

        [HttpPost]
        public async Task<ActionResult<SubCategoriaDto>> Criar(SubCategoriaDto dto)
        {
            var subCategoriaCriada = await _subCategoriaServico.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = subCategoriaCriada.Id }, subCategoriaCriada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SubCategoriaDto>> Atualizar(int id, SubCategoriaDto dto)
        {
            try
            {
                var subCategoriaAtualizada = await _subCategoriaServico.AtualizarAsync(id, dto);
                return Ok(subCategoriaAtualizada);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            await _subCategoriaServico.RemoverAsync(id);
            return NoContent();
        }

        [HttpGet("buscar/{nome}")]
        public async Task<ActionResult<IEnumerable<SubCategoriaDto>>> Buscar(string nome)
        {
            var subCategorias = await _subCategoriaServico.ConsultarAsync(nome);
            return Ok(subCategorias);
        }
    }
}
