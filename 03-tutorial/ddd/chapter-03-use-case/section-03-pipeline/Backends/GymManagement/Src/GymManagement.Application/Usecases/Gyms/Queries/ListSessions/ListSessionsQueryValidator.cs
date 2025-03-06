using FluentValidation;

namespace GymManagement.Application.Usecases.Gyms.Queries.ListSessionse;

internal sealed class ListSessionsQueryValidator : AbstractValidator<ListSessionsQuery>
{
    public ListSessionsQueryValidator()
    {
    }
}