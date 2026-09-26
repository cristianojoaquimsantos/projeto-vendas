using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Application.DTOs;
using ProjetoVendas.Application.Services.Interface;

namespace ProjetoVendas.Application.Services.Implementation
{
    public class SubCategoriaServico : ISubCategoriaServico
    {
        private readonly ISubCategoriaRepositorio _subCategoriaRepositorio;

        public SubCategoriaServico(ISubCategoriaRepositorio subCategoriaRepositorio)
        {
            _subCategoriaRepositorio = subCategoriaRepositorio;
        }

        public async Task<SubCategoriaDto> CriarAsync(SubCategoriaDto dto)
        {
            var subCategoria = new SubCategoria
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                CategoriaId = dto.CategoriaId,
                Ativo = dto.Ativo
            };

            await _subCategoriaRepositorio.AdicionarAsync(subCategoria);
            await _subCategoriaRepositorio.SalvarAlteracoesAsync();

            return new SubCategoriaDto
            {
                Id = subCategoria.Id,
                Nome = subCategoria.Nome,
                Descricao = subCategoria.Descricao,
                CategoriaId = subCategoria.CategoriaId,
                DataCadastro = subCategoria.DataCadastro,
                Ativo = subCategoria.Ativo
            };
        }

        public async Task<SubCategoriaDto?> ObterPorIdAsync(int id)
        {
            var subCategoria = await _subCategoriaRepositorio.ObterPorIdAsync(id);
            if (subCategoria == null) return null;

            return new SubCategoriaDto
            {
                Id = subCategoria.Id,
                Nome = subCategoria.Nome,
                Descricao = subCategoria.Descricao,
                CategoriaId = subCategoria.CategoriaId,
                DataCadastro = subCategoria.DataCadastro,
                Ativo = subCategoria.Ativo
            };
        }

        public async Task<IEnumerable<SubCategoriaDto>> ObterTodosAsync()
        {
            var subCategorias = await _subCategoriaRepositorio.ObterTodosAsync();
            return subCategorias.Select(sc => new SubCategoriaDto
            {
                Id = sc.Id,
                Nome = sc.Nome,
                Descricao = sc.Descricao,
                CategoriaId = sc.CategoriaId,
                DataCadastro = sc.DataCadastro,
                Ativo = sc.Ativo
            });
        }

        public async Task<SubCategoriaDto> AtualizarAsync(int id, SubCategoriaDto dto)
        {
            var subCategoria = await _subCategoriaRepositorio.ObterPorIdAsync(id);
            if (subCategoria == null) throw new ArgumentException("Subcategoria não encontrada.");

            subCategoria.Nome = dto.Nome;
            subCategoria.Descricao = dto.Descricao;
            subCategoria.CategoriaId = dto.CategoriaId;
            subCategoria.Ativo = dto.Ativo;

            await _subCategoriaRepositorio.AtualizarAsync(subCategoria);
            await _subCategoriaRepositorio.SalvarAlteracoesAsync();

            return new SubCategoriaDto
            {
                Id = subCategoria.Id,
                Nome = subCategoria.Nome,
                Descricao = subCategoria.Descricao,
                CategoriaId = subCategoria.CategoriaId,
                DataCadastro = subCategoria.DataCadastro,
                Ativo = subCategoria.Ativo
            };
        }

        public async Task RemoverAsync(int id)
        {
            var subCategoria = await _subCategoriaRepositorio.ObterPorIdAsync(id);
            if (subCategoria != null)
            {
                await _subCategoriaRepositorio.RemoverAsync(subCategoria);
                await _subCategoriaRepositorio.SalvarAlteracoesAsync();
            }
        }

        public async Task<IEnumerable<SubCategoriaDto>> ConsultarAsync(string nome)
        {
            var subCategorias = await _subCategoriaRepositorio.ConsultarAsync(sc => sc.Nome.Contains(nome));
            return subCategorias.Select(sc => new SubCategoriaDto
            {
                Id = sc.Id,
                Nome = sc.Nome,
                Descricao = sc.Descricao,
                CategoriaId = sc.CategoriaId,
                DataCadastro = sc.DataCadastro,
                Ativo = sc.Ativo
            });
        }
    }
}
