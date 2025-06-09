using GymManagement.Application.Abstractions.TokenGenerator;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Adapters.Infrastructure.Authentication.TokenGenerator;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        return string.Empty;
    }
}
