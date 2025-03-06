using FluentValidation;

namespace GymManagement.Application.Usecases.Users.Queries.ListProfiles;

internal sealed class GetProfileQueryValidator : AbstractValidator<GetProfileQuery>
{
    public GetProfileQueryValidator()
    {
    }
}