using ProjetoVendas.Domain.Entities;

namespace ProjetoVendas.Domain.Repositories
{
    public interface ICategoriaRepositorio : IRepositorio<Categoria>
    {
        Task<Categoria?> ObterComSubCategoriasAsync(int id);
    }
}
