---
outline: deep
---

# CQRS 네이밍컨벤션 테스트

## 규칙
- 메시지
  - 접미사: Command, Query
  - 클래스: public, sealed, record
- 핸들러
  - 접미사: CommandUsecase, QueryUsecase
  - 클래스: internal, sealed

## 테스트
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