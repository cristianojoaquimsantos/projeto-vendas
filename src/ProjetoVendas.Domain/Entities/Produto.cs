using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoVendas.Domain.Entities
{
    public class Produto : EntidadeBase
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string CodigoSKU { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorUnitario { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

        public int? SubCategoriaId { get; set; }
        public SubCategoria? SubCategoria { get; set; }

        [Required]
        public int Estoque { get; set; }

        public bool ValidarEstoque(int quantidade)
        {
            return Estoque >= quantidade;
        }

        public void ReduzirEstoque(int quantidade)
        {
            if (!ValidarEstoque(quantidade))
                throw new InvalidOperationException("Quantidade insuficiente em estoque.");
                
            Estoque -= quantidade;
        }
    }
}
