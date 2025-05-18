using ArchUnitNET.Fluent;
using FunctionalDdd.Framework.BaseTypes;
using GymDdd.Tests.Architecture.Abstractions.Rules;
using GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules.Utilities;
using GymDdd.Tests.Architecture.ArchitectureTests.Rules.SyntaxLevelRules;
using static GymDdd.Tests.Architecture.Abstractions.Constants.Constants;

namespace GymDdd.Tests.Architecture.RuleTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class DomainTypesTests : ArchitectureTestBase
{
    //[Fact]
    //public void IValueObjects_ShouldBe_Immutable()
    //{
    //    ////Arrange
    //    //var assembly = Domain.AssemblyReference.Assembly;

    //    ////Act
    //    //var result = Types
    //    //    .InAssembly(assembly)
    //    //    .That()
    //    //    .ImplementInterface(typeof(IValueObject))
    //    //    .And()
    //    //    .AreNotAbstract()
    //    //    .Should()
    //    //    .BeImmutable()
    //    //    .GetResult();

    //    ////Assert
    //    //result.IsSuccessful.Should().BeTrue();

    //    var sut = ArchRuleDefinition
    //        .Classes()
    //        .That()
    //        .ImplementInterface(typeof(IValueObject));

    //    var classes = sut.GetObjects(Architecture);
    //    if (!classes.Any())
    //        return;

    //    sut.Should().BeImmutable();
    //}


    [Fact]
    public void ValueObject_ShouldSatisfy_DesignRules()
    {
        // Arrange
        var provider = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValueObject));

        // 설계 규칙
        List<IArchitectureRule> sut = [
            // Value Object는 public sealed 클래스여야 합니다.
            provider
                .Should().BePublic()
                .AndShould().BeSealed()
                .ToArchitectureRule(),

            // Value Object는 private 파라미터 없는 생성자를 가져야 합니다.
            Must.HavePrivateParameterlessConstructor(provider),

            // Value Object는 모든 생성자가 private 이어야 합니다.
            Must.HaveAllPrivateConstructors(provider),

            // Value Object는 public static Create와 Validato 메서드를 가져야 합니다.
            Must.HaveStaticMethod(provider,
                IValueObject.CreateMethodName,
                IValueObject.ValidateMethodName),

            // TODO: Immutable 규칙 추가 (필드 Readonly, 컬렉션 방어적 복사 등)
        ];

        // Act
        var actual = sut.Evaluate(ServiceArchitecture);

        // Assert
        actual.ShouldHaveNoViolationRules();
    }

    [Fact]
    public void Entity_ShouldSatisfy_DesignRules()
    {
        // Arrange
        var provider = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IEntity));

        // 설계 규칙
        List<IArchitectureRule> sut = [
            // Entity는 public sealed 클래스여야 합니다.
            //Must.BeSealed(provider),
            provider.Should().BeSealed().ToArchitectureRule(),

            // Entity는 private 파라미터 없는 생성자를 가져야 합니다.
            Must.HavePrivateParameterlessConstructor(provider),

            // Entity는 모든 생성자가 private 이어야 합니다.
            Must.HaveAllPrivateConstructors(provider),

            // Entity는 public static Create 메서드를 가져야 합니다.
            Must.HaveStaticMethod(provider, IEntity.CreateMethodName),
        ];

        // Act
        var actual = sut.Evaluate(ServiceArchitecture);

        // Assert
        actual.ShouldHaveNoViolationRules();
    }
}
