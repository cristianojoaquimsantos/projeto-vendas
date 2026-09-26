using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Application.DTOs;
using ProjetoVendas.Application.Services.Interface;

namespace ProjetoVendas.Application.Services.Implementation
{
    public class VendedorServico : IVendedorServico
    {
        private readonly IVendedorRepositorio _vendedorRepositorio;

        public VendedorServico(IVendedorRepositorio vendedorRepositorio)
        {
            _vendedorRepositorio = vendedorRepositorio;
        }

        public async Task<VendedorDto> CriarAsync(VendedorDto dto)
        {
            var vendedor = new Vendedor
            {
                Nome = dto.Nome,
                Documento = dto.Documento,
                Email = dto.Email,
                LojaId = dto.LojaId,
                Ativo = dto.Ativo
            };

            await _vendedorRepositorio.AdicionarAsync(vendedor);
            await _vendedorRepositorio.SalvarAlteracoesAsync();

            return new VendedorDto
            {
                Id = vendedor.Id,
                Nome = vendedor.Nome,
                Documento = vendedor.Documento,
                Email = vendedor.Email,
                LojaId = vendedor.LojaId,
                DataCadastro = vendedor.DataCadastro,
                Ativo = vendedor.Ativo
            };
        }

        public async Task<VendedorDto?> ObterPorIdAsync(int id)
        {
            var vendedor = await _vendedorRepositorio.ObterPorIdAsync(id);
            if (vendedor == null) return null;

            return new VendedorDto
            {
                Id = vendedor.Id,
                Nome = vendedor.Nome,
                Documento = vendedor.Documento,
                Email = vendedor.Email,
                LojaId = vendedor.LojaId,
                DataCadastro = vendedor.DataCadastro,
                Ativo = vendedor.Ativo
            };
        }

        public async Task<IEnumerable<VendedorDto>> ObterTodosAsync()
        {
            var vendedores = await _vendedorRepositorio.ObterTodosAsync();
            return vendedores.Select(v => new VendedorDto
            {
                Id = v.Id,
                Nome = v.Nome,
                Documento = v.Documento,
                Email = v.Email,
                LojaId = v.LojaId,
                DataCadastro = v.DataCadastro,
                Ativo = v.Ativo
            });
        }

        public async Task<VendedorDto> AtualizarAsync(int id, VendedorDto dto)
        {
            var vendedor = await _vendedorRepositorio.ObterPorIdAsync(id);
            if (vendedor == null) throw new ArgumentException("Vendedor não encontrado.");

            vendedor.Nome = dto.Nome;
            vendedor.Documento = dto.Documento;
            vendedor.Email = dto.Email;
            vendedor.LojaId = dto.LojaId;
            vendedor.Ativo = dto.Ativo;

            await _vendedorRepositorio.AtualizarAsync(vendedor);
            await _vendedorRepositorio.SalvarAlteracoesAsync();

            return new VendedorDto
            {
                Id = vendedor.Id,
                Nome = vendedor.Nome,
                Documento = vendedor.Documento,
                Email = vendedor.Email,
                LojaId = vendedor.LojaId,
                DataCadastro = vendedor.DataCadastro,
                Ativo = vendedor.Ativo
            };
        }

        public async Task RemoverAsync(int id)
        {
            var vendedor = await _vendedorRepositorio.ObterPorIdAsync(id);
            if (vendedor != null)
            {
                await _vendedorRepositorio.RemoverAsync(vendedor);
                await _vendedorRepositorio.SalvarAlteracoesAsync();
            }
        }

        public async Task<IEnumerable<VendedorDto>> ConsultarAsync(string nome)
        {
            var vendedores = await _vendedorRepositorio.ConsultarAsync(v => v.Nome.Contains(nome));
            return vendedores.Select(v => new VendedorDto
            {
                Id = v.Id,
                Nome = v.Nome,
                Documento = v.Documento,
                Email = v.Email,
                LojaId = v.LojaId,
                DataCadastro = v.DataCadastro,
                Ativo = v.Ativo
            });
        }
    }
}
