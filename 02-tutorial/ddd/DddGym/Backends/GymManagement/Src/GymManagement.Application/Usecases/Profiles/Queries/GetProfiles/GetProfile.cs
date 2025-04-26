using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles.Queries.GetProfiles;

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
}
