using ErrorOr;

namespace DddGym.Domain.AggregateRoots.Users;

public interface IPasswordHasher
{
    public ErrorOr<string> HasPassword(string password);
    bool IsCorrectPassword(string password, string bash);
}
