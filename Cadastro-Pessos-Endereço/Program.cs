using Microsoft.EntityFrameworkCore;
using Cadastro_Pessos_Endereço;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PessoaDb>(opt => opt.UseInMemoryDatabase("Cadastro_Pessoas"));
builder.Services.AddDbContext<EnderecoDb>(opt => opt.UseInMemoryDatabase("Cadastro_Enderecos"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

//pessoa

app.MapGet("/novapessoa", async (PessoaDb db) =>
    await db.Pessoas.ToListAsync());

app.MapPost("/novapessoa", async (Pessoa pessoa, PessoaDb db) =>
{
    db.Pessoas.Add(pessoa);
    await db.SaveChangesAsync();

    return Results.Created($"/novapessoa/{pessoa.Id}", pessoa);
});

app.MapGet("/novapessoa/{id}", async (int id, PessoaDb dbP, EnderecoDb dbE) =>
{
    var pessoa = await dbP.Pessoas.FindAsync(id);
    if (pessoa == null) return Results.NotFound();

    var enderecos = await dbE.Enderecos
        .Where(e => e.PessoaId == id)
        .ToListAsync();

    var resultado = new
    {
        Pessoa = pessoa,
        Enderecos = enderecos
    };
    
    return Results.Ok(resultado);
});

//endereco

app.MapGet("/novoendereco", async (EnderecoDb db) =>
    await db.Enderecos.ToListAsync());

app.MapPost("/novoendereco", async (Endereco endereco, EnderecoDb db) =>
{
    db.Enderecos.Add(endereco);
    await db.SaveChangesAsync();

    return Results.Created($"/novoendereco/{endereco.Id}", endereco);
});

app.MapGet("/novoendereco/{id}", async (int id, EnderecoDb db) =>
    await db.Enderecos.FindAsync(id)
        is Endereco endereco
        ? Results.Ok(endereco)
        : Results.NotFound());

// app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDb db) =>
// {
//     var todo = await db.Todos.FindAsync(id);
//
//     if (todo is null) return Results.NotFound();
//
//     todo.Name = inputTodo.Name;
//     todo.IsComplete = inputTodo.IsComplete;
//
//     await db.SaveChangesAsync();
//
//     return Results.NoContent();
// });
//
// app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
// {
//     if (await db.Todos.FindAsync(id) is Todo todo)
//     {
//         db.Todos.Remove(todo);
//         await db.SaveChangesAsync();
//         return Results.NoContent();
//     }
//
//     return Results.NotFound();
// });

app.Run();