using Microsoft.AspNetCore.Mvc;
using ProjetoVendas.Application.DTOs;
using ProjetoVendas.Application.Services.Interface;

namespace ProjetoVendas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LojasController : ControllerBase
    {
        private readonly ILojaServico _lojaServico;

        public LojasController(ILojaServico lojaServico)
        {
            _lojaServico = lojaServico;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LojaDto>>> ObterTodos()
        {
            var lojas = await _lojaServico.ObterTodosAsync();
            return Ok(lojas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LojaDto>> ObterPorId(int id)
        {
            var loja = await _lojaServico.ObterPorIdAsync(id);
            if (loja == null)
                return NotFound();

            return Ok(loja);
        }

        [HttpPost]
        public async Task<ActionResult<LojaDto>> Criar(LojaDto dto)
        {
            var lojaCriada = await _lojaServico.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = lojaCriada.Id }, lojaCriada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LojaDto>> Atualizar(int id, LojaDto dto)
        {
            try
            {
                var lojaAtualizada = await _lojaServico.AtualizarAsync(id, dto);
                return Ok(lojaAtualizada);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            await _lojaServico.RemoverAsync(id);
            return NoContent();
        }

        [HttpGet("buscar/{nome}")]
        public async Task<ActionResult<IEnumerable<LojaDto>>> Buscar(string nome)
        {
            var lojas = await _lojaServico.ConsultarAsync(nome);
            return Ok(lojas);
        }
    }
}
