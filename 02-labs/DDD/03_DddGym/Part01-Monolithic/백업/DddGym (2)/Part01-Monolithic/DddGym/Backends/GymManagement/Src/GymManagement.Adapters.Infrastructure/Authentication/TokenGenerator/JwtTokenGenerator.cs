using GymManagement.Application.Abstractions.TokenGenerator;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Adapters.Infrastructure.Authentication.TokenGenerator;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public FinT<IO, string> GenerateToken(User user)
    {
        return IO.lift(() => string.Empty);
    }
}
