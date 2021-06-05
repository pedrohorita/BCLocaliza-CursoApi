using curso.api.Business.Entities;
using System.Collections.Generic;

namespace curso.api.Business.Repositories
{
    public interface ICursoRepository
    {
        void Add(Curso curso);
        void Commit();
        IList<Curso> GetByUser(int codigoUser);
    }
}
