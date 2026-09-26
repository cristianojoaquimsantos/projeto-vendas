using Microsoft.AspNetCore.Mvc;
using ProjetoVendas.Application.DTOs;
using ProjetoVendas.Application.Services.Interface;

namespace ProjetoVendas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendasController : ControllerBase
    {
        private readonly IVendaServico _vendaServico;

        public VendasController(IVendaServico vendaServico)
        {
            _vendaServico = vendaServico;
        }

        [HttpPost]
        public async Task<ActionResult<VendaDto>> Criar(CriarVendaDto dto)
        {
            try
            {
                var venda = await _vendaServico.CriarAsync(dto);
                return CreatedAtAction(nameof(ObterPorId), new { id = venda.Id }, venda);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao criar a venda.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VendaDto>> ObterPorId(int id)
        {
            var venda = await _vendaServico.ObterPorIdAsync(id);
            if (venda == null)
                return NotFound();

            return Ok(venda);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendaDto>>> ObterTodos()
        {
            var vendas = await _vendaServico.ObterTodosAsync();
            return Ok(vendas);
        }

        [HttpGet("consultar")]
        public async Task<ActionResult<IEnumerable<VendaDto>>> Consultar([FromQuery] string termo)
        {
            var vendas = await _vendaServico.ConsultarAsync(termo);
            return Ok(vendas);
        }
    }
}
