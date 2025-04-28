# 아키텍처 테스트 사용자 정의 조건

## 개요
- ArchUnitNET 패키지를 활용하여, 사용자 정의 조건(Custom Rule)을 기반으로 다양한 아키텍처 규칙의 준수 여부를 테스트합니다.
- 주요 테스트 항목에는 네이밍 컨벤션, 레이어 간 의존성 규칙, 클래스 설계 규칙 등이 포함됩니다.
- ArchUnitNET이 기본적으로 제공하지 않는 조건까지 사용자 정의하여 아키텍처 테스트에 적용함으로써, 프로젝트 전반에 걸쳐 일관된 구조를 유지하고 품질 저하를 예방할 수 있습니다.

<br/>

## 테스트 확장 메서드 구현
- ArchUnitNET 패키지의 Assert 구문을 Fluent 스타일로 확장하여, 테스트 코드를 더 읽기 쉽고 직관적으로 작성할 수 있도록 ObjectsShould 확장 메서드를 정의합니다.
- 기본 제공 규칙 외에도 필요한 아키텍처 규칙을 만들기 위해, ICondition<TRuleType> 인터페이스를 구현한 클래스를 작성하고, 이를 FollowCustomCondition 메서드를 통해 테스트에 적용합니다.

```cs
using ArchUnitNET.Domain;
using ArchUnitNET.Fluent.Syntax.Elements;
using ArchUnitNET.Fluent.Syntax;

namespace GymManagement.Tests.Unit.ArchitectureTests.Conditions;

public static partial class ArchitectureUtilities
{
    public static TRuleTypeShouldConjunction HaveStaticMethod<TRuleTypeShouldConjunction, TRuleType>(
        this ObjectsShould<TRuleTypeShouldConjunction, TRuleType> should,
        string prefix)
            where TRuleType : ICanBeAnalyzed
            where TRuleTypeShouldConjunction : SyntaxElement<TRuleType>
    {
        var condition = new HaveStaticMethodCondition<TRuleType>(prefix);
        return should.FollowCustomCondition(condition);
    }
}
```

<br/>

## 사용자 정의 조건 구현
- `ICondition<TRuleType>` 인터페이스를 구현하여, 사용자 정의 조건의 준수 여부를 검증합니다.

```cs
internal sealed class HaveStaticMethodCondition<TRuleType> : ICondition<TRuleType>
    where TRuleType : ICanBeAnalyzed
{
    private readonly string _prefix;

    public HaveStaticMethodCondition(string prefix)
    {
        _prefix = prefix;
    }

    public string Description => $"does not have a method name starting with {_prefix}";

    public IEnumerable<ConditionResult> Check(IEnumerable<TRuleType> objects, Architecture architecture)
    {
        foreach (var @class in objects)
        {
            Class? classObject = @class as Class;
            if (classObject == null)
            {
                string actualType = @class?.GetType().FullName ?? "null";
                string expectedType = typeof(Class).FullName ?? "null";

                throw new InvalidCastException($"Type cast failed: actual type is '{actualType}', expected type was '{expectedType}'.");
            }

            bool hasMatchingMethod = classObject
                .GetMethodMembers()
                .Where(member =>
                    member.Name.StartsWith(_prefix) &&
                    member.IsStatic == true)
                .Any();

            yield return new ConditionResult(
                analyzedObject: @class,
                pass: hasMatchingMethod,
                failDescription: hasMatchingMethod
                    ? null
                    : Description);
        }
    }

    public bool CheckEmpty() =>
        true;
}
```

<br/>

## 사용자 정의 조건 활용
- `HaveStaticMethod` 확장 메서드와 `HaveStaticMethodCondition` 클래스는 `IValueObject` 인터페이스를 구현한 클래스에서 `Create` 및 `Validate` 정적 메서드의 구현 여부를 검증하는 데 활용할 수 있습니다.

```cs
[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class ValueObjectTests : ArchitectureTestBase
{
    [Theory]
    [InlineData(IValueObject.CreateMethodName)]
    [InlineData(IValueObject.ValidateMethodName)]
    public void ValueObjectClasses_ShouldHave_StaticMethod(string requiredMethodName)
    {
        // Arrange
        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValueObject));

        // Act
        var classes = sut.GetObjects(Architecture);
        if (!classes.Any())
            return;

        // Assert
        sut.Should()
            .HaveStaticMethod(requiredMethodName)       // 사용자 정의 조건건
            .Check(Architecture);
    }
}
```