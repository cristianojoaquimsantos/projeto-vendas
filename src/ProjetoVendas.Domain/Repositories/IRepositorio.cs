using System.Linq.Expressions;

namespace ProjetoVendas.Domain.Repositories
{
    public interface IRepositorio<T> where T : class
    {
        Task<T?> ObterPorIdAsync(int id);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<IEnumerable<T>> ConsultarAsync(Expression<Func<T, bool>> predicado);
        Task AdicionarAsync(T entidade);
        Task AtualizarAsync(T entidade);
        Task RemoverAsync(T entidade);
        Task<int> ContarAsync();
        Task SalvarAlteracoesAsync();
    }
}
