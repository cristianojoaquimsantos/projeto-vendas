using Microsoft.EntityFrameworkCore;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Infrastructure.Data;
using System.Linq.Expressions;

namespace ProjetoVendas.Infrastructure.Repositories
{
    public class RepositorioBase<T> : IRepositorio<T> where T : class
    {
        protected readonly VendasContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositorioBase(VendasContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> ObterPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> ConsultarAsync(Expression<Func<T, bool>> predicado)
        {
            return await _dbSet.Where(predicado).ToListAsync();
        }

        public virtual async Task AdicionarAsync(T entidade)
        {
            await _dbSet.AddAsync(entidade);
        }

        public virtual Task AtualizarAsync(T entidade)
        {
            _dbSet.Update(entidade);
            return Task.CompletedTask;
        }

        public virtual Task RemoverAsync(T entidade)
        {
            _dbSet.Remove(entidade);
            return Task.CompletedTask;
        }

        public virtual async Task<int> ContarAsync()
        {
            return await _dbSet.CountAsync();
        }

        public virtual async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
