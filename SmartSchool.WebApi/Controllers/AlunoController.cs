using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno(){Id = 1, Nome = "Rafael", Sobrenome = "Marra", Telefone = "55557777"},
            new Aluno(){Id = 2, Nome = "Debora", Sobrenome = "Silva", Telefone = "66668888"},
            new Aluno(){Id = 3, Nome = "Enzo", Sobrenome = "Silva", Telefone = "22223333"},
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        //api/aluno/byId/1
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.Where(b => b.Id == id).FirstOrDefault();
            if(aluno == null)
                return BadRequest("Aluno não encontrado");

            return Ok(aluno);
        }

        //api/aluno/byName?nome=Rafael&sobrenome=Marra
        [HttpGet("{byName}")]
        public IActionResult GetById(string nome, string sobrenome)
        {
            var aluno = Alunos.Where(b => b.Nome == nome && b.Sobrenome == sobrenome).FirstOrDefault();
            if(aluno == null)
                return BadRequest("Aluno não encontrado");

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPatch]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
        

    }
}