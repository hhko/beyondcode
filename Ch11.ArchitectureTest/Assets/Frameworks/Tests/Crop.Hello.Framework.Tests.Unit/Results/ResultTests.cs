using Crop.Hello.Framework.Contracts.Errors;
using Crop.Hello.Framework.Contracts.Results;
using FluentAssertions;
using static Crop.Hello.Framework.Tests.Unit.Abstractions.Constants.Constants;

namespace Crop.Hello.Framework.Tests.Unit.Results;

[Trait(nameof(UnitTest), UnitTest.Framework)]
public class ResultTests
{
    [Fact]
    public void TwoSuccessResults_ShouldReferTheSameCachedResultInstance_WhenTwoNonGenericSuccessResultsAreCreated()
    {
        // Arrange
        var firstResult = Result.Success();
        var secondResult = Result.Success();

        // Act
        var actual = ReferenceEquals(firstResult, secondResult);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void GettingValueFromGenericResult_ShouldThrowAnException_WhenResultIsFailureStringResult()
    {
        // Arrange
        var result = Result.Failure<string>(Error.ConditionNotSatisfied);
        string GetValueFromFailureResult() => result.Value;

        // Assert
        FluentActions
            .Invoking(GetValueFromFailureResult)
            .Should()
            .ThrowExactly<InvalidOperationException>()
            .Which
            .Message
            .Should()
            .Be("The value of a failure result can not be accessed. Type 'System.String'.");
    }

    [Fact]
    public void GettingValueFromGenericResult_ShouldThrowAnException_WhenResultIsFailureIntResult()
    {
        // Arrange
        var result = Result.Failure<int>(Error.ConditionNotSatisfied);
        var action = () => result.Value;

        // Assert
        action
            .Should()
            .ThrowExactly<InvalidOperationException>()
            .Which
            .Message
            .Should()
            .Be("The value of a failure result can not be accessed. Type 'System.Int32'.");
    }
}
