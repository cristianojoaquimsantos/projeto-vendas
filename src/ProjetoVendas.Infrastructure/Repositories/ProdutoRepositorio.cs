using Microsoft.EntityFrameworkCore;
using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Infrastructure.Data;

namespace ProjetoVendas.Infrastructure.Repositories
{
    public class ProdutoRepositorio : RepositorioBase<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(VendasContext context) : base(context)
        {
        }

        public async Task<Produto?> ObterComCategoriasAsync(int id)
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.SubCategoria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> VerificarEstoqueAsync(int produtoId, int quantidade)
        {
            var produto = await _dbSet.FindAsync(produtoId);
            return produto != null && produto.ValidarEstoque(quantidade);
        }

        public async Task ReduzirEstoqueAsync(int produtoId, int quantidade)
        {
            var produto = await _dbSet.FindAsync(produtoId);
            if (produto != null)
            {
                produto.ReduzirEstoque(quantidade);
            }
        }
    }
}
