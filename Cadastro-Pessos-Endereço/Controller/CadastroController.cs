using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cadastro_Pessos_Endereço.Controller;

[ApiController]
public class CadastroController : ControllerBase
{
    private readonly PessoaDb _pessoaContext;
    private readonly EnderecoDb _enderecoContext;

    public CadastroController(PessoaDb pessoaContext, EnderecoDb enderecoContext)
    {
        _pessoaContext = pessoaContext;
        _enderecoContext = enderecoContext;
    }
    
    //pessoa
    [HttpGet("/novapessoa")]
    public async Task<ActionResult<IEnumerable<Pessoa>>> GetNovapessoa()
    {
        return await _pessoaContext.Pessoas.ToListAsync();
    }
    
    // Pessoa Endpoints
    [HttpPost("/novapessoa")]
    public async Task<ActionResult<Pessoa>> CreateNovapessoa([FromBody] Pessoa pessoa, [FromServices] PessoaDb db)
    {
        db.Pessoas.Add(pessoa);
        await db.SaveChangesAsync();
        return Created($"/novapessoa/{pessoa.Id}", pessoa);
    }

    [HttpGet("/novapessoa/{id}")]
    public async Task<ActionResult> GetNovapessoaById(int id, [FromServices] PessoaDb dbP, [FromServices] EnderecoDb dbE)
    {
        var pessoa = await dbP.Pessoas.FindAsync(id);
        if (pessoa == null) return NotFound();

        var enderecos = await dbE.Enderecos
            .Where(e => e.PessoaId == id)
            .ToListAsync();

        var resultado = new
        {
            Pessoa = pessoa,
            Enderecos = enderecos
        };

        return Ok(resultado);
    }

    // Endereco Endpoints
    [HttpGet("/novoendereco")]
    public async Task<ActionResult<IEnumerable<Endereco>>> GetNovoendereco([FromServices] EnderecoDb db)
    {
        return Ok(await db.Enderecos.ToListAsync());
    }

    [HttpPost("/novoendereco")]
    public async Task<ActionResult<Endereco>> CreateNovoendereco([FromBody] Endereco endereco, [FromServices] EnderecoDb db)
    {
        db.Enderecos.Add(endereco);
        await db.SaveChangesAsync();
        return Created($"/novoendereco/{endereco.Id}", endereco);
    }

    [HttpGet("/novoendereco/{id}")]
    public async Task<ActionResult> GetNovoenderecoById(int id, [FromServices] EnderecoDb db)
    {
        var endereco = await db.Enderecos.FindAsync(id);
        return endereco != null ? Ok(endereco) : NotFound();
    }
}