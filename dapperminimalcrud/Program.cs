using dapperminimalcrud.models;
using dapperminimalcrud.repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>(_ => new ProdutoRepository(builder.Configuration.GetConnectionString("MinhaLojaDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/produtos", async (IProdutoRepository repo) =>
{
    return await repo.GetAllProdutos();
});

app.MapGet("/produtos/{id}", async (IProdutoRepository repo, int id) =>
{
    return await repo.GetProduto(id);
});

app.MapPost("/produtos", async (IProdutoRepository repo, Produto produto) =>
{
    await repo.AddProduto(produto);
    return Results.Created($"/produtos/{produto.Id}", produto);
});

app.MapPut("/produtos/{id}", async (IProdutoRepository repo, int id, Produto produto) =>
{
    produto.Id = id;
    await repo.UpdateProduto(produto);
    return Results.NoContent();
});

app.MapDelete("/produtos/{id}", async (IProdutoRepository repo, int id) =>
{
    await repo.DeleteProduto(id);
    return Results.NoContent();
});


app.Run();


