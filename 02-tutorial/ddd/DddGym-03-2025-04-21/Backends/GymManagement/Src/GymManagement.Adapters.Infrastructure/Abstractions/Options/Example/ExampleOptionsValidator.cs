using FluentValidation;

namespace GymManagement.Adapters.Infrastructure.Abstractions.Options.Example;

internal sealed class ExampleOptionsValidator : AbstractValidator<ExampleOptions>
{
    public ExampleOptionsValidator()
    {
        RuleFor(x => x.Retries)
            .InclusiveBetween(1, 9);
    }
}

//public sealed class ExampleOptionValidator : AbstractValidator<ExampleOptions>
//{
//    public ExampleOptionValidator()
//    {
//        RuleFor(x => x.Retries)
//            .InclusiveBetween(1, 9);
//    }
//}