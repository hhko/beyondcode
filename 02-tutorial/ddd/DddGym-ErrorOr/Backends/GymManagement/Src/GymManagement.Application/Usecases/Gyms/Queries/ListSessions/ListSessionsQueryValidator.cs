using FluentValidation;

namespace GymManagement.Application.Usecases.Gyms.Queries.ListSessions;

internal sealed class ListSessionsQueryValidator : AbstractValidator<ListSessionsQuery>
{
    public ListSessionsQueryValidator()
    {
    }
}