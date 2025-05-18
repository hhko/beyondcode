using ArchUnitNET.Fluent;
using GymDdd.Tests.Architecture.Abstractions.Rules;
using GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules.Musts;
using GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules.Utilities;
using static GymDdd.Tests.Architecture.Abstractions.Constants.Constants;

namespace GymDdd.Tests.Architecture.NamingConventionsTests;

//// CQRS 규칙
////
////public static class {메시지}(Command | Query)
////{
////    public sealed record Request(
////        {입력데이터}...) : ICommand<Response>;
////
////    public sealed record Response(
////        {출력데이터}...) : IResponse;
////
////    internal sealed class Validator : AbstractValidator<Request>
////    {
////        public Validator()
////        {
////            {입력데이터_유효성검사}
////        }
////    }
////
////    // TODO: Telemetry, Usecase
////}

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed partial class CqrsTests : ArchitectureTestBase
{
    [Fact]
    public void CommandClass_ShouldHave_PublicSealedAccessModifiers()
    {
        //ArchRuleDefinition.Classes()
        //    .That()
        //    .HaveNameEndingWith(NamingConvention.Command)
        //    .Should().BePublic()
        //    .AndShould().BeSealed()
        //    .AndShould().NotBeNested()
        //    .WithoutRequiringPositiveResults()
        //    .Check(Architecture);

        // Arrange
        var provider = ArchRuleDefinition
            .Classes()
            .That()
            .HaveNameEndingWith(NamingConvention.Command);

        List<IArchitectureRule> sut = [
            Must.HaveNamedNestedClassesMatches(
                provider,
                new Dictionary<string, Func<Type, bool>>
                {
                    // TODO: public static class RegisterCommand
                    //  - static class: IsAbstract && IsSealed

                    // public sealed record Request
                    [NamingConvention.Request] = nested =>
                        nested.IsNestedPublic &&
                        nested.IsSealed,

                    // public sealed record Response
                    [NamingConvention.Response] = nested =>
                        nested.IsNestedPublic &&
                        nested.IsSealed,

                    // internal sealed class Validator : AbstractValidator<Request>
                    [NamingConvention.Validator] = nested =>
                        nested.IsNestedAssembly &&
                        nested.IsSealed,

                    // TODO: internal sealed class Usecase
                })
        ];

        // Act
        var actual = sut.Evaluate(ServiceArchitecture);

        // Assert
        actual.ShouldHaveNoViolationRules();
    }

    //    [Fact]
    //    public void CommandClass_ShouldHave_NestedRequest_And_NestedResponse_And_NestedValidator()
    //    {
    //        // Arrange
    //        var rules = new[]
    //        {
    //            new NestedClassRuleBuilder(NamingConvention.Request)
    //                .MustBePublic()
    //                .MustBeSealed()
    //                .MustImplementsGenericInterface(typeof(ICommand<>))
    //                .Build(),

    //            new NestedClassRuleBuilder(NamingConvention.Response)
    //                .MustBePublic()
    //                .MustBeSealed()
    //                .MustImplementsInterface(typeof(IResponse))
    //                .Build(),

    //            new NestedClassRuleBuilder(NamingConvention.Validator)
    //                .MustBeInternal()
    //                .MustBeSealed()
    //                .MustImplementsGenericInterface(typeof(IValidator<>))
    //                .Build(),
    //        };

    //        // Act
    //        var violations = CheckNestedClassRules(NamingConvention.Command, rules);

    //        // Assert
    //        violations.ShouldBeEmpty("All Command classes must have required nested classes that fulfill the design rules.");
    //    }

    //    [Fact]
    //    public void QueryClass_ShouldHave_PublicSealedAccessModifiers()
    //    {
    //        ArchRuleDefinition.Classes()
    //            .That()
    //            .HaveNameEndingWith(NamingConvention.Query)
    //            .Should().BePublic()
    //            .AndShould().BeSealed()
    //            .AndShould().NotBeNested()
    //            .WithoutRequiringPositiveResults()
    //            .Check(Architecture);
    //    }

    //    [Fact]
    //    public void QueryClass_ShouldHave_NestedRequest_And_NestedResponse_And_NestedValidator()
    //    {
    //        // Assert
    //        var rules = new[]
    //        {
    //            new NestedClassRuleBuilder(NamingConvention.Request)
    //                .MustBePublic()
    //                .MustBeSealed()
    //                .MustImplementsGenericInterface(typeof(IQuery<>))
    //                .Build(),

    //            new NestedClassRuleBuilder(NamingConvention.Response)
    //                .MustBePublic()
    //                .MustBeSealed()
    //                .MustImplementsInterface(typeof(IResponse))
    //                .Build(),

    //            new NestedClassRuleBuilder(NamingConvention.Validator)
    //                .MustBeInternal()
    //                .MustBeSealed()
    //                .MustImplementsGenericInterface(typeof(IValidator<>))
    //                .Build(),
    //        };

    //        // Act
    //        var violations = CheckNestedClassRules(NamingConvention.Query, rules);

    //        // Assert
    //        violations.ShouldBeEmpty("All Query classes must have required nested classes that fulfill the design rules.");
    //    }

    //    [Fact]
    //    public void CommandRequestClasses_ShouldBe_Nested()
    //    {
    //        ArchRuleDefinition
    //            .Classes()
    //            .That()
    //            .ImplementInterface(typeof(ICommand<>))
    //            .Should().BeNested()
    //            .WithoutRequiringPositiveResults()
    //            .Check(Architecture);
    //    }

    //    [Fact]
    //    public void QueryRequestClasses_ShouldBe_Nested()
    //    {
    //        ArchRuleDefinition
    //            .Classes()
    //            .That()
    //            .ImplementInterface(typeof(IQuery<>))
    //            .Should().BeNested()
    //            .WithoutRequiringPositiveResults()
    //            .Check(Architecture);
    //    }

    //    [Fact]
    //    public void ResponseClasses_ShouldBe_Nested()
    //    {
    //        ArchRuleDefinition
    //            .Classes()
    //            .That()
    //            .ImplementInterface(typeof(IResponse))
    //            .Should().BeNested()
    //            .WithoutRequiringPositiveResults()
    //            .Check(Architecture);
    //    }

    //    [Fact]
    //    public void ValidatorClasses_ShouldBe_Nested()
    //    {
    //        ArchRuleDefinition
    //            .Classes()
    //            .That()
    //            .ImplementInterface(typeof(IValidator<>))
    //            .Should().BeNested()
    //            .WithoutRequiringPositiveResults()
    //            .Check(Architecture);
    //    }

    //    // TODO: Telemetry
    //    // TODO: Usecase

    //    //[Fact]
    //    //public void Class_Should_Have_Static_TryCreate_Method()
    //    //{
    //    //    // Arrange
    //    //    var rule = new StaticMethodRule(
    //    //        "Must have static TryCreate method",
    //    //        StaticMethodPredicates.HasStaticMethod("TryCreate"));

    //    //    var validator = new ValidatorEngine<Type>([rule]);

    //    //    // Act
    //    //    var validResult = validator.Validate(typeof(SampleWithFactory));
    //    //    var invalidResult = validator.Validate(typeof(SampleWithoutFactory));

    //    //    // Assert
    //    //    validResult.ShouldBeEmpty();
    //    //    invalidResult.ShouldContain("Violated: Must have static TryCreate method");
    //    //}
    //}
}