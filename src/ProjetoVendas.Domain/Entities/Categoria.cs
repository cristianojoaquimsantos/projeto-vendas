using System.ComponentModel.DataAnnotations;

namespace ProjetoVendas.Domain.Entities
{
    public class Categoria : EntidadeBase
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Descricao { get; set; } = string.Empty;
        
        // Relacionamento
        public ICollection<SubCategoria> SubCategorias { get; set; } = new List<SubCategoria>();
    }
}
