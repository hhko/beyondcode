# 옵션 유효성 검사 (appsettings.json, IOption&lt;T&gt;)

## 개요
- `FluentValidation`을 활용해 `IOptions<T>`의 유효성 검사를 전용 클래스로 분리하여 구성합니다.
- 이렇게 하면 설정 검증 로직이 명확해지고, 애플리케이션 시작 시점에 자동으로 검사가 수행되어 잘못된 설정을 조기에 감지할 수 있습니다.
  - `FluentValidation` 기반의 옵션 유효성 검사 구성
  - **`AddConfigureOptions<TOptions, TValidator>()`** 메서드를 통해 의존성 등록 간소화
  - `ValidateOnStart()`를 통해 앱 시작 시 자동으로 유효성 검사 수행

<br/>

## 옵션 클래스

- `Options`: 옵션 클래스 접미사
- `SectionName`: 옵션 섹션 이름 (appsettings.json)
- `Validator`: 옵션 유효성 검사 클래스

### 옵션 클래스 정의
```cs
public class ExampleOptions
{
    // 옵션 섹션 이름
    public const string SectionName = "Example";

    // 옵션
    public int Retries { get; set; }

    // 옵션 유효성 검사
    internal class Validator : AbstractValidator<ExampleOptions>
    {
        public Validator()
        {
            RuleFor(x => x.Retries)
                .InclusiveBetween(1, 9)
                .WithMessage("Retries는 1 이상 9 이하여야 합니다.");
        }
    }
}
```

### 옵션 클래스 의존성 등록
```cs
builder.Services
    .AddConfigureOptions<                       // 옵션 유효성 검사를 위한 확장 메서드
        ExampleOptions,                         // 옵션 클래스
        ExampleOptions.Validator>(              // 옵션 유효성 검사 클래스
            ExampleOptions.SectionName);        // 옵션 섹션 이름
```

### appsettings.json 옵션
```json
{
  "Example": {
    "Retries": -1
  }
}
```

<br/>

## 옵션 클래스 단위 테스트
```cs
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
```

<br/>

## 옵션 클래스 아키텍처 테스트
- `Options` 접미사를 가진 설정 클래스는 `public const string SectionName` 필드를 포함해야 합니다.
- `Options` 접미사를 가진 설정 클래스는 `Validator`라는 이름의 중첩 클래스(nested class)를 포함해야 합니다.

```cs
[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class OptionsRuleTests : ArchitectureTestBase
{
    [Fact]
    public void OptionsClasses_ShouldSatisfy_DesignRules()
    {
        // Arrange
        var provider = ArchRuleDefinition
            .Classes()
            .That()
            .HaveNameEndingWith(NamingConvention.Options);

        List<IArchitectureRule> sut = [
            // public sealed class {옵션}Options
            provider.Should().BePublic()
                .AndShould().BeSealed()
                .AndShould().NotBeNested()
                .ToArchitectureRule(),

            // public const string SectionName
            Must.HaveNamedFieldMatches(provider,
                (NamingConvention.SectionName, field => Must.IsPublicStaticField(field, typeof(string)))
            ),

            // internal sealed class Validator 중첩 클래스
            Must.HaveNamedNestedClassMatches(provider, (NamingConvention.Validator, Must.IsNestedInternalSealed))
        ];

        // Act
        var actual = sut.Evaluate(ServiceArchitecture);

        // Assert
        actual.ShouldHaveNoViolationRules();
    }
}
```
