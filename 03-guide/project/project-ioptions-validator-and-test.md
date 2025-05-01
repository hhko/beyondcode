# 프로젝트 IOptions&lt;TOptions&gt; 유효성 검사 및 테스트

## 개요
- `FluentValidation`을 활용해 `IOptions<T>`의 유효성 검사를 전용 클래스로 분리하여 구성합니다.
- 이렇게 하면 설정 검증 로직이 명확해지고, 애플리케이션 시작 시점에 자동으로 검사가 수행되어 잘못된 설정을 조기에 감지할 수 있습니다.
  - `FluentValidation` 기반의 옵션 유효성 검사 구성
  - **`AddConfigureOptions<TOptions, TValidator>()`** 메서드를 통해 구성 및 등록 간소화
  - 앱 시작 시 자동으로 `ValidateOnStart()`를 통해 유효성 검사 수행

<br/>

## IOptions&lt;TOptions&gt; 사용
### 옵션 클래스 정의
```cs
public class ExampleOptions
{
    public const string SectionName = "Example";

    public int Retries { get; set; }
}
```

### FluentValidation 유효성 검사 정의
```cs
public class ExampleOptionsValidator : AbstractValidator<ExampleOptions>
{
    public ExampleOptionsValidator()
    {
        RuleFor(x => x.Retries)
            .InclusiveBetween(1, 9)
            .WithMessage("Retries는 1 이상 9 이하여야 합니다.");
    }
}
```

### 의존성 등록
```cs
builder
    .Services
    .AddConfigureOptions<
        ExampleOptions,                         // 옵션
        ExampleOptionsValidator>(               // 옵션 유효성 검사
            ExampleOptions.SectionName);        // 옵션 섹션 이름
```

### appsettings.json 설정
```json
{
  "Example": {
    "Retries": -1
  }
}
```

<br/>

## IOptions&lt;TOptions&gt; 단위 테스트
```cs

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
        services.AddConfigureOptions<ExampleOptions, ExampleOptionsValidator>("Example");

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
        services.AddConfigureOptions<ExampleOptions, ExampleOptionsValidator>("Example");

        // Act
        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<ExampleOptions>>().Value;

        // Assert
        options.Retries.ShouldBe(5);
    }
}
```

<br/>

## IOptions&lt;TOptions&gt; 아키텍처 테스트
- `Options` 접미사를 가진 설정 클래스은 `public const string SectionName` 필드 존재를 테스트합니다.

```cs
[Fact]
public void OptionsClasses_Should_Have_SectionName_ConstField()
{
    ArchRuleDefinition
        .Classes()
        .That()
        .HaveNameEndingWith("Options")
        .Should()
        .HaveSectionNameField()
        .Check(Architecture);
}
```

```cs
public static partial class ArchitectureUtilities
{
    public static TRuleTypeShouldConjunction HaveSectionNameField<TRuleTypeShouldConjunction, TRuleType>(
        this ObjectsShould<TRuleTypeShouldConjunction, TRuleType> should)
            where TRuleType : ICanBeAnalyzed
            where TRuleTypeShouldConjunction : SyntaxElement<TRuleType>
    {
        var condition = new HaveSectionNameFieldCondition<TRuleType>();
        return should.FollowCustomCondition(condition);
    }
}

internal sealed class HaveSectionNameFieldCondition<TRuleType>
    : ICondition<TRuleType>
      where TRuleType : ICanBeAnalyzed
{
    public string Description => "does not declare a public const string field named 'SectionName'";

    public IEnumerable<ConditionResult> Check(IEnumerable<TRuleType> objects, Architecture architecture)
    {
        foreach (var @class in objects)
        {
            Class? classObject = @class as Class;
            if (classObject == null)
            {
                string actualType = @class?.GetType().FullName ?? "null";
                string expectedType = typeof(Class).FullName ?? "null";

                throw new InvalidCastException($"Type cast failed: actual type is '{actualType}', expected type was '{expectedType}'.");
            }

            bool hasConstField = classObject
                .GetFieldMembers()
                .Any(f =>
                    f.Name == "SectionName" &&
                    f.Visibility == Visibility.Public &&
                    f.IsStatic == true &&
                    f.Type.FullName == typeof(string).FullName);

            yield return new ConditionResult(
                analyzedObject: classObject,
                pass: hasConstField,
                failDescription: hasConstField ? null : Description);
        }
    }

    public bool CheckEmpty() =>
        true;
}
```