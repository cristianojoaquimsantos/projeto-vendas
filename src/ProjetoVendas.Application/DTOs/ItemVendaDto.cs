namespace ProjetoVendas.Application.DTOs
{
    public class ItemVendaDto
    {
        public int Id { get; set; }
        
        public int VendaId { get; set; }
        
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; } = string.Empty;
        
        public int Quantidade { get; set; }
        
        public decimal ValorUnitario { get; set; }
        
        public decimal ValorTotal { get; set; }
    }
}
