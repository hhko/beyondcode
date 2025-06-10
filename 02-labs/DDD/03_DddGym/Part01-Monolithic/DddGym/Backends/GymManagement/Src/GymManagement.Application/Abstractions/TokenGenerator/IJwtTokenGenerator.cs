using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Abstractions.TokenGenerator;

public interface IJwtTokenGenerator
{
    FinT<IO, string> GenerateToken(User user);
}
