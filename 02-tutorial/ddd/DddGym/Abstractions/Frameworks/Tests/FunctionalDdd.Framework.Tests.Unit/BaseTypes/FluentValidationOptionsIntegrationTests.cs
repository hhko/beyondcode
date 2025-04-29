using FluentValidation;
using FunctionalDdd.Framework.Options;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static FunctionalDdd.Framework.Tests.Unit.Abstractions.Constants.Constants;

namespace FunctionalDdd.Framework.Tests.Unit.BaseTypes;

[Trait(nameof(UnitTest), UnitTest.Framework)]
public class FluentValidationOptionsIntegrationTests
{
    public class ExampleOptions
    {
        public int Retries { get; set; }
    }

    public class ExampleOptionsValidator : AbstractValidator<ExampleOptions>
    {
        public ExampleOptionsValidator()
        {
            RuleFor(x => x.Retries)
                .InclusiveBetween(1, 9)
                .WithMessage("Retries는 1 이상 9 이하여야 합니다.");
        }
    }

    [Fact]
    public void InvalidOptions_ShouldThrowOptionsValidationException_OnStartup()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Example:Retries"] = "-1"
            })
            .Build();

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddConfigureOptions<ExampleOptions, ExampleOptionsValidator>("Example");

        // Act
        using var provider = services.BuildServiceProvider(validateScopes: true);

        // Assert
        var exception = Should.Throw<OptionsValidationException>(() =>
            provider
                .GetRequiredService<IOptions<ExampleOptions>>()
                .Value);

        //exception.Message.ShouldContain("Retries");
        //exception.Message.ShouldContain("1 이상 9 이하여야 합니다");
    }

    [Fact]
    public void ValidOptions_ShouldBindAndValidateSuccessfully()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Example:Retries"] = "5"
            })
            .Build();

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddConfigureOptions<ExampleOptions, ExampleOptionsValidator>("Example");

        // Act
        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<ExampleOptions>>().Value;

        // Assert
        options.Retries.ShouldBe(5);
    }
}

