using GymManagement.Application.Usecases.Authentication;
using System.Text.RegularExpressions;
using static GymManagement.Adapters.Infrastructure.Authentication.PasswordHasher.Errors.Infrastructure;

namespace GymManagement.Adapters.Infrastructure.Authentication.PasswordHasher;

public partial class PasswordHasher : IPasswordHasher
{
    // https://stackoverflow.com/a/34715674/10091553
    [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled)]
    private static partial Regex StrongPasswordRegex();

    private static readonly Regex PasswordRegex = StrongPasswordRegex();

    public FinT<IO, string> HashPassword(string password)
    {
        return !PasswordRegex.IsMatch(password)
                ? PasswordHasherErrors.PasswordWeak(password)
                : Fin<string>.Succ(BCrypt.Net.BCrypt.EnhancedHashPassword(password));
    }

    public FinT<IO, Unit> IsCorrectPassword(string password, string hash)
    {
        return !BCrypt.Net.BCrypt.EnhancedVerify(password, hash)
            ? PasswordHasherErrors.PasswordIncorrect()
            : Fin<Unit>.Succ(unit);
    }
}