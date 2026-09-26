using Microsoft.AspNetCore.Mvc;
using ProjetoVendas.Application.DTOs;
using ProjetoVendas.Application.Services.Interface;

namespace ProjetoVendas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendedoresController : ControllerBase
    {
        private readonly IVendedorServico _vendedorServico;

        public VendedoresController(IVendedorServico vendedorServico)
        {
            _vendedorServico = vendedorServico;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendedorDto>>> ObterTodos()
        {
            var vendedores = await _vendedorServico.ObterTodosAsync();
            return Ok(vendedores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VendedorDto>> ObterPorId(int id)
        {
            var vendedor = await _vendedorServico.ObterPorIdAsync(id);
            if (vendedor == null)
                return NotFound();

            return Ok(vendedor);
        }

        [HttpPost]
        public async Task<ActionResult<VendedorDto>> Criar(VendedorDto dto)
        {
            var vendedorCriado = await _vendedorServico.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = vendedorCriado.Id }, vendedorCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VendedorDto>> Atualizar(int id, VendedorDto dto)
        {
            try
            {
                var vendedorAtualizado = await _vendedorServico.AtualizarAsync(id, dto);
                return Ok(vendedorAtualizado);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            await _vendedorServico.RemoverAsync(id);
            return NoContent();
        }

        [HttpGet("buscar/{nome}")]
        public async Task<ActionResult<IEnumerable<VendedorDto>>> Buscar(string nome)
        {
            var vendedores = await _vendedorServico.ConsultarAsync(nome);
            return Ok(vendedores);
        }
    }
}
