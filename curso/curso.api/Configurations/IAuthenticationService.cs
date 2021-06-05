using curso.api.Models.Users;

namespace curso.api.Configurations
{
    public interface IAuthenticationService
    {
        string GenerateToken(UserViewModelOutput userViewModelOutput);
    }
}
