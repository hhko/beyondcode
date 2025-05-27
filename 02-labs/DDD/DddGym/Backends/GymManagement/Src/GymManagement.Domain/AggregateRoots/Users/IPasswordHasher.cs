namespace GymManagement.Domain.AggregateRoots.Users;

public interface IPasswordHasher
{
    FinT<IO, string> HashPassword(string password);

    FinT<IO, bool> IsCorrectPassword(string password, string bash);
}