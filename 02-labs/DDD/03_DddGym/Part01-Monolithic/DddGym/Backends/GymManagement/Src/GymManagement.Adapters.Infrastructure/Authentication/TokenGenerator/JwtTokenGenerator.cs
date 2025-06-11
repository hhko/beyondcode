using GymManagement.Application.Abstractions.TokenGenerator;
using GymManagement.Domain.AggregateRoots.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymManagement.Adapters.Infrastructure.Authentication.TokenGenerator;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public FinT<IO, string> GenerateToken(User user)
    {
        // 보안 키 생성: JWT 서명에 사용할 HMAC-SHA256 키를 생성합니다
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtOptions.Secret));

        // 서명 자격 생성
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        // 클레임 설정: 표준 클레임 이름 (Name, Email)을 사용하며, ID는 커스텀 클레임입니다
        //  - 클레임: JWT(JWT (JSON Web Token)의 Payload(본문)에 포함되며, 인증된 사용자에 대한 정보를 담는 데이터 조각입니다.
        //  - 클레임 역할
        //    - 인증(Authentication): 클레임을 통해 "누구인가?"를 식별
        //    - 인가(Authorization): role, permission 같은 클레임으로 권한 부여
        //    - 로깅, 감사(Logging / Auditing): 사용자의 행동 추적 및 보안 감사
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("id", user.Id.ToString()),
        };

        // JWT 토큰 생성
        var token = new JwtSecurityToken(
            // 토큰 발급자
            issuer: _jwtOptions.Issuer,
            // 토큰 수신자
            audience: _jwtOptions.Audience,
            // 클레임
            claims: claims,
            // 유효 시작 시간
            notBefore: DateTime.UtcNow,
            // 만료 시간
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.TokenExpirationInMinutes),
            // 서명 자격 정보
            signingCredentials: credentials
        );

        return Fin<string>.Succ(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
