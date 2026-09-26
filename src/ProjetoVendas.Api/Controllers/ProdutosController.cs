using Microsoft.AspNetCore.Mvc;
using ProjetoVendas.Application.DTOs;
using ProjetoVendas.Application.Services.Interface;

namespace ProjetoVendas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoServico _produtoServico;

        public ProdutosController(IProdutoServico produtoServico)
        {
            _produtoServico = produtoServico;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> ObterTodos()
        {
            var produtos = await _produtoServico.ObterTodosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDto>> ObterPorId(int id)
        {
            var produto = await _produtoServico.ObterPorIdAsync(id);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDto>> Criar(ProdutoDto dto)
        {
            var produtoCriado = await _produtoServico.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = produtoCriado.Id }, produtoCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoDto>> Atualizar(int id, ProdutoDto dto)
        {
            try
            {
                var produtoAtualizado = await _produtoServico.AtualizarAsync(id, dto);
                return Ok(produtoAtualizado);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            await _produtoServico.RemoverAsync(id);
            return NoContent();
        }

        [HttpGet("buscar/{nome}")]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> Buscar(string nome)
        {
            var produtos = await _produtoServico.ConsultarAsync(nome);
            return Ok(produtos);
        }
    }
}
