namespace Cadastro_Pessos_Endereço;

using Microsoft.EntityFrameworkCore;

class EnderecoDb : DbContext
{
    public EnderecoDb(DbContextOptions<EnderecoDb> options)
        : base(options) { }

    public DbSet<Endereco> Enderecos => Set<Endereco>();
}