using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Crop.Hello.Framework.Options;

// Easily Validate the Options Pattern with FluentValidation
//  https://www.youtube.com/watch?v=I0YPTeCYvrE)
// Adding validation to strongly typed configuration objects using FluentValidation
//  https://andrewlock.net/adding-validation-to-strongly-typed-configuration-objects-using-flentvalidation/

// AddOptionsWithValidation                 // IServiceCollection
//  -> ValidateFluentValidation             // OptionsBuilder<TOptions>
//  -> FluentValidationOptions<TOptions>

internal class FluentValidationOptions<TOptions>(
    string? name,
    IServiceProvider serviceProvider)
    : IValidateOptions<TOptions>
    where TOptions : class
{
    // we need the service provider to create a scope later
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    // Handle named options
    private readonly string? _name = name;

    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        // Null name is used to configure all named options.
        if (_name != null && _name != name)
        {
            // Ignored if not validating this instance.
            return ValidateOptionsResult.Skip;
        }

        // Ensure options are provided to validate against
        ArgumentNullException.ThrowIfNull(options);

        // Validators are typically registered as scoped,
        // so we need to create a scope to be safe, as this
        // method is be called from the root scope
        using IServiceScope scope = _serviceProvider.CreateScope();

        // retrieve an instance of the validator
        var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

        // Run the validation
        ValidationResult results = validator.Validate(options);
        if (results.IsValid)
        {
            // All good!
            return ValidateOptionsResult.Success;
        }

        // Validation failed, so build the error message
        string typeName = options.GetType().Name;
        var errors = new List<string>();
        foreach (var result in results.Errors)
        {
            // Microsoft.Extensions.Options.OptionsValidationException:
            //      Options validation failed for 'TestOptions.Name' with the error:
            //          ''Name'은(는) 최소한 한 글자 이상이어야 합니다.'.
            //
            // typeName             : 클래스 이름, 예: TestOptions
            // result.PropertyName  : 멤버 속성, 예. Name
            // result.ErrorMessage  : Fluent Validation 예외 로그
            errors.Add($"Options validation failed for '{typeName}.{result.PropertyName}' with the error: '{result.ErrorMessage}'.");
        }

        return ValidateOptionsResult.Fail(errors);
    }
}
