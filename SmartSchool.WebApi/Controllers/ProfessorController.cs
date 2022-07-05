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
        private readonly DataContext _context;
        public ProfessorController(DataContext context)
        {
           _context = context; 
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var prof = _context.Professores.AsNoTracking().Where(a => a.Id == id).FirstOrDefault();
            if(prof == null)
                return BadRequest("Id não encontrado!");
            
            return Ok(prof);
        }

        //api/professor/byName?nome=Rafael
        [HttpGet("{byName}")]
        public IActionResult GetByName(string nome)
        {
            var prof = _context.Professores.AsNoTracking().Where(a => a.Nome.Contains(nome)).FirstOrDefault();
            if(prof == null)
                return BadRequest("Nome não encontrado!");
            return Ok(prof);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().Where(a => a.Id == id).FirstOrDefault();
            if(prof == null)
                return BadRequest("Id não encontrado!");
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().Where(a => a.Id == id).FirstOrDefault();
            if(prof == null)
                return BadRequest("Id não encontrado!");
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = _context.Professores.AsNoTracking().Where(a => a.Id == id).FirstOrDefault();
            if(prof == null)
                return BadRequest("Id não encontrado!");
            _context.Remove(prof);
            _context.SaveChanges();
            return Ok("Registro apagado com sucesso!");
        }

    }
}