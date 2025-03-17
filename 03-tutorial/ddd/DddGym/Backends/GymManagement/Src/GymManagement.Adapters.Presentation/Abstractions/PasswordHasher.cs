using ErrorOr;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Adapters.Presentation.Abstractions;

public class PasswordHasher : IPasswordHasher
{
    public ErrorOr<string> HashPassword(string password)
    {
        return string.Empty;
    }

    public bool IsCorrectPassword(string password, string bash)
    {
        return true;
    }
}