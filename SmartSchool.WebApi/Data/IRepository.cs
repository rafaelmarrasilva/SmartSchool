using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartSchool.WebApi.Helpers;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        //Alunos
        Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor);
        Task<IEnumerable<Aluno>> GetAllAlunosByDisciplinaIdAsync(int disciplinaId, bool includeProfessor);
        Task<Aluno> GetAlunoByIdAsync(int alunoId, bool includeProfessor);

        //Professor
        IEnumerable<Professor> GetAllProfessores(bool includeAlunos);
        IEnumerable<Professor> GetAllProferroesByDisciplinaId(int disciplinaId, bool includeAlunos);
        Professor GetProfessorById(int professorId, bool includeAlunos);

    }
}