using Microsoft.AspNetCore.Mvc;
using ProjetoVendas.Application.DTOs;
using ProjetoVendas.Application.Services.Interface;

namespace ProjetoVendas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaServico _categoriaServico;

        public CategoriasController(ICategoriaServico categoriaServico)
        {
            _categoriaServico = categoriaServico;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> ObterTodos()
        {
            var categorias = await _categoriaServico.ObterTodosAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDto>> ObterPorId(int id)
        {
            var categoria = await _categoriaServico.ObterPorIdAsync(id);
            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDto>> Criar(CategoriaDto dto)
        {
            var categoriaCriada = await _categoriaServico.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = categoriaCriada.Id }, categoriaCriada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaDto>> Atualizar(int id, CategoriaDto dto)
        {
            try
            {
                var categoriaAtualizada = await _categoriaServico.AtualizarAsync(id, dto);
                return Ok(categoriaAtualizada);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            await _categoriaServico.RemoverAsync(id);
            return NoContent();
        }

        [HttpGet("buscar/{nome}")]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> Buscar(string nome)
        {
            var categorias = await _categoriaServico.ConsultarAsync(nome);
            return Ok(categorias);
        }
    }
}
