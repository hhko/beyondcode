//using ArchUnitNET.Fluent;
//using ArchUnitNET.xUnit;
//using Mirero.DBS.Framework.Contracts.Application.CQRS;
//using static Mirero.DBS.Listener.Tests.Unit.Abstractions.Constants.Constants;

//namespace Crop.Hello.Api.Tests.Unit.ArchitectureTests;

//[Trait(nameof(UnitTest), UnitTest.Architecture)]
//public sealed class NamingConventionsCQRSTests : ArchitectureBaseTest
//{
//    [Fact]
//    public void CommandMessages_ShouldComplyWith_DesignRules()
//    {
//        var suts = ArchRuleDefinition
//            .Classes()
//            .That()
//            .ImplementInterface(typeof(ICommand));

//        if (!suts.GetObjects(Architecture).Any())
//            return;

//        suts.Should().BePublic()
//            .AndShould().BeSealed()
//            .AndShould().BeRecord()
//            .AndShould().HaveNameEndingWith(NamingConvention.Command)
//            .Check(Architecture);
//    }

//    [Fact]
//    public void CommandMessagesT_ShouldComplyWith_DesignRules()
//    {
//        var suts = ArchRuleDefinition
//            .Classes()
//            .That()
//            .ImplementInterface(typeof(ICommand<>));

//        if (!suts.GetObjects(Architecture).Any())
//            return;

//        suts.Should().BePublic()
//            .AndShould().BeSealed()
//            .AndShould().BeRecord()
//            .AndShould().HaveNameEndingWith(NamingConvention.Command)
//            .Check(Architecture);
//    }

//    [Fact]
//    public void CommandUseCases_ShouldComplyWith_DesignRules()
//    {
//        var suts = ArchRuleDefinition
//            .Classes()
//            .That()
//            .ImplementInterface(typeof(ICommandUseCase<>));

//        if (!suts.GetObjects(Architecture).Any())
//            return;

//        suts.Should().BeInternal()
//            .AndShould().BeSealed()
//            .AndShould().HaveNameEndingWith(NamingConvention.CommandUseCase)
//            .Check(Architecture);
//    }

//    [Fact]
//    public void CommandUseCasesT_ShouldComplyWith_DesignRules()
//    {
//        var suts = ArchRuleDefinition
//            .Classes()
//            .That()
//            .ImplementInterface(typeof(ICommandUseCase<,>));

//        if (!suts.GetObjects(Architecture).Any())
//            return;

//        suts.Should().BeInternal()
//            .AndShould().BeSealed()
//            .AndShould().HaveNameEndingWith(NamingConvention.CommandUseCase)
//            .Check(Architecture);
//    }

//    [Fact]
//    public void QueryMessagesT_ShouldComplyWith_DesignRules()
//    {
//        var suts = ArchRuleDefinition
//            .Classes()
//            .That()
//            .ImplementInterface(typeof(IQuery<>));

//        if (!suts.GetObjects(Architecture).Any())
//            return;

//        suts.Should().BePublic()
//            .AndShould().BeSealed()
//            .AndShould().BeRecord()
//            .AndShould().HaveNameEndingWith(NamingConvention.Query)
//            .Check(Architecture);
//    }

//    [Fact]
//    public void QueryUseCasesT_ShouldComplyWith_DesignRules()
//    {
//        var suts = ArchRuleDefinition
//            .Classes()
//            .That()
//            .ImplementInterface(typeof(IQueryUseCase<,>));

//        if (!suts.GetObjects(Architecture).Any())
//            return;

//        suts.Should().BeInternal()
//            .AndShould().BeSealed()
//            .AndShould().HaveNameEndingWith(NamingConvention.QueryUseCase)
//            .Check(Architecture);
//    }
//}