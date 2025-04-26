using FunctionalDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles.Queries.GetProfiles;

internal sealed class GetProfileUsecase(IUsersRepository usersRepository)
    : IQueryUsecase2<GetProfile.Query, GetProfile.Response>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<Fin<GetProfile.Response>> Handle(GetProfile.Query request, CancellationToken cancellationToken)
    {
        Fin<User> result = await _usersRepository.GetByIdAsync(request.UserId);
        return result.ToGetProfileResponse();

        //return await 
        //(
        //    from user in liftIO(env => _usersRepository.GetByIdAsync(request.UserId))
        //    select user.ToResponse()
        //).RunAsync();
    }
}