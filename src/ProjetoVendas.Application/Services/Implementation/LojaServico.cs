using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Application.DTOs;
using ProjetoVendas.Application.Services.Interface;

namespace ProjetoVendas.Application.Services.Implementation
{
    public class LojaServico : ILojaServico
    {
        private readonly ILojaRepositorio _lojaRepositorio;

        public LojaServico(ILojaRepositorio lojaRepositorio)
        {
            _lojaRepositorio = lojaRepositorio;
        }

        public async Task<LojaDto> CriarAsync(LojaDto dto)
        {
            var loja = new Loja
            {
                Nome = dto.Nome,
                Documento = dto.Documento,
                Endereco = dto.Endereco,
                Ativo = dto.Ativo
            };

            await _lojaRepositorio.AdicionarAsync(loja);
            await _lojaRepositorio.SalvarAlteracoesAsync();

            return new LojaDto
            {
                Id = loja.Id,
                Nome = loja.Nome,
                Documento = loja.Documento,
                Endereco = loja.Endereco,
                DataCadastro = loja.DataCadastro,
                Ativo = loja.Ativo
            };
        }

        public async Task<LojaDto?> ObterPorIdAsync(int id)
        {
            var loja = await _lojaRepositorio.ObterPorIdAsync(id);
            if (loja == null) return null;

            return new LojaDto
            {
                Id = loja.Id,
                Nome = loja.Nome,
                Documento = loja.Documento,
                Endereco = loja.Endereco,
                DataCadastro = loja.DataCadastro,
                Ativo = loja.Ativo
            };
        }

        public async Task<IEnumerable<LojaDto>> ObterTodosAsync()
        {
            var lojas = await _lojaRepositorio.ObterTodosAsync();
            return lojas.Select(l => new LojaDto
            {
                Id = l.Id,
                Nome = l.Nome,
                Documento = l.Documento,
                Endereco = l.Endereco,
                DataCadastro = l.DataCadastro,
                Ativo = l.Ativo
            });
        }

        public async Task<LojaDto> AtualizarAsync(int id, LojaDto dto)
        {
            var loja = await _lojaRepositorio.ObterPorIdAsync(id);
            if (loja == null) throw new ArgumentException("Loja não encontrada.");

            loja.Nome = dto.Nome;
            loja.Documento = dto.Documento;
            loja.Endereco = dto.Endereco;
            loja.Ativo = dto.Ativo;

            await _lojaRepositorio.AtualizarAsync(loja);
            await _lojaRepositorio.SalvarAlteracoesAsync();

            return new LojaDto
            {
                Id = loja.Id,
                Nome = loja.Nome,
                Documento = loja.Documento,
                Endereco = loja.Endereco,
                DataCadastro = loja.DataCadastro,
                Ativo = loja.Ativo
            };
        }

        public async Task RemoverAsync(int id)
        {
            var loja = await _lojaRepositorio.ObterPorIdAsync(id);
            if (loja != null)
            {
                await _lojaRepositorio.RemoverAsync(loja);
                await _lojaRepositorio.SalvarAlteracoesAsync();
            }
        }

        public async Task<IEnumerable<LojaDto>> ConsultarAsync(string nome)
        {
            var lojas = await _lojaRepositorio.ConsultarAsync(l => l.Nome.Contains(nome));
            return lojas.Select(l => new LojaDto
            {
                Id = l.Id,
                Nome = l.Nome,
                Documento = l.Documento,
                Endereco = l.Endereco,
                DataCadastro = l.DataCadastro,
                Ativo = l.Ativo
            });
        }
    }
}
