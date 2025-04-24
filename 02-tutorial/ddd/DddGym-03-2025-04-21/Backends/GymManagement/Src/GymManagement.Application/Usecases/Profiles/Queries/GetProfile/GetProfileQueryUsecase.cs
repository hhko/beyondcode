using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;

namespace GymManagement.Application.Usecases.Profiles.Queries.GetProfile;

// TODO: LanguageExt
internal sealed class GetProfileQueryUsecase
    : IQueryUsecase2<GetProfileQuery, GetProfileResponse>
{
    private readonly IUsersRepository _usersRepository;

    public GetProfileQueryUsecase(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
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

    public async Task<Fin<GetProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        return await 
        (
            from user in liftIO(env => _usersRepository.GetByIdAsync(request.UserId))
            select user.ToResponse()
        ).RunAsync();
    }
}