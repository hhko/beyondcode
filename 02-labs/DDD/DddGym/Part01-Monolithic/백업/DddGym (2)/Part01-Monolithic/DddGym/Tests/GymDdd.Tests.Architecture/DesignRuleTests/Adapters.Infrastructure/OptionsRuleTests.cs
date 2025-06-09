using ArchUnitNET.Fluent;
using GymDdd.Tests.Architecture.Abstractions.ArchitectureRules;
using GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.Musts;
using GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.SyntaxLevelRules.Utilities;
using static GymDdd.Tests.Architecture.Abstractions.Constants.Constants;

namespace GymDdd.Tests.Architecture.DesignRuleTests.Adapters.Infrastructure;

//// Options 규칙
////
////public sealed class {옵션}Options
////{
////    public const string SectionName = "{옵션}";
////
////    public required {타입} {옵션1} { get; init; }
////    public required {타입} {옵션2} { get; init; }
////
////    internal sealed class Validator : AbstractValidator<{섹션이름}Options>
////    {
////        public Validator()
////        {
////            RulFor(x => x_{옵션1_유효성검사});
////            RulFor(x => x_{옵션2_유효성검사});
////        }
////    }
////}

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
