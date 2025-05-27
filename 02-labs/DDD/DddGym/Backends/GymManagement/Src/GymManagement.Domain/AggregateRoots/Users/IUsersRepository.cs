namespace GymManagement.Domain.AggregateRoots.Users;

public interface IUsersRepository
{
    FinT<IO, Unit> AddUserAsync(User user);
    FinT<IO, bool> ExistsByEmailAsync(string email);
    FinT<IO, Option<User>> GetByEmailAsync(string email);
    FinT<IO, User> GetByIdAsync(Guid userId);
    FinT<IO, Unit> UpdateAsync(User user);
}