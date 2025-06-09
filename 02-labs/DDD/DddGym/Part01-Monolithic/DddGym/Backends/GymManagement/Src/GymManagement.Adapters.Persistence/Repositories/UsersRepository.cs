using Bogus;
using GymManagement.Application.Usecases.Profiles;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Adapters.Persistence.Repositories;

// -------------------------------------------------------------------------------------------------------
// Task 0개: lift(() =>                      실패 시 컴파일러 에러             
// Task 0개: IO.lift(() =>                   실패 시 예외 발생
// Task 1개: liftIO(() =>                    반환값 1개만 Task 사용, 명시적으로 async/await 사용하지 않음
// Task N개: IO.liftAsync(async () =>        <-- 기본
// -------------------------------------------------------------------------------------------------------

//public FinT<IO, User> GetByIdAsync(Guid userId)
//{
//      // ------------------
//      // 실패 때
//      // ------------------
//      return IO.liftAsync(async () =>
//      {
//          await Task.Delay(1000);
//      
//          // 성공 때: 성공값
//          // 실패 때: Fin<User>.Fail(에러 코드)
//          return Fin<User>.Fail(ErrorCodeFactory.Create("code1111", "message111111"));
//      });
//      
//      // ------------------
//      // 성공 때
//      // ------------------
//      return IO.liftAsync(async () =>
//      {
//          await Task.Delay(1000);
//      
//          // 성공 때: 성공값
//          // 실패 때: Fin<User>.Fail(에러 코드)
//          return User.Create("", "", "", "");
//      });

public class UsersRepository : IUsersRepository
{
    public FinT<IO, Unit> AddUserAsync(User user)
    {
        return lift(() => unit);
    }

    //public FinT<IO, bool> ExistsByEmailAsync(string email)
    //{
    //    return lift(() => true);
    //}

    public FinT<IO, Unit> ExistsByEmailAsync(string email)
    {
        return lift(() => unit);
    }

    public FinT<IO, Option<User>> GetByEmailAsync(string email)
    {
        return lift(() => Option<User>.None);
    }

    public FinT<IO, Option<User>> GetByIdAsync(Guid userId)
    {
        return IO.liftAsync(async () =>
        {
            await Task.CompletedTask;

            var userFaker = new Faker<User>()
                            .CustomInstantiator(f => User.Create(
                                firstName: f.Name.FirstName(),
                                lastName: f.Name.LastName(),
                                email: f.Internet.Email(),
                                passwordHash: f.Internet.Password()));

            return Option<User>.Some(userFaker.Generate());
        });
    }

    public Fin<User> Test()
    {
        throw new NotImplementedException();
    }

    public FinT<IO, Unit> UpdateAsync(User user)
    {
        return IO.liftAsync(async () =>
        {
            await Task.CompletedTask;
            return unit;
        });
    }
}

//public class UsersRepositoryIO : IUsersRepositoryIO
//{
//    public IO<User> GetByIdAsync(Guid userId)
//    {
//        return lift(() =>
//        {
//            var userFaker = new Faker<User>()
//                        .CustomInstantiator(f => User.Create(
//                            firstName: f.Name.FirstName(),
//                            lastName: f.Name.LastName(),
//                            email: f.Internet.Email(),
//                            passwordHash: f.Internet.Password()));

//            return userFaker.Generate();
//        });
//    }
//}
