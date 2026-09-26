using ProjetoVendas.Application.DTOs;

namespace ProjetoVendas.Application.Services.Interface
{
    public interface IVendedorServico
    {
        Task<VendedorDto> CriarAsync(VendedorDto dto);
        Task<VendedorDto?> ObterPorIdAsync(int id);
        Task<IEnumerable<VendedorDto>> ObterTodosAsync();
        Task<VendedorDto> AtualizarAsync(int id, VendedorDto dto);
        Task RemoverAsync(int id);
        Task<IEnumerable<VendedorDto>> ConsultarAsync(string nome);
    }
}
