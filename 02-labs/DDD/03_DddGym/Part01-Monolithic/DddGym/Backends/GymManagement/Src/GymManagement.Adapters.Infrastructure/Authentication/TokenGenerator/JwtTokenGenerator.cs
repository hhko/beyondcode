using GymManagement.Application.Abstractions.TokenGenerator;
using GymManagement.Domain.AggregateRoots.Users;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;

namespace GymManagement.Adapters.Infrastructure.Authentication.TokenGenerator;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public FinT<IO, string> GenerateToken(User user)
    {
        return IO.lift(() => string.Empty);
    }

    //private readonly JwtSettings _jwtSettings;

    //public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
    //{
    //    _jwtSettings = jwtOptions.Value;
    //}

    //public string GenerateToken(User user)
    //{
    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
    //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //    var claims = new[]
    //    {
    //        new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
    //        new Claim(JwtRegisteredClaimNames.Email, user.Email),
    //        new Claim("id", user.Id.ToString()),
    //    };

    //    var token = new JwtSecurityToken(
    //        _jwtSettings.Issuer,
    //        _jwtSettings.Audience,
    //        claims,
    //        expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
    //        signingCredentials: credentials
    //    );

    //    return new JwtSecurityTokenHandler().WriteToken(token);
    //}
}
