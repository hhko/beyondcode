using ArchUnitNET.Fluent;

using DomainTypes_ValueObjects.Case08_ArchitectureTests.Framework;
using DomainTypes_ValueObjects.Tests.Abstractions.ArchitectureRules;
using DomainTypes_ValueObjects.Tests.Abstractions.ArchitectureRules.Musts;
using DomainTypes_ValueObjects.Tests.Abstractions.ArchitectureRules.Utilities;

using static DomainTypes_ValueObjects.Tests.Abstractions.Constants.Constants;

namespace DomainTypes_ValueObjects.Tests.Case08_ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed class DomainTypeRuleTests : ArchitectureTestBase
{
    [Fact]
    public void ValueObject_ShouldSatisfy_DesignRules()
    {
        // Arrange
        var provider = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValueObject8));

        // 설계 규칙
        List<IArchitectureRule> sut = [
            // public sealed class {ValueObject}
            provider
                .Should().BePublic()
                .AndShould().BeSealed()
                .ToArchitectureRule(),

            // private {생성자}
            Must.HaveConstructorAllMatches(provider, Must.IsPrivateConstructor),

            // public static {ValueObject} Create
            // public static {ValueObject} Validate
            Must.HaveNamedMethodMatches(provider,
                (IValueObject8.CreateMethodName, Must.IsPublicStaticMethod),
                (IValueObject8.ValidateMethodName, Must.IsPublicStaticMethod)
            )

            // TODO: Immutable 규칙 추가 (필드 Readonly, 컬렉션 방어적 복사 등)
        ];

        // Act
        var actual = sut.Evaluate(Architectures);

        // Assert
        actual.ShouldHaveNoViolationRules();
    }
}
