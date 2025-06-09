using ArchUnitNET.Fluent;
using GymDdd.Framework.BaseTypes;
using GymDdd.Tests.Architecture.Abstractions.ArchitectureRules;
using GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.Musts;
using GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.SyntaxLevelRules.Utilities;
using static GymDdd.Tests.Architecture.Abstractions.Constants.Constants;

namespace GymDdd.Tests.Architecture.DesignRuleTests.Domain;

//// 값 객체 규칙
////
////public sealed class {값} : IValueObject
////{
////    private {값}()
////    { }
////
////    private {값}({입력1}, { 입력2}, ...)
////    {
////        // 단순 대입...
////    }
////
////    public static Fin<{값}> Create({입력1}, {입력2}, ...)
////    {
////        Error error = Validate({입력1}, {입력2}, ...);
////        return error.CreateValueObject(() => new {값}( ... );
////    }
////
////    public static Error Validate({입력1}, { 입력2}, ...)
////    {
////        // 유효성 검사
////        return Error.Empty
////            .If( ... );
////    }
////}

//// 엔티티 규칙
////
////{접근제어자} sealed class {엔티티} : IValueObject
////{
////    private {엔티티}()
////    { }
////
////    private {엔티티}({입력1}, { 입력2}, ...)
////    {
////        // 단순 대입...
////    }
////
////    public static {엔티티} Create({입력1}, {입력2}, ...)
////    {
////        return new {엔티티}( ... );
////    }
////}

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class DomainRuleTests : ArchitectureTestBase
{
    // TODO: Value Object Immutable 테스트

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
            // public sealed class {ValueObject}
            provider
                .Should().BePublic()
                .AndShould().BeSealed()
                .ToArchitectureRule(),

            // private {생성자}()
            Must.HaveConstructorAnyMatches(provider, Must.IsPrivateParameterlessConstructor),

            // private {생성자}
            Must.HaveConstructorAllMatches(provider, Must.IsPrivateConstructor),

            // public static {ValueObject} Create
            // public static {ValueObject} Validate
            Must.HaveNamedMethodMatches(provider,
                (IValueObject.CreateMethodName, Must.IsPublicStaticMethod),
                (IValueObject.ValidateMethodName, Must.IsPublicStaticMethod)
            )

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
            // sealed class {Entity}
            provider.Should().BeSealed().ToArchitectureRule(),

            // private {생성자}()
            Must.HaveConstructorAnyMatches(provider, Must.IsPrivateParameterlessConstructor),

            // private {생성자}
            Must.HaveConstructorAllMatches(provider, Must.IsPrivateConstructor),
            //Must.HaveAllPrivateConstructors(provider),

            // public static {Entity} Create
            Must.HaveNamedMethodMatches(provider,
                (IValueObject.CreateMethodName, Must.IsPublicStaticMethod)
            )
        ];

        // Act
        var actual = sut.Evaluate(ServiceArchitecture);

        // Assert
        actual.ShouldHaveNoViolationRules();
    }
}
