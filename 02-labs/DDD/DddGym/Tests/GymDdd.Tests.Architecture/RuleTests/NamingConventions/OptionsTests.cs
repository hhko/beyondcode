using ArchUnitNET.Fluent;
using GymDdd.Tests.Architecture.Abstractions.Rules;
using GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules.Musts;
using GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules.Utilities;
using static GymDdd.Tests.Architecture.Abstractions.Constants.Constants;

namespace GymDdd.Tests.Architecture.RuleTests.NamingConventions;

//// Options 규칙
////
////public sealed class {섹션이름}Options
////{
////    public const string SectionName = "{섹션이름}";
////
////    public required {옵션_타입} {옵션} { get; init; }
////
////    internal sealed class Validator : AbstractValidator<{섹션이름}Options>
////    {
////        public Validator()
////        {
////            {옵션_유효성검사}
////        }
////    }
////}

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class OptionsTests : ArchitectureTestBase
{
    [Fact]
    public void OptionsClasses_ShouldHave_SectionName()
    {
        // Arrange
        var provider = ArchRuleDefinition
            .Classes()
            .That()
            .HaveNameEndingWith(NamingConvention.Options);

        List<IArchitectureRule> sut = [
            // Options는 public sealed 클래스여야 합니다.
            // Options는 SectionName 필드를 가져야 합니다.
            provider.Should().BePublic()
                .AndShould().BeSealed()
                .AndShould().HaveFieldMemberWithName(NamingConvention.SectionName)
                .ToArchitectureRule(),

            // Options는 internal sealed Validator 중첩 클래스를 가져야 합니다.
            Must.HaveInternalSealedNestedClass(provider, NamingConvention.Validator)
        ];

        // Act
        var actual = sut.Evaluate(ServiceArchitecture);

        // Assert
        actual.ShouldHaveNoViolationRules();
    }
}
