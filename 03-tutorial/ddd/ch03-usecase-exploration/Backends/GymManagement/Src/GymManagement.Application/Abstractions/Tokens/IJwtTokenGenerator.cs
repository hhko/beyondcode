using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Abstractions.Tokens;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
