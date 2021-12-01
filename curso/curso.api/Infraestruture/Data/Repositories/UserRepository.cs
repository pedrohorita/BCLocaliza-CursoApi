using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Infraestruture.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CursoDBContext _context;

        public UserRepository(CursoDBContext context)
        {
            _context = context;
        }
        public void Add(User user)
        {
            _context.Usuario.Add(user);
            
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task<User> GetUserAsync(string login)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.Login == login);
        }
    }
}
