using System.ComponentModel.DataAnnotations;

namespace ProjetoVendas.Application.DTOs
{
    public class VendedorDto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string Documento { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(250)]
        public string Email { get; set; } = string.Empty;
        
        public int LojaId { get; set; }
        public string LojaNome { get; set; } = string.Empty;
        
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
