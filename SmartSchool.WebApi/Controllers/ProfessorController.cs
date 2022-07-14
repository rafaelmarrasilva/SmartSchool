using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SmartSchool.WebApi.Dtos;

namespace SmartSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        public ProfessorController(IRepository repo, IMapper mapper)
        {
           _repo = repo;
           _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores(true);
            var result = _mapper.Map<IEnumerable<ProfessorDto>>(professores);
            return Ok(result);
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if(professor == null)
                return BadRequest("Id não encontrado!");
            
            var result = _mapper.Map<ProfessorDto>(professor);
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

        [HttpGet("getRegistrar")]
        public IActionResult getRegistrar()
        {
            return Ok(new ProfessorRegistrarDto());
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);
            _repo.Add(professor);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível gravar o registro.");
            
            return Created($"api/byId/{model.Id}", _mapper.Map<ProfessorDto>(professor));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id, false);
            if(professor == null)
                return BadRequest("Id não encontrado!");

            _mapper.Map(model,professor);

            _repo.Update(professor);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível atualizar o registro.");
            return Created($"api/byId/{model.Id}",_mapper.Map<ProfessorDto>(professor));
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id,false);
            if(professor == null)
                return BadRequest("Id não encontrado!");
            
            _mapper.Map(model,professor);

            _repo.Update(professor);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível atualizar o registro.");
            return Created($"api/byId/{model.Id}", _mapper.Map<ProfessorDto>(professor));
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