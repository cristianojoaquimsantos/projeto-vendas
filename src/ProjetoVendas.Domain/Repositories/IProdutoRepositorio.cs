using ProjetoVendas.Domain.Entities;

namespace ProjetoVendas.Domain.Repositories
{
    public interface IProdutoRepositorio : IRepositorio<Produto>
    {
        Task<Produto?> ObterComCategoriasAsync(int id);
        Task<bool> VerificarEstoqueAsync(int produtoId, int quantidade);
        Task ReduzirEstoqueAsync(int produtoId, int quantidade);
    }
}
