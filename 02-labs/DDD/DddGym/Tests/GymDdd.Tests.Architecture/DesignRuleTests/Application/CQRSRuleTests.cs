using ArchUnitNET.Fluent;
using GymDdd.Framework.BaseTypes.Cqrs;
using GymDdd.Tests.Architecture.Abstractions.ArchitectureRules;
using GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.Musts;
using GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.SyntaxLevelRules.Utilities;
using static GymDdd.Tests.Architecture.Abstractions.Constants.Constants;

namespace GymDdd.Tests.Architecture.DesignRuleTests.Application;

//// CQRS 규칙
////
////public static class {메시지}(Command | Query)
////{
////    public sealed record Request(
////        {입력1}, {입력2}, ...) : ICommand<Response>;
////
////    public sealed record Response(
////        {출력1}, {출력2}, ...) : IResponse;
////
////    internal sealed class Validator : AbstractValidator<Request>
////    {
////        public Validator()
////        {
////            RuleFor(x => {입력1_유효성검사});
////            RuleFor(x => {입력2_유효성검사});
////        }
////    }
////
////    // TODO: Telemetry
////    // TODO: Usecase
////}

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed partial class CQRSRuleTests : ArchitectureTestBase
{
    [Fact]
    public void Command_ShouldSatisfy_DesignRules()
    {
        // Arrange
        var provider = ArchRuleDefinition
            .Classes()
            .That()
            .HaveNameEndingWith(NamingConvention.Command);

        IList<IArchitectureRule> sut = [
            // public static class {Command}
            provider
                .Should().BePublic()
                .AndShould().BeAbstract()
                .AndShould().BeSealed()
                .AndShould().NotBeNested()
                .ToArchitectureRule(),

            // 중첩 클래스
            Must.HaveNamedNestedClassMatches(provider,
                // public sealed record Request : ICommand<>
                (NamingConvention.Request, nested => Must.IsNestedPublicSealed(nested)
                                                     && Must.IsImplementsInterface(nested, typeof(ICommand<>))),

                // public sealed record Response : IResponse
                (NamingConvention.Response, nested => Must.IsNestedPublicSealed(nested)
                                                      && Must.IsImplementsInterface(nested, typeof(IResponse))),

                // internal sealed class Validator : AbstractValidator<Request>
                (NamingConvention.Validator, Must.IsNestedInternalSealed)

                // TODO:
                //
                // internal sealed class CreateGymCommandUsecase
                //  : ICommandUsecase<CreateGymCommand, CreateGymResponse>
            )
        ];

        // Act
        var actual = sut.Evaluate(ServiceArchitecture);

        // Assert
        actual.ShouldHaveNoViolationRules();
    }

    // TODO: Query_ShouldSatisfy_DesignRules()
    // TODO: Event_ShouldSatisfy_DesignRules()
}