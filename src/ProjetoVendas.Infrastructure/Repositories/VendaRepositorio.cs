using Microsoft.EntityFrameworkCore;
using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Infrastructure.Data;

namespace ProjetoVendas.Infrastructure.Repositories
{
    public class VendaRepositorio : RepositorioBase<Venda>, IVendaRepositorio
    {
        public VendaRepositorio(VendasContext context) : base(context)
        {
        }

        public async Task<Venda?> ObterComDetalhesAsync(int id)
        {
            return await _context.Vendas
                .Include(v => v.Loja)
                .Include(v => v.Vendedor)
                .Include(v => v.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(v => v.Id == id);
        }
    }
}
