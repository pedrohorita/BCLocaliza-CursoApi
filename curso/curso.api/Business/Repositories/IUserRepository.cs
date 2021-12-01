using curso.api.Business.Entities;
using System.Threading.Tasks;

namespace curso.api.Business.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Commit();
        Task<User> GetUserAsync(string login);
    }
}
