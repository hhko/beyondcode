using Bogus;
using DddGym.Framework.BaseTypes;
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

    public async Task<Fin<User>> GetByIdAsync(Guid userId)
    {
        //await Task.Delay(3000);
        await Task.CompletedTask;

        var userFaker = new Faker<User>()
                        .CustomInstantiator(f => User.Create(
                            firstName: f.Name.FirstName(),
                            lastName: f.Name.LastName(),
                            email: f.Internet.Email(),
                            passwordHash: f.Internet.Password()));

        return userFaker.Generate();

        //return Fin<User>.Fail(ErrorCodeFactory.Create("나의 코드", "나의 메시지"));
    }

    public Fin<User> Test()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(User user)
    {
        //throw new NotImplementedException();
        await Task.CompletedTask;
    }
}