using GymManagement.Adapters.Infrastructure.Abstractions.Options;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Adapters.Infrastructure;

[Trait(nameof(UnitTest), UnitTest.Infrastructure)]
public class ExampleOptionsTests
{
    private readonly ExampleOptions.Validator _validator = new();

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(9)]
    public void Validator_Should_Succeed_For_Valid_Retries(int retries)
    {
        // Arrange
        var options = new ExampleOptions
        {
            Retries = retries,
        };

        // Act
        var result = _validator.Validate(options);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(10)]
    public void Validator_Should_Fail_For_Invalid_Retries(int retries)
    {
        // Arrange
        var options = new ExampleOptions
        {
            Retries = retries,
        };

        // Act
        var result = _validator.Validate(options);

        // Assert
        result.IsValid.ShouldBeFalse();
    }
}
