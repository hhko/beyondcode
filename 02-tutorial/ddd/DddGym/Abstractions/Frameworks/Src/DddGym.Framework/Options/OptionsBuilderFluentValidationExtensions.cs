using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DddGym.Framework.Options;

public static class OptionsBuilderFluentValidationExtensions
{
    ///// <summary>
    ///// Register this options instance for validation of its DataAnnotations.
    ///// </summary>
    ///// <typeparam name="TOptions">The options type to be configured.</typeparam>
    ///// <param name="optionsBuilder">The options builder to add the services to.</param>
    ///// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that additional calls can be chained.</returns>
    //[RequiresUnreferencedCode("Uses DataAnnotationValidateOptions which is unsafe given that the options type passed in when calling Validate cannot be statically analyzed so its" +
    //    " members may be trimmed.")]
    //public static OptionsBuilder<TOptions> ValidateDataAnnotations<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)] TOptions>(this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
    public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(x =>
            new FluentValidationOptions<TOptions>(
                optionsBuilder.Name,
                x.GetRequiredService<IValidator<TOptions>>()));
        return optionsBuilder;
    }
}