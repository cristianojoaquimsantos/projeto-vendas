using System.ComponentModel.DataAnnotations;

namespace ProjetoVendas.Application.DTOs
{
    public class CriarItemVendaDto
    {
        [Required]
        public int ProdutoId { get; set; }
        
        [Required]
        public int Quantidade { get; set; }
    }
}
