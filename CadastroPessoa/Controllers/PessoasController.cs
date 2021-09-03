using CadastroPessoa.Context;
using CadastroPessoa.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using CadastroPessoa.Utils;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CadastroPessoa.Controllers
{
    [ApiController]
    [Route("pessoas")]
    public class PessoasController : ControllerBase
    {
        private readonly CadastroContext _cadastroContext;
        public PessoasController(CadastroContext cadastroContext) =>
            _cadastroContext = cadastroContext;

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Pessoa pessoa)
        {
            if (pessoa == null)
                return BadRequest("Dados Nao informados.");

            pessoa.Codigo = Guid.NewGuid().ToString();
            pessoa.Ativo = true;
            pessoa.DataHoraCriacao = DateTime.Now;

            if (pessoa.Tipo == TipoPessoa.Fisica)
            {
                var cpfValido = Validador.ValidarCpf(pessoa.CpfCnpj);

                if (!cpfValido)
                    return BadRequest("CPF e invalido");
            }

            if (pessoa.Tipo == TipoPessoa.Juridica)
            {
                var CnpjValido = Validador.ValidarCnpj(pessoa.CpfCnpj);

                if (!CnpjValido)
                    return BadRequest("CPF e invalido");
            }

            _cadastroContext.Pessoa.Add(pessoa);
            _cadastroContext.SaveChanges();
            return Ok(pessoa);
        }

        [HttpPut]
        [Route("alterar")]
        public IActionResult Alterar([FromBody] Pessoa pessoa)
        {
            if (pessoa == null)
                return BadRequest("Dados Nao informados.");

            if (string.IsNullOrEmpty(pessoa.Nome))
                return BadRequest("O nome e obrigatorio");

            var pessoaParaAlterar = _cadastroContext.Pessoa.Where(pessoa => pessoa.Codigo == pessoa.Codigo).FirstOrDefault();

            if (pessoaParaAlterar == null)
                return BadRequest("A pessoa nao existe");

            pessoaParaAlterar.Nome = pessoa.Nome;
            _cadastroContext.Pessoa.Update(pessoaParaAlterar);
            _cadastroContext.SaveChanges();
            return Ok(pessoaParaAlterar);
        }

        [HttpDelete]
        [Route("excluir/{codigo}")]
        public IActionResult Excluir([FromRoute] string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
                return BadRequest("Codigo nao informado"); 

            var pessoa = _cadastroContext.Pessoa.Where(pessoa => pessoa.Codigo == codigo).FirstOrDefault();
            
            if (pessoa == null)
                return BadRequest("A pessoa nao existe.");

            pessoa.Ativo = false;
            _cadastroContext.Pessoa.Update(pessoa);
            _cadastroContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IActionResult ObterTodas()
        {
            var pessoas = _cadastroContext.Pessoa.Include(pessoas => pessoas.Enderecos).Where(pessoa => pessoa.Ativo).ToList();
            return Ok(pessoas);
        }
    }
}
