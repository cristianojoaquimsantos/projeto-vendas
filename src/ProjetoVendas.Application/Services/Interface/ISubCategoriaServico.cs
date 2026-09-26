using ProjetoVendas.Application.DTOs;

namespace ProjetoVendas.Application.Services.Interface
{
    public interface ISubCategoriaServico
    {
        Task<SubCategoriaDto> CriarAsync(SubCategoriaDto dto);
        Task<SubCategoriaDto?> ObterPorIdAsync(int id);
        Task<IEnumerable<SubCategoriaDto>> ObterTodosAsync();
        Task<SubCategoriaDto> AtualizarAsync(int id, SubCategoriaDto dto);
        Task RemoverAsync(int id);
        Task<IEnumerable<SubCategoriaDto>> ConsultarAsync(string nome);
    }
}
