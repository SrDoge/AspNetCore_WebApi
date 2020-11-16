using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApnCore_CrudExemplo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AspNetCore_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioDAL _FuncionarioDAL;
        public FuncionarioController(IFuncionarioDAL tarefaRepositorio)
        {
            _FuncionarioDAL = tarefaRepositorio;
        }
        [HttpGet]
        public IEnumerable<Funcionario> GetAllFuncionarios()
        {
            return _FuncionarioDAL.GetAllFuncionarios();
        }

        [HttpGet("{id}", Name = "GetFuncionario")]
        public IActionResult GetById(long id)
        {
            var item = _FuncionarioDAL.GetFuncionario(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Funcionario item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _FuncionarioDAL.AddFuncionario(item);
            return CreatedAtRoute("GetFuncionario", new { id = item.FuncionarioId }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Funcionario item)
        {
            if (item == null || item.FuncionarioId != id)
            {
                return BadRequest();
            }

            var funcionario = _FuncionarioDAL.GetFuncionario(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            funcionario.Nome = item.Nome;
            funcionario.Cidade = item.Cidade;
            funcionario.Departamento = item.Departamento;
            funcionario.Sexo = item.Sexo;

            _FuncionarioDAL.UpdateFuncionario(funcionario);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _FuncionarioDAL.GetFuncionario(id);
            if (todo == null)
            {
                return NotFound();
            }
            _FuncionarioDAL.DeleteFuncionario(id);
            return new NoContentResult();
        }
    }
}
