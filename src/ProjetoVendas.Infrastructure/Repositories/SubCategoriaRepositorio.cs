using Microsoft.EntityFrameworkCore;
using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Infrastructure.Data;

namespace ProjetoVendas.Infrastructure.Repositories
{
    public class SubCategoriaRepositorio : RepositorioBase<SubCategoria>, ISubCategoriaRepositorio
    {
        public SubCategoriaRepositorio(VendasContext context) : base(context)
        {
        }

        public async Task<SubCategoria?> ObterComProdutosAsync(int id)
        {
            return await _context.SubCategorias
                .Include(sc => sc.Produtos)
                .FirstOrDefaultAsync(sc => sc.Id == id);
        }
    }
}
