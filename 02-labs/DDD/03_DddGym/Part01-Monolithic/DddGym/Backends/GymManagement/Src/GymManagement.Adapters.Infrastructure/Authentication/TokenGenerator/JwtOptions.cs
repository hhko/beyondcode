using FluentValidation;

namespace GymManagement.Adapters.Infrastructure.Authentication.TokenGenerator;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public required string Audience { get; init; }
    public required string Issuer { get; init; }
    public required string Secret { get; init; }
    public int TokenExpirationInMinutes { get; init; }

    internal sealed class Validator : AbstractValidator<JwtOptions>
    {
        public Validator()
        {
            RuleFor(x => x.Audience).NotEmpty();
            RuleFor(x => x.Issuer).NotEmpty();
            RuleFor(x => x.Secret).NotEmpty().MinimumLength(32);
            RuleFor(x => x.TokenExpirationInMinutes).GreaterThan(0);
        }
    }
}