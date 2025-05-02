using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed partial class NamingConventionTestsCqrs : ArchitectureTestBase
{
    [Fact]
    public void CommandClass_ShouldHave_PublicSealedAccessModifiers()
    {
        ArchRuleDefinition.Classes()
            .That()
            .HaveNameEndingWith(NamingConvention.Command)
            .Should().BePublic()
            .AndShould().BeSealed()
            .AndShould().NotBeNested()
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void CommandClass_ShouldHave_NestedRequest_And_NestedResponse_And_NestedValidator()
    {
        // Arrange
        var rules = new[]
        {
            new NestedClassRuleBuilder(NamingConvention.Request)
                .MustBePublic()
                .MustBeSealed()
                .MustImplementsGenericInterface(typeof(ICommand<>))
                .Build(),

            new NestedClassRuleBuilder(NamingConvention.Response)
                .MustBePublic()
                .MustBeSealed()
                .MustImplementsInterface(typeof(IResponse))
                .Build(),

            new NestedClassRuleBuilder(NamingConvention.Validator)
                .MustBeInternal()
                .MustBeSealed()
                .MustImplementsGenericInterface(typeof(IValidator<>))
                .Build(),
        };

        // Act
        var violations = CheckNestedClassRules(NamingConvention.Command, rules);

        // Assert
        violations.ShouldBeEmpty("All Command classes must have required nested classes that fulfill the design rules.");
    }

    [Fact]
    public void QueryClass_ShouldHave_PublicSealedAccessModifiers()
    {
        ArchRuleDefinition.Classes()
            .That()
            .HaveNameEndingWith(NamingConvention.Query)
            .Should().BePublic()
            .AndShould().BeSealed()
            .AndShould().NotBeNested()
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void QueryClass_ShouldHave_NestedRequest_And_NestedResponse_And_NestedValidator()
    {
        // Arrange
        var rules = new[]
        {
            new NestedClassRuleBuilder(NamingConvention.Request)
                .MustBePublic()
                .MustBeSealed()
                .MustImplementsGenericInterface(typeof(IQuery<>))
                .Build(),

            new NestedClassRuleBuilder(NamingConvention.Response)
                .MustBePublic()
                .MustBeSealed()
                .MustImplementsInterface(typeof(IResponse))
                .Build(),

            new NestedClassRuleBuilder(NamingConvention.Validator)
                .MustBeInternal()
                .MustBeSealed()
                .MustImplementsGenericInterface(typeof(IValidator<>))
                .Build(),
        };

        // Act
        var violations = CheckNestedClassRules(NamingConvention.Query, rules);

        // Assert
        violations.ShouldBeEmpty("All Query classes must have required nested classes that fulfill the design rules.");
    }

    //[Fact]
    //public void CommandClass_ShouldHave_NestedRequest()
    //{
    //    // Arrange
    //    var rules = new[]
    //    {

    //    };

    //    // Act
    //    var violations = CheckNestedClassRules(NamingConvention.Command, rules);

    //    // Assert
    //    violations.ShouldBeEmpty("All Options classes must have required nested classes that fulfill the design rules.");
    //}


    //// CQRS 네이밍 컨벤션
    ////
    ////  메시지: public sealed record {클래스}(Command | Query)
    ////  핸들러: internal sealed class {클래스}(CommandUsecase | QueryUsecase)

    ////[Fact]
    ////public void CommandMessages_ShouldComplyWith_DesignRules()
    ////{
    ////    var sut = ArchRuleDefinition
    ////        .Classes()
    ////        .That()
    ////        .ImplementInterface(typeof(ICommand));

    ////    if (!sut.GetObjects(Architecture).Any())
    ////        return;

    ////    sut.Should().BePublic()
    ////        .AndShould().BeSealed()
    ////        .AndShould().BeRecord()
    ////        .AndShould().HaveNameEndingWith(NamingConvention.Command)
    ////        .Check(Architecture);
    ////}

    //[Fact]
    //public void CommandMessagesT_ShouldComplyWith_DesignRules()
    //{
    //    ArchRuleDefinition
    //        .Classes()
    //        .That()
    //        .ImplementInterface(typeof(ICommand<>))
    //        .Should().BePublic()
    //        .AndShould().BeSealed()
    //        .AndShould().BeRecord()
    //        .AndShould().HaveNameEndingWith(NamingConvention.Command)
    //        .WithoutRequiringPositiveResults()
    //        .Check(Architecture);
    //}

    ////[Fact]
    ////public void CommandUsecases_ShouldComplyWith_DesignRules()
    ////{
    ////    var sut = ArchRuleDefinition
    ////        .Classes()
    ////        .That()
    ////        .ImplementInterface(typeof(ICommandUsecase2<>));

    ////    if (!sut.GetObjects(Architecture).Any())
    ////        return;

    ////    sut.Should().BeInternal()
    ////        .AndShould().BeSealed()
    ////        .AndShould().HaveNameEndingWith(NamingConvention.CommandUsecase)
    ////        .Check(Architecture);
    ////}

    //[Fact]
    //public void CommandUsecasesT_ShouldComplyWith_DesignRules()
    //{
    //    ArchRuleDefinition
    //        .Classes()
    //        .That()
    //        .ImplementInterface(typeof(ICommandUsecase<,>))
    //        .Should().BeInternal()
    //        .AndShould().BeSealed()
    //        .AndShould().HaveNameEndingWith(NamingConvention.CommandUsecase)
    //        .WithoutRequiringPositiveResults()
    //        .Check(Architecture);
    //}

    //[Fact]
    //public void QueryMessagesT_ShouldComplyWith_DesignRules()
    //{
    //    ArchRuleDefinition
    //        .Classes()
    //        .That()
    //        .ImplementInterface(typeof(IQuery<>))
    //        .Should().BePublic()
    //        .AndShould().BeSealed()
    //        .AndShould().BeRecord()
    //        .AndShould().HaveNameEndingWith(NamingConvention.Query)
    //        .WithoutRequiringPositiveResults()
    //        .Check(Architecture);
    //}

    //[Fact]
    //public void QueryUsecasesT_ShouldComplyWith_DesignRules()
    //{
    //    ArchRuleDefinition
    //        .Classes()
    //        .That()
    //        .ImplementInterface(typeof(IQueryUsecase<,>))
    //        .Should().BeInternal()
    //        .AndShould().BeSealed()
    //        .AndShould().HaveNameEndingWith(NamingConvention.QueryUsecase)
    //        .WithoutRequiringPositiveResults()
    //        .Check(Architecture);
    //}
}
