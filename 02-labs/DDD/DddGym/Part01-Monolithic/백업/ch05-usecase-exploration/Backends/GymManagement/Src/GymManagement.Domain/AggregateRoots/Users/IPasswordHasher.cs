using ErrorOr;

namespace GymManagement.Domain.AggregateRoots.Users;

public interface IPasswordHasher
{
    public ErrorOr<string> HashPassword(string password);

    bool IsCorrectPassword(string password, string bash);
}