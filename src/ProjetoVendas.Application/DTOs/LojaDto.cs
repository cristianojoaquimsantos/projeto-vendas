using System.ComponentModel.DataAnnotations;

namespace ProjetoVendas.Application.DTOs
{
    public class LojaDto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string Documento { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Endereco { get; set; } = string.Empty;
        
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
