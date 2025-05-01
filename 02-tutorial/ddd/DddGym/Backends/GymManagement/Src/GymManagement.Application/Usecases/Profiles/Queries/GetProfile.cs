using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles.Queries;

public static class GetProfile
{
    //public sealed record Query(
    //    Guid UserId,
    //    string? Hello = null)
    //    //: IQuery2<Response>;

    public sealed record Query(
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

    internal sealed class Validator : AbstractValidator<Query>
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
        : IQueryUsecase2<Query, Response>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;

        public async Task<Fin<Response>> Handle(Query request, CancellationToken cancellationToken)
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
