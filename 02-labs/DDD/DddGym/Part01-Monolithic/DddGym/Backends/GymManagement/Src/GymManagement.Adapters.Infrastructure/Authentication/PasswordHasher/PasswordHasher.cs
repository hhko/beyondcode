using GymManagement.Application.Usecases.Authentication;

namespace GymManagement.Adapters.Infrastructure.Authentication.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    public FinT<IO, string> HashPassword(string password)
    {
        return lift(() => string.Empty);
    }

    public FinT<IO, Unit> IsCorrectPassword(string password, string bash)
    {
        return lift(() => unit);
    }
}