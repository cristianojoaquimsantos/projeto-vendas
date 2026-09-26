using System.ComponentModel.DataAnnotations;

namespace ProjetoVendas.Application.DTOs
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string Descricao { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string CodigoSKU { get; set; } = string.Empty;
        
        public int CategoriaId { get; set; }
        public string CategoriaNome { get; set; } = string.Empty;
        
        public int? SubCategoriaId { get; set; }
        public string? SubCategoriaNome { get; set; }
        
        [Required]
        public decimal ValorUnitario { get; set; }
        
        [Required]
        public int QuantidadeEstoque { get; set; }
        
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
