using CadastroPessoa.Models;
using Microsoft.EntityFrameworkCore;


namespace CadastroPessoa.Context
{
    public class CadastroContext : DbContext
    {

        public CadastroContext(DbContextOptions<CadastroContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
     
    }


}

