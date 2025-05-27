using LanguageExt;

namespace GymManagement.Domain.AggregateRoots.Users;

public interface IUsersRepository
{
    Task AddUserAsync(User user);
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetByEmailAsync(string email);
    //Task<User?> GetByIdAsync(Guid userId);
    //Task<Fin<User>> GetByIdAsync(Guid userId);

    //Task UpdateAsync(User user);
    FinT<IO, User> GetByIdAsync(Guid userId);
    FinT<IO, Unit> UpdateAsync(User user);

    Fin<User> Test();
}