using Crop.Hello.Framework.Contracts.Errors;
using Crop.Hello.Framework.Contracts.Results;
using FluentAssertions;
using static Crop.Hello.Framework.Tests.Unit.Abstractions.Constants.Constants;

namespace Crop.Hello.Framework.Tests.Unit.Results;

[Trait(nameof(UnitTest), UnitTest.Framework)]
public class ResultTests
{
    // 값이 없는 성공은 메모리 효율성을 위해 단일 인스턴스로 관리합니다.
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

    // 값이 있는 실패일 때는 값을 참조할 수 없습니다.
    //   - 값이 있는 실패일 때는 default 값(NULL)로 저장됩니다.
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

    // 값이 있는 실패일 때는 값을 참조할 수 없습니다.
    //   - 값이 있는 실패일 때는 default 값(NULL)로 저장됩니다.
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
