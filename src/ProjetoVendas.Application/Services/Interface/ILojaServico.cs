using ProjetoVendas.Application.DTOs;

namespace ProjetoVendas.Application.Services.Interface
{
    public interface ILojaServico
    {
        Task<LojaDto> CriarAsync(LojaDto dto);
        Task<LojaDto?> ObterPorIdAsync(int id);
        Task<IEnumerable<LojaDto>> ObterTodosAsync();
        Task<LojaDto> AtualizarAsync(int id, LojaDto dto);
        Task RemoverAsync(int id);
        Task<IEnumerable<LojaDto>> ConsultarAsync(string nome);
    }
}
