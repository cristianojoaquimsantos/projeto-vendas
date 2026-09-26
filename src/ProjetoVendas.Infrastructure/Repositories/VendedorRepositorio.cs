using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Infrastructure.Data;

namespace ProjetoVendas.Infrastructure.Repositories
{
    public class VendedorRepositorio : RepositorioBase<Vendedor>, IVendedorRepositorio
    {
        public VendedorRepositorio(VendasContext context) : base(context)
        {
        }
    }
}
