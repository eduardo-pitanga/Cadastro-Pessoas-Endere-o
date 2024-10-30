using Cadastro_Pessos_Endereço;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PessoaDb>(opt => opt.UseInMemoryDatabase("Cadastro_Pessoas"));
builder.Services.AddDbContext<EnderecoDb>(opt => opt.UseInMemoryDatabase("Cadastro_Enderecos"));
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();