using System.ComponentModel.DataAnnotations;

namespace ProjetoVendas.Domain.Entities
{
    public abstract class EntidadeBase
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        
        public bool Ativo { get; set; } = true;
    }
}
