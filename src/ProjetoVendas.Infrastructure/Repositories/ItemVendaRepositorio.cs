using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Infrastructure.Data;

namespace ProjetoVendas.Infrastructure.Repositories
{
    public class ItemVendaRepositorio : RepositorioBase<ItemVenda>, IItemVendaRepositorio
    {
        public ItemVendaRepositorio(VendasContext context) : base(context)
        {
        }
    }
}
