---
outline: deep
---

# 네이밍컨벤션

```cs
public static partial class Constants
{
    public static class NamingConvention
    {
        // CQRS
        public const string Command = nameof(Command);
        public const string CommandUsecase = nameof(CommandUsecase);
        public const string Query = nameof(Query);
        public const string QueryUsecase = nameof(QueryUsecase);

        public const string ValidatorSuffix = ".*(Command|Query|Options)Validator$";
    }
}
```

## C# 네이밍컨벤션
### 규칙
- 인터페이스는 `^I[A-Z].*`로 시작한다.
- 메서드는 대문자로 시작한다.

### 테스트
```cs
[Fact]
public void Interfaces_ShouldStartWith_I()
{
    IEnumerable<Interface> sut = ArchRuleDefinition
        .Interfaces()
        .GetObjects(Architecture);

    if (!sut.Any())
        return;

    sut.ShouldAllBe(i => Regex.IsMatch(i.Name, "^I[A-Z].*"));
}

[Fact]
public void AllMethods_ShouldStartWith_CapitalLetter()
{
    ArchRuleDefinition
        .MethodMembers()
        
        // 제외
        .That().DoNotHaveNameStartingWith(".ctor")      // 클래스 생성자
        .And().DoNotHaveNameStartingWith(".cctor")      // 정적 클래스 생성자
        .And().DoNotHaveNameStartingWith("get_")        // property
        .And().DoNotHaveNameStartingWith("set_")        // property
        .And().DoNotHaveNameStartingWith("op_")         // 연산자 오버로딩: 예. + op_Addition, == op_Equality, ...

        // 순수 메서드
        .Should().HaveName(@"^[A-Z]", true)
        .Check(Architecture);
}
```

## Validator 네이밍컨벤션
### 규칙
- IValidator<T> 을 상속받는 모든 클래스는 internal sealed 이어야 한다.
- 클래스 이름은 반드시 Validator 접미사를 가져야 한다.

### 테스트
```cs
[Fact]
public void ValidatorClasses_ShouldBe_InternalSealed_And_Have_ValidatorSuffix()
{
    var sut = ArchRuleDefinition
        .Classes()
        .That()
        .ImplementInterface(typeof(IValidator<>));
    
    if (!sut.GetObjects(Architecture).Any())
        return;

    sut.Should().BeInternal()
        .AndShould().BeSealed()
        .AndShould().HaveName(NamingConvention.ValidatorSuffix, true)
        .Check(Architecture);
}
```

## Options 네이밍컨벤션

### 규칙
- "OptionsValidator : Options = 1 : 1" 관계이다.
- Options 클래스는 public sealed 클래스이다.
- public const string SectionName 필드를 갖는다.

### 테스트
```cs
[Fact]
public void OptionsClasses_ShouldBe_Sealed_And_Have_SectionName()
{
    // Arrange
    var optionsValidatorClasses = ArchRuleDefinition
        .Classes()
        .That().ImplementInterface(typeof(IValidator<>))
        .And().HaveNameEndingWith("OptionsValidator")
        .GetObjects(Architecture);

    foreach (var optionsValidatorClass in optionsValidatorClasses)
    {
        // Act
        Class? optionsClass = GetCorrespondingOptionsClass(optionsValidatorClass);

        // Assert
        //  - public sealed 클래스
        //  - public const string SectionName
        //    - TODO: 값 예. XyzOption -> Xyz
        optionsClass
            .ShouldNotBeNull($"Corresponding Options class for '{optionsValidatorClass.FullName}' not found.");
        optionsClass
            .Visibility.ShouldBe(Visibility.Public, $"Options class '{optionsClass.FullName}' should be public.");
        optionsClass
            .IsSealed!.Value.ShouldBeTrue($"Options class '{optionsClass.FullName}' should be sealed.");
        optionsClass
            .GetFieldMembers()
            .ShouldContain(field =>
                field.Name == "SectionName" &&
                    field.Visibility == Visibility.Public &&
                    field.IsStatic.Value &&
                    field.Type.FullName == "System.String",
                $"Options class '{optionsClass.FullName}' should have a public const string SectionName."
        );
    }
}

private static Class? GetCorrespondingOptionsClass(Class optionsValidatorClass)
{
    var optionClassName = optionsValidatorClass.Name[..optionsValidatorClass.Name.LastIndexOf("Validator")];
    return ArchRuleDefinition
        .Classes()
        .That().HaveName(optionClassName)
        .GetObjects(Architecture)
        .FirstOrDefault();
}
```

## CQRS 네이밍컨벤션

### 규칙
- 메시지
  - 접미사: Command, Query
  - 클래스: public, sealed, record
- 핸들러
  - 접미사: CommandUsecase, QueryUsecase
  - 클래스: internal, sealed

### 테스트
```xml
<ItemGroup>
	<InternalsVisibleTo Include="{T}.Tests.Unit" />
	<InternalsVisibleTo Include="DynamicProxyGenAssembly2" />
</ItemGroup>
```
- 테스트 프로젝트에서 Application 프로젝트의 `internal` 클래스를 접근하기 위해 `InternalsVisibleTo`을 지정합니다.

```cs
public static partial class Constants
{
    public static class NamingConvention
    {
        public const string Command = nameof(Command);
        public const string CommandUsecase = nameof(CommandUsecase);
        public const string Query = nameof(Query);
        public const string QueryUsecase = nameof(QueryUsecase);
    }
}
```

```cs
[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed partial class NamingConventionsTests : ArchitectureTestBase
{
    [Fact]
    public void CommandMessages_ShouldComplyWith_DesignRules()
    {
        var suts = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(ICommand));

        if (!suts.GetObjects(Architecture).Any())
            return;

        suts.Should().BePublic()
            .AndShould().BeSealed()
            .AndShould().BeRecord()
            .AndShould().HaveNameEndingWith(NamingConvention.Command)
            .Check(Architecture);
    }

    [Fact]
    public void CommandMessagesT_ShouldComplyWith_DesignRules()
    {
        var suts = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(ICommand<>));

        if (!suts.GetObjects(Architecture).Any())
            return;

        suts.Should().BePublic()
            .AndShould().BeSealed()
            .AndShould().BeRecord()
            .AndShould().HaveNameEndingWith(NamingConvention.Command)
            .Check(Architecture);
    }

    [Fact]
    public void CommandUsecases_ShouldComplyWith_DesignRules()
    {
        var suts = ArchRuleDefinition
        .Classes()
            .That()
            .ImplementInterface(typeof(ICommandUsecase<>));

        if (!suts.GetObjects(Architecture).Any())
            return;

        suts.Should().BeInternal()
            .AndShould().BeSealed()
            .AndShould().HaveNameEndingWith(NamingConvention.CommandUsecase)
            .Check(Architecture);
    }

    [Fact]
    public void CommandUsecasesT_ShouldComplyWith_DesignRules()
    {
        var suts = ArchRuleDefinition
        .Classes()
            .That()
            .ImplementInterface(typeof(ICommandUsecase<,>));

        if (!suts.GetObjects(Architecture).Any())
            return;

        suts.Should().BeInternal()
            .AndShould().BeSealed()
            .AndShould().HaveNameEndingWith(NamingConvention.CommandUsecase)
            .Check(Architecture);
    }

    [Fact]
    public void QueryMessagesT_ShouldComplyWith_DesignRules()
    {
        var suts = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IQuery<>));

        if (!suts.GetObjects(Architecture).Any())
            return;

        suts.Should().BePublic()
            .AndShould().BeSealed()
            .AndShould().BeRecord()
            .AndShould().HaveNameEndingWith(NamingConvention.Query)
            .Check(Architecture);
    }

    [Fact]
    public void QueryUsecasesT_ShouldComplyWith_DesignRules()
    {
        var suts = ArchRuleDefinition
        .Classes()
            .That()
            .ImplementInterface(typeof(IQueryUsecase<,>));

        if (!suts.GetObjects(Architecture).Any())
            return;

        suts.Should().BeInternal()
            .AndShould().BeSealed()
            .AndShould().HaveNameEndingWith(NamingConvention.QueryUsecase)
            .Check(Architecture);
    }
}
```