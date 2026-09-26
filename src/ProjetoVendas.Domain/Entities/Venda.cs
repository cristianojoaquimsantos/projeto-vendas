using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoVendas.Domain.Entities
{
    public class Venda : EntidadeBase
    {
        [Required]
        public DateTime DataVenda { get; set; } = DateTime.Now;

        [Required]
        public int LojaId { get; set; }
        public Loja Loja { get; set; } = null!;

        [Required]
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }

        public ICollection<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
    }
}
