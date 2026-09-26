namespace ProjetoVendas.Application.DTOs
{
    public class VendaDto
    {
        public int Id { get; set; }
        
        public DateTime DataVenda { get; set; }
        
        public int LojaId { get; set; }
        public string LojaNome { get; set; } = string.Empty;
        
        public int VendedorId { get; set; }
        public string VendedorNome { get; set; } = string.Empty;
        
        public decimal ValorTotal { get; set; }
        
        public ICollection<ItemVendaDto> Itens { get; set; } = new List<ItemVendaDto>();
    }
}
