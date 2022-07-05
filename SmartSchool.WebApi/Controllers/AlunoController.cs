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
        private readonly DataContext _context;
        public AlunoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        //api/aluno/byId/1
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.Where(b => b.Id == id).FirstOrDefault();
            if(aluno == null)
                return BadRequest("Aluno não encontrado");

            return Ok(aluno);
        }

        //api/aluno/byName?nome=Rafael&sobrenome=Marra
        [HttpGet("{byName}")]
        public IActionResult GetById(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.Where(b => b.Nome.Contains(nome) && b.Sobrenome.Contains(nome)).FirstOrDefault();
            if(aluno == null)
                return BadRequest("Aluno não encontrado");

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().Where(c => c.Id == id).FirstOrDefault();
            if(alu == null)
                return BadRequest("Aluno não encontrado!");

            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpPatch]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().Where(c => c.Id == id).FirstOrDefault();
            if(alu == null)
                return BadRequest("Aluno não encontrado!");

            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _context.Alunos.AsNoTracking().Where(c => c.Id == id).FirstOrDefault();
            if(aluno == null)
                return BadRequest("Aluno não encontrado!");
            _context.Remove(aluno);
            _context.SaveChanges();
            return Ok("Registro deletado com sucesso!");
        }

    }
}