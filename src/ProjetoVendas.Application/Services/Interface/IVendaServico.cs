using ProjetoVendas.Application.DTOs;

namespace ProjetoVendas.Application.Services.Interface
{
    public interface IVendaServico
    {
        Task<VendaDto> CriarAsync(CriarVendaDto dto);
        Task<VendaDto?> ObterPorIdAsync(int id);
        Task<IEnumerable<VendaDto>> ObterTodosAsync();
        Task<IEnumerable<VendaDto>> ConsultarAsync(string termo);
    }
}
