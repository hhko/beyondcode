using GymManagement.Application.Abstractions.Tokens;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Adapters.Presentation.Abstractions.JwtToken;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        return string.Empty;
    }
}
