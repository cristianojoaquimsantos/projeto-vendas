using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoVendas.Domain.Entities
{
    public class ItemVenda : EntidadeBase
    {
        [Required]
        public int VendaId { get; set; }
        public Venda Venda { get; set; } = null!;

        [Required]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

        [Required]
        public int Quantidade { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorUnitario { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }

        public void CalcularValorTotal()
        {
            ValorTotal = Quantidade * ValorUnitario;
        }
    }
}
