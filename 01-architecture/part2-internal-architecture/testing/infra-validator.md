# 유효성 검사 테스트

## 단위 테스트

- TOptions의 Validator 클래스를 직접 생성해서 테스트합니다.

```cs
[Trait(nameof(UnitTest), UnitTest.Infrastructure)]
public class ExampleOptionsTests
{
    // 유효성 검사 테스트 대상
    private readonly ExampleOptionsValidator _sut = new();

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
        var actual = _sut.Validate(options);

        // Assert
        actual.IsValid.ShouldBeTrue();
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
        var actual = _sut.Validate(options);

        // Assert
        actual.IsValid.ShouldBeFalse();
    }
}
```

## 통합 테스트
- TODO

