using Microsoft.EntityFrameworkCore;
using ProjetoVendas.Application.Services.Implementation;
using ProjetoVendas.Application.Services.Interface;
using ProjetoVendas.Domain.Entities;
using ProjetoVendas.Domain.Repositories;
using ProjetoVendas.Infrastructure.Data;
using ProjetoVendas.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os controladores ao container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do DbContext
builder.Services.AddDbContext<VendasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registra os repositórios
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<ISubCategoriaRepositorio, SubCategoriaRepositorio>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<ILojaRepositorio, LojaRepositorio>();
builder.Services.AddScoped<IVendedorRepositorio, VendedorRepositorio>();
builder.Services.AddScoped<IVendaRepositorio, VendaRepositorio>();
builder.Services.AddScoped<IItemVendaRepositorio, ItemVendaRepositorio>();
builder.Services.AddScoped<IRepositorio<Venda>, VendaRepositorio>();
builder.Services.AddScoped<IRepositorio<ItemVenda>, ItemVendaRepositorio>();

// Registra os serviços de aplicação
builder.Services.AddScoped<ICategoriaServico, CategoriaServico>();
builder.Services.AddScoped<ISubCategoriaServico, SubCategoriaServico>();
builder.Services.AddScoped<IProdutoServico, ProdutoServico>();
builder.Services.AddScoped<ILojaServico, LojaServico>();
builder.Services.AddScoped<IVendedorServico, VendedorServico>();
builder.Services.AddScoped<IVendaServico, VendaServico>();

var app = builder.Build();

// Aplica as migrations pendentes no banco de dados automaticamente na inicialização
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VendasContext>();
    try
    {
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrations no banco de dados.");
    }
}

// Configura o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
