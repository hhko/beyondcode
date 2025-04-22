using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;

namespace GymManagement.Adapters.Persistence.Repositories;

public class UsersRepository : IUsersRepository
{
    public Task AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Fin<User>> GetByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }
}