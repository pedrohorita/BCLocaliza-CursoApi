using curso.api.Business.Entities;
using curso.api.Models.Users;

namespace curso.api.Business.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Commit();
        User GetUser(string login);
    }
}
