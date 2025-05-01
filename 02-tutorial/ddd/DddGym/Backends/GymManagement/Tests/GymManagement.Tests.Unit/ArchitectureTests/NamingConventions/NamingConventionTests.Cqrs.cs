using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed partial class NamingConventionTests_Cqrs : ArchitectureTestBase
{

    // CQRS 네이밍 컨벤션
    //
    //  메시지: public sealed record {클래스}(Command | Query)
    //  핸들러: internal sealed class {클래스}(CommandUsecase | QueryUsecase)

    //[Fact]
    //public void CommandMessages_ShouldComplyWith_DesignRules()
    //{
    //    var sut = ArchRuleDefinition
    //        .Classes()
    //        .That()
    //        .ImplementInterface(typeof(ICommand));

    //    if (!sut.GetObjects(Architecture).Any())
    //        return;

    //    sut.Should().BePublic()
    //        .AndShould().BeSealed()
    //        .AndShould().BeRecord()
    //        .AndShould().HaveNameEndingWith(NamingConvention.Command)
    //        .Check(Architecture);
    //}

    [Fact]
    public void CommandMessagesT_ShouldComplyWith_DesignRules()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(ICommand2<>))
            .Should().BePublic()
            .AndShould().BeSealed()
            .AndShould().BeRecord()
            .AndShould().HaveNameEndingWith(NamingConvention.Command)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    //[Fact]
    //public void CommandUsecases_ShouldComplyWith_DesignRules()
    //{
    //    var sut = ArchRuleDefinition
    //        .Classes()
    //        .That()
    //        .ImplementInterface(typeof(ICommandUsecase2<>));

    //    if (!sut.GetObjects(Architecture).Any())
    //        return;

    //    sut.Should().BeInternal()
    //        .AndShould().BeSealed()
    //        .AndShould().HaveNameEndingWith(NamingConvention.CommandUsecase)
    //        .Check(Architecture);
    //}

    [Fact]
    public void CommandUsecasesT_ShouldComplyWith_DesignRules()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(ICommandUsecase2<,>))
            .Should().BeInternal()
            .AndShould().BeSealed()
            .AndShould().HaveNameEndingWith(NamingConvention.CommandUsecase)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void QueryMessagesT_ShouldComplyWith_DesignRules()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IQuery2<>))
            .Should().BePublic()
            .AndShould().BeSealed()
            .AndShould().BeRecord()
            .AndShould().HaveNameEndingWith(NamingConvention.Query)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void QueryUsecasesT_ShouldComplyWith_DesignRules()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IQueryUsecase2<,>))
            .Should().BeInternal()
            .AndShould().BeSealed()
            .AndShould().HaveNameEndingWith(NamingConvention.QueryUsecase)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }
}
