using CadastroPessoa.Context;
using CadastroPessoa.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoa.Controllers
{
    [ApiController]
    [Route("enderecos")]
    public class EnderecoController : ControllerBase
    {
        private readonly CadastroContext _cadastroContext;
        public EnderecoController(CadastroContext cadastroContext) =>
            _cadastroContext = cadastroContext;

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Endereco endereco)
        {
            if (endereco == null)
                return BadRequest("Dados nao informado.");

            endereco.Codigo = Guid.NewGuid().ToString();
            _cadastroContext.Endereco.Add(endereco);
            _cadastroContext.SaveChanges();
            return Ok(endereco);
        }

        [HttpDelete]
        [Route("excluir/{codigo}")]
        public IActionResult Excluir([FromRoute] string codigo)
      {
            if(string.IsNullOrEmpty(codigo))
             return BadRequest("Codigo nao informado");

        var endereco = _cadastroContext.Endereco.Where(e => e.Codigo ==  codigo).FirstOrDefault();

        if (endereco == null)
            return BadRequest("Endereco nao existe.");

        _cadastroContext.Remove(endereco);
        _cadastroContext.SaveChanges();
            return Ok ();

        }
    }
}
