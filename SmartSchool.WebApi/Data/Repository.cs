using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;
         
        public Repository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public IEnumerable<Aluno> GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor)
            {
                query = query
                             .Include(a => a.AlunoDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(pd => pd.Professor);
            }

            query = query
                    .AsNoTracking()
                    .OrderBy(s => s.Id);

            return query.ToList();
        }

        public IEnumerable<Aluno> GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor)
                query = query.Include(a => a.AlunoDisciplinas).ThenInclude(ad => ad.Disciplina).ThenInclude(pd => pd.Professor);

            query = query.AsNoTracking().OrderBy(s => s.Id).Where(ad => ad.AlunoDisciplinas.Any(aa => aa.DisciplinaId == disciplinaId));

            return query.ToList();
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor)
                query = query.Include(a => a.AlunoDisciplinas).ThenInclude(ad => ad.Disciplina).ThenInclude(pd => pd.Professor);
            
            query = query.AsNoTracking().OrderBy(s => s.Id).Where(a => a.Id == alunoId);

            return query.FirstOrDefault();
        }

        public IEnumerable<Professor> GetAllProfessores(bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAlunos)
                query = query.Include(d => d.Disciplinas).ThenInclude(ad => ad.AlunoDisciplinas).ThenInclude(al => al.Aluno);
            
            query = query.AsNoTracking().OrderBy(s => s.Id);
            
            return query.ToList();
        }

        public IEnumerable<Professor> GetAllProferroesByDisciplinaId(int disciplinaId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAlunos)
                query = query.Include(d => d.Disciplinas).ThenInclude(ad => ad.AlunoDisciplinas).ThenInclude(a => a.Aluno);
            
            //query = query.AsNoTracking().OrderBy(s => s.Id).Where(d => d.Disciplinas.Any(dd => dd.Id == disciplinaId));

            query = query.AsNoTracking().OrderBy(s => s.Id)
                                        .Where(d => d.Disciplinas
                                        .Any(dd => dd.AlunoDisciplinas
                                        .Any(ad => ad.DisciplinaId == disciplinaId)));

            return query.ToList();
        }

        public Professor GetProfessorById(int professorId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAlunos)
                query = query.Include(d => d.Disciplinas).ThenInclude(da => da.AlunoDisciplinas).ThenInclude(a => a.Aluno);
            
            query = query.AsNoTracking().OrderBy(s => s.Id).Where(p => p.Id == professorId);

            return query.FirstOrDefault();
        }
    }
}