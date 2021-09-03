using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CadastroPessoa.Models
{
    public class Pessoa
    {       
            [Key]
            public string Codigo { get; set; }
            public string Nome { get; set; }
            public string CpfCnpj { get; set; }
            public TipoPessoa Tipo { get; set; }
            public DateTime? DataNascimento { get; set; }
            public bool Ativo { get; set; }
            public DateTime DataHoraCriacao { get; set; }
            public List<Endereco> Enderecos { get; set; }
        }
    }


