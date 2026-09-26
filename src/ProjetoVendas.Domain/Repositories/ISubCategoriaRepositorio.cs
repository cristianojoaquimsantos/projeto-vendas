using ProjetoVendas.Domain.Entities;

namespace ProjetoVendas.Domain.Repositories
{
    public interface ISubCategoriaRepositorio : IRepositorio<SubCategoria>
    {
        Task<SubCategoria?> ObterComProdutosAsync(int id);
    }
}
