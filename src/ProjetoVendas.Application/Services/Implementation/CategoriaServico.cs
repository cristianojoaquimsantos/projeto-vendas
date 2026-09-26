using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Application.DTOs;
using ProjetoVendas.Application.Services.Interface;

namespace ProjetoVendas.Application.Services.Implementation
{
    public class CategoriaServico : ICategoriaServico
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;

        public CategoriaServico(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }

        public async Task<CategoriaDto> CriarAsync(CategoriaDto dto)
        {
            var categoria = new Categoria
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Ativo = dto.Ativo
            };

            await _categoriaRepositorio.AdicionarAsync(categoria);
            await _categoriaRepositorio.SalvarAlteracoesAsync();

            return new CategoriaDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                DataCadastro = categoria.DataCadastro,
                Ativo = categoria.Ativo
            };
        }

        public async Task<CategoriaDto?> ObterPorIdAsync(int id)
        {
            var categoria = await _categoriaRepositorio.ObterPorIdAsync(id);
            if (categoria == null) return null;

            return new CategoriaDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                DataCadastro = categoria.DataCadastro,
                Ativo = categoria.Ativo
            };
        }

        public async Task<IEnumerable<CategoriaDto>> ObterTodosAsync()
        {
            var categorias = await _categoriaRepositorio.ObterTodosAsync();
            return categorias.Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Descricao = c.Descricao,
                DataCadastro = c.DataCadastro,
                Ativo = c.Ativo
            });
        }

        public async Task<CategoriaDto> AtualizarAsync(int id, CategoriaDto dto)
        {
            var categoria = await _categoriaRepositorio.ObterPorIdAsync(id);
            if (categoria == null) throw new ArgumentException("Categoria não encontrada.");

            categoria.Nome = dto.Nome;
            categoria.Descricao = dto.Descricao;
            categoria.Ativo = dto.Ativo;

            await _categoriaRepositorio.AtualizarAsync(categoria);
            await _categoriaRepositorio.SalvarAlteracoesAsync();

            return new CategoriaDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                DataCadastro = categoria.DataCadastro,
                Ativo = categoria.Ativo
            };
        }

        public async Task RemoverAsync(int id)
        {
            var categoria = await _categoriaRepositorio.ObterPorIdAsync(id);
            if (categoria != null)
            {
                await _categoriaRepositorio.RemoverAsync(categoria);
                await _categoriaRepositorio.SalvarAlteracoesAsync();
            }
        }

        public async Task<IEnumerable<CategoriaDto>> ConsultarAsync(string nome)
        {
            var categorias = await _categoriaRepositorio.ConsultarAsync(c => c.Nome.Contains(nome));
            return categorias.Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Descricao = c.Descricao,
                DataCadastro = c.DataCadastro,
                Ativo = c.Ativo
            });
        }
    }
}
