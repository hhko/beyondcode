using FluentValidation;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.Quartz;


internal sealed class JobOptionsValidator : AbstractValidator<JobOptions>
{
    public JobOptionsValidator()
    {
        RuleFor(x => x.CronSchedule)
            .NotEmpty();
        //RuleFor(x => x.WebhookUrl)
        //    .NotEmpty()
        //    // .MustAsync((_, _) => Task.FromResult(true)) 👈 can't use async validators
        //    .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
        //    .When(x => !string.IsNullOrEmpty(x.WebhookUrl));
    }
}
