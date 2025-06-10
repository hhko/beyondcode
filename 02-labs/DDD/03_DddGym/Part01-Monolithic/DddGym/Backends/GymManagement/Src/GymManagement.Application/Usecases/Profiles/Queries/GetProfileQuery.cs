using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;
using static GymManagement.Application.Usecases.Profiles.Errors.ApplicationErrors;

namespace GymManagement.Application.Usecases.Profiles.Queries;

public static class GetProfileQuery
{
    public sealed record Request(
        Guid UserId)
        : IQueryRequest<Response>;

    //public sealed record Request(
    //    Guid UserId)
    //    : ICachedQuery<Response>
    //{
    //    public string CacheKey => UserId.ToString();

    //    public TimeSpan? Duration => TimeSpan.FromMinutes(1);
    //}

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

    internal sealed class Usecase(IUsersRepository usersRepository)
        : IQueryUsecase<Request, Response>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;

        public async Task<Fin<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var usecase = from _ in _usersRepository.GetByIdAsync(request.UserId)
                                                    .ToRequiredOrError(GetProfileErrors.UserIdNotFound(request.UserId))
                          select _;

            return await usecase
                .ToGetProfileResponse();
        }
    }
}
