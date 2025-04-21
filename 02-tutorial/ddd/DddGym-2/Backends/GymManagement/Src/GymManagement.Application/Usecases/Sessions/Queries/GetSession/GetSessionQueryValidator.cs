using FluentValidation;

namespace GymManagement.Application.Usecases.Sessions.Queries.GetSession;

internal sealed class GetSessionQueryValidator : AbstractValidator<GetSessionQuery>
{
    public GetSessionQueryValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEmpty();

        RuleFor(x => x.SessionId)
            .NotEmpty();
    }
}