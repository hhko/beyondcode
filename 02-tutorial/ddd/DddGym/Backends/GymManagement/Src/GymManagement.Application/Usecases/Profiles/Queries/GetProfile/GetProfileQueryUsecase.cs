using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace GymManagement.Application.Usecases.Profiles.Queries.GetProfile;

// TODO: LanguageExt
internal sealed class GetProfileQueryUsecase(IUsersRepository usersRepository)
    : IQueryUsecase2<GetProfileQuery, GetProfileResponse>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<Fin<GetProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        //Fin<User> user = await _usersRepository.GetByIdAsync(request.UserId);
        //return user.Map(GetProfileResponse.Create);
        //var result = from user in FinT<Task, User>.LiftIO(_usersRepository.GetByIdAsync(request.UserId))
        //             select GetProfileResponse.Create(user);
        //return result.Run();

        Fin<User> userResult = await _usersRepository.GetByIdAsync(request.UserId);
        if (userResult.IsSucc)
            return new GetProfileResponse(
                ((User)userResult).AdminId,
                ((User)userResult).ParticipantId,
                Guid.NewGuid());
        //((User)userResult).TrainerId);
        else
            return (Error)userResult;
        //return from user in await _usersRepository.GetByIdAsync(request.UserId)
        //       select GetProfileResponse.Create(user);

        //return await 
        //(
        //    from user in liftIO(env => _usersRepository.GetByIdAsync(request.UserId))
        //    select user.ToResponse()
        //).RunAsync();
    }

    //public async Task<IErrorOr<GetProfileResponse>> Handle(GetProfileQuery query, CancellationToken cancellationToken)
    //{
    //    User? user = await _usersRepository.GetByIdAsync(query.UserId);
    //    if (user is null)
    //    {
    //        return Error
    //            .NotFound(description: "User not found")
    //            .ToErrorOr<GetProfileResponse>();
    //    }

    //    return user
    //        .ToResponse()
    //        .ToErrorOr();
    //}
}