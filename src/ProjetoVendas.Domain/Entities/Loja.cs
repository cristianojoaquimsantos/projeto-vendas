using System.ComponentModel.DataAnnotations;

namespace ProjetoVendas.Domain.Entities
{
    public class Loja : EntidadeBase
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string Documento { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Endereco { get; set; } = string.Empty;
        
        // Relacionamento
        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();
    }
}
