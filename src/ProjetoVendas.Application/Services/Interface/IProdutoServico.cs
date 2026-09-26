using ProjetoVendas.Application.DTOs;

namespace ProjetoVendas.Application.Services.Interface
{
    public interface IProdutoServico
    {
        Task<ProdutoDto> CriarAsync(ProdutoDto dto);
        Task<ProdutoDto?> ObterPorIdAsync(int id);
        Task<IEnumerable<ProdutoDto>> ObterTodosAsync();
        Task<ProdutoDto> AtualizarAsync(int id, ProdutoDto dto);
        Task RemoverAsync(int id);
        Task<IEnumerable<ProdutoDto>> ConsultarAsync(string nome);
    }
}
