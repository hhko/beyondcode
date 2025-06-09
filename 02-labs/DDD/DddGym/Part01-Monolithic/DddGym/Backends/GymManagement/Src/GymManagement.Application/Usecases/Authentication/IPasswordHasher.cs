namespace GymManagement.Application.Usecases.Authentication;

public interface IPasswordHasher
{
    FinT<IO, string> HashPassword(string password);

    FinT<IO, Unit> IsCorrectPassword(string password, string bash);
}
