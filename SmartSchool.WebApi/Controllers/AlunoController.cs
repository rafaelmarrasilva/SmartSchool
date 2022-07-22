using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Dtos;
using SmartSchool.WebApi.Helpers;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Controllers
{
    ///<summary>
    ///
    ///</summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        ///<summary>
        ///
        ///</summary>
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        ///<summary>
        ///Método responsável para retornar todos os meus alunos
        ///</summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);
            
            var result = _mapper.Map<IEnumerable<AlunoDto>>(alunos);
            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);
            return Ok(result);
        }
        
         [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
        return Ok(new AlunoRegistrarDto());
        }

        ///<summary>
        ///Método responsável por retornar apenas um único aluno, por meio do Id
        ///</summary>
        //api/aluno/byId/1
        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aluno = await _repo.GetAlunoByIdAsync(id,false);
            if(aluno == null)
                return BadRequest("Aluno não encontrado");

            var result = _mapper.Map<AlunoDto>(aluno);
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
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            _repo.Add(aluno);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível gravar o registro.");
            
            return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AlunoRegistrarDto model)
        {
            var aluno = await _repo.GetAlunoByIdAsync(id,false);
            if(aluno == null)
                return BadRequest("Aluno não encontrado!");

            _mapper.Map(model,aluno);

            _repo.Update(aluno);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível atualizar o registro.");
            
            return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = await _repo.GetAlunoByIdAsync(id,false);
            if(aluno == null)
                return BadRequest("Aluno não encontrado!");

            _mapper.Map(model,aluno);

            _repo.Update(aluno);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível atualizar o registro.");
            
            return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.GetAlunoByIdAsync(id,false);
            if(result == null)
                return BadRequest("Aluno não encontrado!");
            
            _repo.Delete(result);
            if(!_repo.SaveChanges())
                return BadRequest("Não foi possível apagar o registro.");
            return Ok("Registro deletado com sucesso!");
        }

    }
}