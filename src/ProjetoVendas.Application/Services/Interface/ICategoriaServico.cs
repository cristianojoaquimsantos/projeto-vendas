using ProjetoVendas.Application.DTOs;

namespace ProjetoVendas.Application.Services.Interface
{
    public interface ICategoriaServico
    {
        Task<CategoriaDto> CriarAsync(CategoriaDto dto);
        Task<CategoriaDto?> ObterPorIdAsync(int id);
        Task<IEnumerable<CategoriaDto>> ObterTodosAsync();
        Task<CategoriaDto> AtualizarAsync(int id, CategoriaDto dto);
        Task RemoverAsync(int id);
        Task<IEnumerable<CategoriaDto>> ConsultarAsync(string nome);
    }
}
