using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MPCalcHub.Application.DataTransferObjects;
using MPCalcHub.Application.Interfaces;
using MPCalcHub.Domain.Interfaces;
using EN = MPCalcHub.Domain.Entities;

namespace MPCalcHub.Application.Services
{
    public class TokenApplicationService(IUserService userService, IConfiguration configuration) : ITokenApplicationService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUserService _userService = userService;

        public async Task<string> GetToken(UserLogin userLogin)
        {
            var user = await _userService.GetByEmail(userLogin.Email);

            if (user == null)
                throw new Exception("Usuário não encontrado");

            if (user.Password != userLogin.Password)
                throw new Exception("Senha inválida");

            return GenerateToken(user);
        }

        private string GenerateToken(EN.User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
                throw new Exception("JWT Key is not configured.");

            var key = Encoding.ASCII.GetBytes(jwtKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, (user.PermissionLevel -1).ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}