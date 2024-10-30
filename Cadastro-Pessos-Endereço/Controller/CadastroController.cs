using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cadastro_Pessos_Endereço.Controller;

[ApiController]
public class CadastroController : ControllerBase
{
    private PessoaDb pessoaContext;
    private EnderecoDb enderecoContext;

    public CadastroController(PessoaDb pessoaContext, EnderecoDb enderecoContext)
    {
        this.pessoaContext = pessoaContext;
        this.enderecoContext = enderecoContext;
    }
    
    //pessoa
    [HttpGet("/novapessoa")]
    public async Task<ActionResult<IEnumerable<Pessoa>>> GetNovapessoa()
    {
        return await pessoaContext.Pessoas.ToListAsync();
    }
    
    [HttpPost("/novapessoa")]
    
    app.MapPost("/novapessoa", async (Pessoa pessoa, PessoaDb db) =>
    {
        db.Pessoas.Add(pessoa);
        await db.SaveChangesAsync();
    
        return Results.Created($"/novapessoa/{pessoa.Id}", pessoa);
    });
    
    // app.MapGet("/novapessoa/{id}", async (int id, PessoaDb dbP, EnderecoDb dbE) =>
    // {
    //     var pessoa = await dbP.Pessoas.FindAsync(id);
    //     if (pessoa == null) return Results.NotFound();
    //
    //     var enderecos = await dbE.Enderecos
    //         .Where(e => e.PessoaId == id)
    //         .ToListAsync();
    //
    //     var resultado = new
    //     {
    //         Pessoa = pessoa,
    //         Enderecos = enderecos
    //     };
    //
    //     return Results.Ok(resultado);
    // });
    //
    // //endereco
    //
    // app.MapGet("/novoendereco", async (EnderecoDb db) =>
    //     await db.Enderecos.ToListAsync());
    //
    // app.MapPost("/novoendereco", async (Endereco endereco, EnderecoDb db) =>
    // {
    //     db.Enderecos.Add(endereco);
    //     await db.SaveChangesAsync();
    //
    //     return Results.Created($"/novoendereco/{endereco.Id}", endereco);
    // });
    //
    // app.MapGet("/novoendereco/{id}", async (int id, EnderecoDb db) =>
    //     await db.Enderecos.FindAsync(id)
    //         is Endereco endereco
    //         ? Results.Ok(endereco)
    //         : Results.NotFound());

}