using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using Crop.Hello.Framework.Contracts.CQRS;
using static Crop.Hello.Api.Tests.Unit.Abstractions.Constants.Constants;

namespace Crop.Hello.Api.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed class NamingConventionsCQRSTests : ArchitectureBaseTest
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
    public void CommandUseCases_ShouldComplyWith_DesignRules()
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
    public void CommandUseCasesT_ShouldComplyWith_DesignRules()
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
    public void QueryUseCasesT_ShouldComplyWith_DesignRules()
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