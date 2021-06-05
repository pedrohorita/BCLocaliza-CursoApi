using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Infraestruture.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDBContext _context;

        public CursoRepository(CursoDBContext context)
        {
            _context = context;
        }
        public void Add(Curso curso)
        {
            _context.Curso.Add(curso);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IList<Curso> GetByUser(int codigoUser)
        {
            return _context.Curso.Include(c => c.Usuario).Where(c => c.CodigoUsuario == codigoUser).ToList();
        }
    }
}
