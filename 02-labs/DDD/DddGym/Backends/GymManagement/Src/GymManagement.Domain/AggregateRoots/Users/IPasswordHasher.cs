namespace GymManagement.Domain.AggregateRoots.Users;

public interface IPasswordHasher
{
    FinT<IO, string> HashPassword(string password);

    FinT<IO, Unit> IsCorrectPassword(string password, string bash);
    //bool IsCorrectPassword(string password, string bash);
}
