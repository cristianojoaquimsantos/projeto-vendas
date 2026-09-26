using Microsoft.EntityFrameworkCore;
using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Infrastructure.Data;

namespace ProjetoVendas.Infrastructure.Repositories
{
    public class CategoriaRepositorio : RepositorioBase<Categoria>, ICategoriaRepositorio
    {
        public CategoriaRepositorio(VendasContext context) : base(context)
        {
        }

        public async Task<Categoria?> ObterComSubCategoriasAsync(int id)
        {
            return await _context.Categorias
                .Include(c => c.SubCategorias)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
