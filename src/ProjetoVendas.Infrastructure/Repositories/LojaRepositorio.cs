using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Infrastructure.Data;

namespace ProjetoVendas.Infrastructure.Repositories
{
    public class LojaRepositorio : RepositorioBase<Loja>, ILojaRepositorio
    {
        public LojaRepositorio(VendasContext context) : base(context)
        {
        }
    }
}
