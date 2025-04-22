

using LanguageExt;

namespace GymManagement.Domain.AggregateRoots.Users;

public interface IPasswordHasher
{
    public Fin<string> HashPassword(string password);

    bool IsCorrectPassword(string password, string bash);
}