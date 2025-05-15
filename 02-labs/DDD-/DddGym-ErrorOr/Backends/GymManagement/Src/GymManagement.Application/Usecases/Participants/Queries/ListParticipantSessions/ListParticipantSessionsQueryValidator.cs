using FluentValidation;

namespace GymManagement.Application.Usecases.Participants.Queries.ListParticipantSessions;

internal sealed class ListParticipantSessionsQueryValidator : AbstractValidator<ListParticipantSessionsQuery>
{
    public ListParticipantSessionsQueryValidator()
    {
    }
}