using curso.api.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace curso.api.Configurations
{
    public class JwtService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string GenerateToken(UserViewModelOutput userViewModelOutput)
        {
            var secret = Encoding.ASCII.GetBytes(_configuration.GetSection(@"JwtConfigurations:Secret").Value);
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userViewModelOutput.Codigo.ToString()  ),
                    new Claim(ClaimTypes.Name, userViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, userViewModelOutput.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecytiryTokenHandler = new JwtSecurityTokenHandler();
            var tokenGeneretded = jwtSecytiryTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecytiryTokenHandler.WriteToken(tokenGeneretded);
        }
    }
}
