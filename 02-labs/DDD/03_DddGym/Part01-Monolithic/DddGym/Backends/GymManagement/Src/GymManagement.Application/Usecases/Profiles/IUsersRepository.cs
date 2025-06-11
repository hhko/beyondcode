using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Profiles;

public interface IUsersRepository
{
    FinT<IO, Unit> AddUserAsync(User user);
    FinT<IO, User> GetByEmailAsync(string email);
    FinT<IO, User> GetByIdAsync(Guid userId);
    FinT<IO, Unit> UpdateAsync(User user);

    //FinT<IO, bool> ExistsByEmailAsync(string email);
    FinT<IO, Unit> ExistsByEmailAsync(string email);
}