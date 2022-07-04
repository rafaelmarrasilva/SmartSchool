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
    public class ProfessorController : ControllerBase
    {
        public List<Professor> Professores;
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Sucesso no teste do Professor.");
        }
    }
}