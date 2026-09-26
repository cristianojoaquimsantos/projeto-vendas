using System.ComponentModel.DataAnnotations;

namespace ProjetoVendas.Domain.Entities
{
    public class SubCategoria : EntidadeBase
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Descricao { get; set; } = string.Empty;
        
        // Relacionamento
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
        
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
