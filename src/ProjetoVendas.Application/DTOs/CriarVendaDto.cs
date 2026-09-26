using System.ComponentModel.DataAnnotations;

namespace ProjetoVendas.Application.DTOs
{
    public class CriarVendaDto
    {
        [Required]
        public int LojaId { get; set; }
        
        [Required]
        public int VendedorId { get; set; }
        
        [Required]
        public List<CriarItemVendaDto> Itens { get; set; } = new List<CriarItemVendaDto>();
    }
}
