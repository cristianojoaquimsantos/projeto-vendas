using System.ComponentModel.DataAnnotations;

namespace ProjetoVendas.Domain.Entities
{
    public class Vendedor : EntidadeBase
    {
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
        
        // Relacionamentos
        public int LojaId { get; set; }
        public Loja Loja { get; set; } = null!;
        
        // Vendas realizadas por este vendedor
        public ICollection<Venda> Vendas { get; set; } = new List<Venda>();
    }
}
