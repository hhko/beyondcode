using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Abstractions.TokenGenerator;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
