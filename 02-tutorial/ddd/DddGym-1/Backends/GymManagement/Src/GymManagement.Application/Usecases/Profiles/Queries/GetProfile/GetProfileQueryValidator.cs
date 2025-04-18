using FluentValidation;

namespace GymManagement.Application.Usecases.Profiles.Queries.GetProfile;

internal sealed class GetProfileQueryValidator : AbstractValidator<GetProfileQuery>
{
    public GetProfileQueryValidator()
    {
    }
}