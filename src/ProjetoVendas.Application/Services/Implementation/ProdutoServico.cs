using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Application.DTOs;
using ProjetoVendas.Application.Services.Interface;

namespace ProjetoVendas.Application.Services.Implementation
{
    public class ProdutoServico : IProdutoServico
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoServico(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<ProdutoDto> CriarAsync(ProdutoDto dto)
        {
            var produto = new Produto
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                CodigoSKU = dto.CodigoSKU,
                CategoriaId = dto.CategoriaId,
                SubCategoriaId = dto.SubCategoriaId,
                ValorUnitario = dto.ValorUnitario,
                Estoque = dto.QuantidadeEstoque,
                Ativo = dto.Ativo
            };

            await _produtoRepositorio.AdicionarAsync(produto);
            await _produtoRepositorio.SalvarAlteracoesAsync();

            return new ProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                CodigoSKU = produto.CodigoSKU,
                CategoriaId = produto.CategoriaId,
                SubCategoriaId = produto.SubCategoriaId,
                ValorUnitario = produto.ValorUnitario,
                QuantidadeEstoque = produto.Estoque,
                DataCadastro = produto.DataCadastro,
                Ativo = produto.Ativo
            };
        }

        public async Task<ProdutoDto?> ObterPorIdAsync(int id)
        {
            var produto = await _produtoRepositorio.ObterComCategoriasAsync(id) 
                          ?? await _produtoRepositorio.ObterPorIdAsync(id);
            if (produto == null) return null;

            return new ProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                CodigoSKU = produto.CodigoSKU,
                CategoriaId = produto.CategoriaId,
                CategoriaNome = produto.Categoria?.Nome ?? string.Empty,
                SubCategoriaId = produto.SubCategoriaId,
                SubCategoriaNome = produto.SubCategoria?.Nome,
                ValorUnitario = produto.ValorUnitario,
                QuantidadeEstoque = produto.Estoque,
                DataCadastro = produto.DataCadastro,
                Ativo = produto.Ativo
            };
        }

        public async Task<IEnumerable<ProdutoDto>> ObterTodosAsync()
        {
            var produtos = await _produtoRepositorio.ObterTodosAsync();
            
            var result = new List<ProdutoDto>();
            foreach (var produto in produtos)
            {
                result.Add(new ProdutoDto
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    CodigoSKU = produto.CodigoSKU,
                    CategoriaId = produto.CategoriaId,
                    SubCategoriaId = produto.SubCategoriaId,
                    ValorUnitario = produto.ValorUnitario,
                    QuantidadeEstoque = produto.Estoque,
                    DataCadastro = produto.DataCadastro,
                    Ativo = produto.Ativo
                });
            }
            
            return result;
        }

        public async Task<ProdutoDto> AtualizarAsync(int id, ProdutoDto dto)
        {
            var produto = await _produtoRepositorio.ObterPorIdAsync(id);
            if (produto == null) throw new ArgumentException("Produto não encontrado.");

            produto.Nome = dto.Nome;
            produto.Descricao = dto.Descricao;
            produto.CodigoSKU = dto.CodigoSKU;
            produto.CategoriaId = dto.CategoriaId;
            produto.SubCategoriaId = dto.SubCategoriaId;
            produto.ValorUnitario = dto.ValorUnitario;
            produto.Estoque = dto.QuantidadeEstoque;
            produto.Ativo = dto.Ativo;

            await _produtoRepositorio.AtualizarAsync(produto);
            await _produtoRepositorio.SalvarAlteracoesAsync();

            return new ProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                CodigoSKU = produto.CodigoSKU,
                CategoriaId = produto.CategoriaId,
                SubCategoriaId = produto.SubCategoriaId,
                ValorUnitario = produto.ValorUnitario,
                QuantidadeEstoque = produto.Estoque,
                DataCadastro = produto.DataCadastro,
                Ativo = produto.Ativo
            };
        }

        public async Task RemoverAsync(int id)
        {
            var produto = await _produtoRepositorio.ObterPorIdAsync(id);
            if (produto != null)
            {
                await _produtoRepositorio.RemoverAsync(produto);
                await _produtoRepositorio.SalvarAlteracoesAsync();
            }
        }

        public async Task<IEnumerable<ProdutoDto>> ConsultarAsync(string nome)
        {
            var produtos = await _produtoRepositorio.ConsultarAsync(p => p.Nome.Contains(nome));
            
            var result = new List<ProdutoDto>();
            foreach (var produto in produtos)
            {
                result.Add(new ProdutoDto
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    CodigoSKU = produto.CodigoSKU,
                    CategoriaId = produto.CategoriaId,
                    SubCategoriaId = produto.SubCategoriaId,
                    ValorUnitario = produto.ValorUnitario,
                    QuantidadeEstoque = produto.Estoque,
                    DataCadastro = produto.DataCadastro,
                    Ativo = produto.Ativo
                });
            }
            
            return result;
        }
    }
}
