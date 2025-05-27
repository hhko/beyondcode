using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Adapters.Infrastructure.Authentication.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    public FinT<IO, string> HashPassword(string password)
    {
        return lift(() => string.Empty);
    }

    public FinT<IO, bool> IsCorrectPassword(string password, string bash)
    {
        return lift(() => true);
    }
}