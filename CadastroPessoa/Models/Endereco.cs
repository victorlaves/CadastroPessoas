using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroPessoa.Models
{
    public class Endereco
    {
        [Key]
        public string Codigo { get; set; }


        [ForeignKey("Pessoa")]  
        public string CodigoPessoa { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public Pessoa Pessoa { get; set; } 
    }


}
