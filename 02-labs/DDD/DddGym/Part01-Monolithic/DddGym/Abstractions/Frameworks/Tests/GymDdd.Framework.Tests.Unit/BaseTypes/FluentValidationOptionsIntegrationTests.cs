using FluentValidation;
using GymDdd.Framework.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static GymDdd.Framework.Tests.Unit.Abstractions.Constants.Constants;

namespace GymDdd.Framework.Tests.Unit.BaseTypes;

[Trait(nameof(UnitTest), UnitTest.Framework)]
public class FluentValidationOptionsIntegrationTests
{
    public class ExampleOptions
    {
        public const string SectionName = "Example";

        public int Retries { get; set; }

        public class Validator : AbstractValidator<ExampleOptions>
        {
            public Validator()
            {
                RuleFor(x => x.Retries)
                    .InclusiveBetween(1, 9)
                    .WithMessage("Retries는 1 이상 9 이하여야 합니다.");
            }
        }
    }


    [Fact]
    public void Should_Throw_When_Options_Are_Invalid()
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
        services.AddConfigureOptions<ExampleOptions, ExampleOptions.Validator>(ExampleOptions.SectionName);

        // Act
        using var provider = services.BuildServiceProvider(validateScopes: true);

        // Assert
        var exception = Should.Throw<OptionsValidationException>(() =>
            provider
                .GetRequiredService<IOptions<ExampleOptions>>()
                .Value);

        exception.Message.ShouldContain($"{nameof(ExampleOptions)}.{nameof(ExampleOptions.Retries)}");
    }

    [Fact]
    public void Should_Bind_And_Pass_Validation_When_Options_Are_Valid()
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
        services.AddConfigureOptions<ExampleOptions, ExampleOptions.Validator>(ExampleOptions.SectionName);

        // Act
        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<ExampleOptions>>().Value;

        // Assert
        options.Retries.ShouldBe(5);
    }
}

