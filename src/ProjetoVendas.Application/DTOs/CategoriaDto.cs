using System.ComponentModel.DataAnnotations;

namespace ProjetoVendas.Application.DTOs
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Descricao { get; set; } = string.Empty;
        
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
