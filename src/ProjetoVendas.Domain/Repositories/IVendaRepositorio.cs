using ProjetoVendas.Domain.Entities;

namespace ProjetoVendas.Domain.Repositories
{
    public interface IVendaRepositorio : IRepositorio<Venda>
    {
        Task<Venda?> ObterComDetalhesAsync(int id);
    }
}
