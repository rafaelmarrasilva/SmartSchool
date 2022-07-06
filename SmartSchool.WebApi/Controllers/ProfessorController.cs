using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        public ProfessorController(IRepository repo)
        {
           _repo = repo;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(result);
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _repo.GetProfessorById(id, false);
            if(result == null)
                return BadRequest("Id não encontrado!");
            
            return Ok(result);
        }

        /*
        //api/professor/byName?nome=Rafael
        [HttpGet("{byName}")]
        public IActionResult GetByName(string nome)
        {
            var prof = _context.Professores.AsNoTracking().Where(a => a.Nome.Contains(nome)).FirstOrDefault();
            if(prof == null)
                return BadRequest("Nome não encontrado!");
            return Ok(prof);
        }
        */

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível gravar o registro.");
            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var result = _repo.GetProfessorById(id, false);
            if(result == null)
                return BadRequest("Id não encontrado!");

            _repo.Update(professor);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível atualizar o registro.");
            return Ok(professor);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var result = _repo.GetProfessorById(id,false);
            if(result == null)
                return BadRequest("Id não encontrado!");
            
            _repo.Update(professor);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível atualizar o registro.");
            return Ok(professor);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _repo.GetProfessorById(id,false);
            if(result == null)
                return BadRequest("Id não encontrado!");
            
            _repo.Delete(result);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível apagar o registro.");
            return Ok("Registro apagado com sucesso!");
        }

    }
}