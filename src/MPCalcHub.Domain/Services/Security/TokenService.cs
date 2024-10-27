using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MPCalcHub.Domain.Entities;
using MPCalcHub.Domain.Interfaces.Security;
using MPCalcHub.Domain.Options;

namespace MPCalcHub.Domain.Services.Security;

public class TokenService(IOptions<TokenSettings> options) : ITokenService
{
    private readonly TokenSettings _settings = options.Value;

    public string GenerateToken(User user)
    {
        var jwtKey = _settings.Key;
        if (string.IsNullOrEmpty(jwtKey))
            throw new Exception("JWT Key is not configured.");

        var key = Encoding.ASCII.GetBytes(jwtKey);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, ((int)user.PermissionLevel).ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}