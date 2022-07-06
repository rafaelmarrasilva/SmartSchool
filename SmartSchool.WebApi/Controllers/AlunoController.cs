using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        public AlunoController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(result);
        }

        //api/aluno/byId/1
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _repo.GetAlunoById(id,false);
            if(result == null)
                return BadRequest("Aluno não encontrado");

            return Ok(result);
        }

        /*
        //api/aluno/byName?nome=Rafael&sobrenome=Marra
        [HttpGet("{byName}")]
        public IActionResult GetById(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.Where(b => b.Nome.Contains(nome) && b.Sobrenome.Contains(nome)).FirstOrDefault();
            if(aluno == null)
                return BadRequest("Aluno não encontrado");

            return Ok(aluno);
        }
        */

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível gravar o registro.");
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var result = _repo.GetAlunoById(id,false);
            if(result == null)
                return BadRequest("Aluno não encontrado!");

            _repo.Update(aluno);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível atualizar o registro.");
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var result = _repo.GetAlunoById(id,false);
            if(result == null)
                return BadRequest("Aluno não encontrado!");

            _repo.Update(aluno);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível atualizar o registro.");
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _repo.GetAlunoById(id,false);
            if(result == null)
                return BadRequest("Aluno não encontrado!");
            
            _repo.Delete(result);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível apagar o registro.");
            return Ok("Registro deletado com sucesso!");
        }

    }
}