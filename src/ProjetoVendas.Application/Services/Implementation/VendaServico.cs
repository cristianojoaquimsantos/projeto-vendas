using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Application.DTOs;
using ProjetoVendas.Application.Services.Interface;

namespace ProjetoVendas.Application.Services.Implementation
{
    public class VendaServico : IVendaServico
    {
        private readonly IVendaRepositorio _vendaRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ILojaRepositorio _lojaRepositorio;
        private readonly IVendedorRepositorio _vendedorRepositorio;

        public VendaServico(
            IVendaRepositorio vendaRepositorio,
            IProdutoRepositorio produtoRepositorio,
            ILojaRepositorio lojaRepositorio,
            IVendedorRepositorio vendedorRepositorio)
        {
            _vendaRepositorio = vendaRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _lojaRepositorio = lojaRepositorio;
            _vendedorRepositorio = vendedorRepositorio;
        }

        public async Task<VendaDto> CriarAsync(CriarVendaDto dto)
        {
            var loja = await _lojaRepositorio.ObterPorIdAsync(dto.LojaId);
            if (loja == null)
                throw new ArgumentException("Loja não encontrada.");

            var vendedor = await _vendedorRepositorio.ObterPorIdAsync(dto.VendedorId);
            if (vendedor == null)
                throw new ArgumentException("Vendedor não encontrado.");

            if (dto.Itens == null || !dto.Itens.Any())
                throw new ArgumentException("A venda deve conter pelo menos um item.");

            var venda = new Venda
            {
                LojaId = dto.LojaId,
                VendedorId = dto.VendedorId,
                DataVenda = DateTime.Now,
                Itens = new List<ItemVenda>()
            };

            decimal valorTotalVenda = 0;

            foreach (var itemDto in dto.Itens)
            {
                var produto = await _produtoRepositorio.ObterPorIdAsync(itemDto.ProdutoId);
                if (produto == null)
                    throw new ArgumentException($"Produto com ID {itemDto.ProdutoId} não encontrado.");

                if (!produto.ValidarEstoque(itemDto.Quantidade))
                    throw new InvalidOperationException($"Estoque insuficiente para o produto '{produto.Nome}'.");

                await _produtoRepositorio.ReduzirEstoqueAsync(produto.Id, itemDto.Quantidade);

                var itemVenda = new ItemVenda
                {
                    ProdutoId = produto.Id,
                    Quantidade = itemDto.Quantidade,
                    ValorUnitario = produto.ValorUnitario,
                    ValorTotal = produto.ValorUnitario * itemDto.Quantidade
                };

                valorTotalVenda += itemVenda.ValorTotal;
                venda.Itens.Add(itemVenda);
            }

            venda.ValorTotal = valorTotalVenda;

            await _vendaRepositorio.AdicionarAsync(venda);
            await _vendaRepositorio.SalvarAlteracoesAsync();

            return await ObterPorIdAsync(venda.Id) ?? new VendaDto
            {
                Id = venda.Id,
                DataVenda = venda.DataVenda,
                LojaId = venda.LojaId,
                LojaNome = loja.Nome,
                VendedorId = venda.VendedorId,
                VendedorNome = vendedor.Nome,
                ValorTotal = venda.ValorTotal
            };
        }

        public async Task<VendaDto?> ObterPorIdAsync(int id)
        {
            var venda = await _vendaRepositorio.ObterComDetalhesAsync(id);
            if (venda == null) return null;

            return new VendaDto
            {
                Id = venda.Id,
                DataVenda = venda.DataVenda,
                LojaId = venda.LojaId,
                LojaNome = venda.Loja?.Nome ?? string.Empty,
                VendedorId = venda.VendedorId,
                VendedorNome = venda.Vendedor?.Nome ?? string.Empty,
                ValorTotal = venda.ValorTotal,
                Itens = venda.Itens.Select(i => new ItemVendaDto
                {
                    Id = i.Id,
                    VendaId = i.VendaId,
                    ProdutoId = i.ProdutoId,
                    ProdutoNome = i.Produto?.Nome ?? string.Empty,
                    Quantidade = i.Quantidade,
                    ValorUnitario = i.ValorUnitario,
                    ValorTotal = i.ValorTotal
                }).ToList()
            };
        }

        public async Task<IEnumerable<VendaDto>> ObterTodosAsync()
        {
            var vendas = await _vendaRepositorio.ObterTodosAsync();
            var resultado = new List<VendaDto>();

            foreach (var venda in vendas)
            {
                var detalhe = await ObterPorIdAsync(venda.Id);
                if (detalhe != null)
                {
                    resultado.Add(detalhe);
                }
            }

            return resultado;
        }

        public async Task<IEnumerable<VendaDto>> ConsultarAsync(string termo)
        {
            var vendas = await _vendaRepositorio.ObterTodosAsync();
            var resultado = new List<VendaDto>();

            foreach (var venda in vendas)
            {
                var detalhe = await ObterPorIdAsync(venda.Id);
                if (detalhe != null && (
                    detalhe.LojaNome.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                    detalhe.VendedorNome.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                    detalhe.Id.ToString() == termo))
                {
                    resultado.Add(detalhe);
                }
            }

            return resultado;
        }
    }
}
