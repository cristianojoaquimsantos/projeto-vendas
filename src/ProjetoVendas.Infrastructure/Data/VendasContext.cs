using Microsoft.EntityFrameworkCore;
using ProjetoVendas.Domain.Entities;

namespace ProjetoVendas.Infrastructure.Data
{
    public class VendasContext : DbContext
    {
        public VendasContext(DbContextOptions<VendasContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<SubCategoria> SubCategorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Loja> Lojas { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).HasMaxLength(500);
            });

            // SubCategoria
            modelBuilder.Entity<SubCategoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).HasMaxLength(500);
                entity.HasOne(e => e.Categoria)
                      .WithMany(c => c.SubCategorias)
                      .HasForeignKey(e => e.CategoriaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Produto
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descricao).HasMaxLength(1000);
                entity.Property(e => e.CodigoSKU).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ValorUnitario).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Categoria)
                      .WithMany()
                      .HasForeignKey(e => e.CategoriaId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.SubCategoria)
                      .WithMany(p => p.Produtos)
                      .HasForeignKey(e => e.SubCategoriaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Loja
            modelBuilder.Entity<Loja>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Documento).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Endereco).HasMaxLength(500);
            });

            // Vendedor
            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Documento).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(250);
                entity.HasOne(e => e.Loja)
                      .WithMany(l => l.Vendedores)
                      .HasForeignKey(e => e.LojaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Venda
            modelBuilder.Entity<Venda>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DataVenda).IsRequired();
                entity.Property(e => e.ValorTotal).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Loja)
                      .WithMany()
                      .HasForeignKey(e => e.LojaId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Vendedor)
                      .WithMany(v => v.Vendas)
                      .HasForeignKey(e => e.VendedorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ItemVenda
            modelBuilder.Entity<ItemVenda>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantidade).IsRequired();
                entity.Property(e => e.ValorUnitario).HasColumnType("decimal(18,2)");
                entity.Property(e => e.ValorTotal).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Venda)
                      .WithMany(v => v.Itens)
                      .HasForeignKey(e => e.VendaId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Produto)
                      .WithMany()
                      .HasForeignKey(e => e.ProdutoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
