﻿namespace Cadastro_Pessos_Endereço;

using Microsoft.EntityFrameworkCore;

public class PessoaDb : DbContext
{
    public PessoaDb(DbContextOptions<PessoaDb> options)
        : base(options) { }

    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
}