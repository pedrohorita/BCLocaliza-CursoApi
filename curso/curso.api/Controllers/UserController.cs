using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Configurations;
using curso.api.Filters;
using curso.api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace curso.api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
       
        private readonly IAuthenticationService _authenticationService;
        public UserController(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;            
            _authenticationService = authenticationService;
        }
        /// <summary>
        /// Este serviço permite autenticar um usuário cadastrado e ativo.
        /// </summary>
        /// <param name="loginViewModelInput">View model do login</param>
        /// <returns>Retorna status OK, dados do usuário e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type =  typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("login")]
        [ValidationModelStateCustom]
        public IActionResult Login(LoginViewModelInput loginViewModelInput)
        {
            User user = _userRepository.GetUser(loginViewModelInput.Login);

            if(user == null)
            {
                return BadRequest("Houve um erro ao tentar acessar.");
            }

            var userViewModelOutput = new UserViewModelOutput
            {
                Codigo = user.Codigo,
                Login = loginViewModelInput.Login,
                Email = user.Email
            };

            

            var token = _authenticationService.GenerateToken(userViewModelOutput);

            return Ok(new
            {
                Token = token,
                User = userViewModelOutput
            });
        }

        /// <summary>
        /// Este serviço permite cadastrar um usuário não existente.
        /// </summary>
        /// <param name="loginViewModelInput">View model do registro de login</param>
        /// <returns>Retorna status OK, dados do usuário e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("register")]
        [ValidationModelStateCustom]
        public IActionResult Register(RegisterViewModelInput registerViewModelInput)
        {
            var user = new User();
            user.Login = registerViewModelInput.Login;
            user.Senha = registerViewModelInput.Password;
            user.Email = registerViewModelInput.Email;
            _userRepository.Add(user);
            _userRepository.Commit();


            return Created("", registerViewModelInput);
        }
    }

}
