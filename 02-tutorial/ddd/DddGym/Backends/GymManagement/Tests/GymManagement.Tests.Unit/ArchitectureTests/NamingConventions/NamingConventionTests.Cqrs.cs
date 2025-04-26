using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed partial class NamingConventionTests_Cqrs : ArchitectureTestBase
{

    // CQRS 네이밍컨벤션
    //
    // - 메시지
    //   - 접미사: Command, Query
    //   - 클래스: public, sealed, record
    // - 핸들러
    //   - 접미사: CommandUsecase, QueryUsecase
    //   - 클래스: internal, sealed

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
        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(ICommand2<>));

        if (!sut.GetObjects(Architecture).Any())
            return;

        sut.Should().BePublic()
            .AndShould().BeSealed()
            .AndShould().BeRecord()
            .AndShould().HaveNameEndingWith(NamingConvention.Command)
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
        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(ICommandUsecase2<,>));

        if (!sut.GetObjects(Architecture).Any())
            return;

        sut.Should().BeInternal()
            .AndShould().BeSealed()
            .AndShould().HaveNameEndingWith(NamingConvention.CommandUsecase)
            .Check(Architecture);
    }

    [Fact]
    public void QueryMessagesT_ShouldComplyWith_DesignRules()
    {
        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IQuery2<>));

        if (!sut.GetObjects(Architecture).Any())
            return;

        sut.Should().BePublic()
            .AndShould().BeSealed()
            .AndShould().BeRecord()
            .AndShould().HaveNameEndingWith(NamingConvention.Query)
            .Check(Architecture);
    }

    [Fact]
    public void QueryUsecasesT_ShouldComplyWith_DesignRules()
    {
        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IQueryUsecase2<,>));

        if (!sut.GetObjects(Architecture).Any())
            return;

        sut.Should().BeInternal()
            .AndShould().BeSealed()
            .AndShould().HaveNameEndingWith(NamingConvention.QueryUsecase)
            .Check(Architecture);
    }
}
