﻿namespace Cadastro_Pessos_Endereço;

public class Endereco
{
    public int Id { get; set; }
    public string? Logradouro { get; set; }
    public int Numero { get; set; }
    public string? Estado { get; set; }
    public string? Cidade { get; set; }
    public string? Bairro { get; set; }
    public int PessoaId { get; set; }
}