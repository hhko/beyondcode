using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;

namespace GymManagement.Adapters.Infrastructure.Authentication.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    public Fin<string> HashPassword(string password)
    {
        return string.Empty;
    }

    public bool IsCorrectPassword(string password, string bash)
    {
        return true;
    }
}