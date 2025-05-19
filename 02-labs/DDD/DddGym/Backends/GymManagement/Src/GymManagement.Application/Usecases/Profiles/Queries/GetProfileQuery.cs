using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles.Queries;

public static class GetProfileQuery
{
    //public sealed record Query(
    //    Guid UserId,
    //    string? Hello = null)
    //    //: IQuery2<Response>;

    public sealed record Request(
        Guid UserId)
        : ICachedQuery<Response>
    {
        public string CacheKey => UserId.ToString();

        public TimeSpan? Duration => TimeSpan.FromMinutes(1);
    }

    public sealed record Response(
        Option<Guid> AdminId,
        Option<Guid> ParticipantId,
        Option<Guid> TrainerId)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }

    internal sealed class Telemetry
    {

    }

    internal sealed class Usecase(IUsersRepository usersRepository)
        : IQueryUsecase<Request, Response>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;

        public async Task<Fin<Response>> Handle(Request request, CancellationToken cancellationToken)
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
}
