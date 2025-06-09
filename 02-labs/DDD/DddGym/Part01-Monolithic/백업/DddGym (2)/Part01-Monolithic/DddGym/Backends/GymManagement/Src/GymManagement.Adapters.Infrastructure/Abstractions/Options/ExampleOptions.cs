using FluentValidation;

namespace GymManagement.Adapters.Infrastructure.Abstractions.Options;

public sealed class ExampleOptions
{
    public const string SectionName = "Example";

    public required int Retries { get; init; }

    internal sealed class Validator : AbstractValidator<ExampleOptions>
    {
        public Validator()
        {
            RuleFor(x => x.Retries)
                .InclusiveBetween(1, 9);
        }
    }
}

//public sealed class XyzOptions
//{

//}
