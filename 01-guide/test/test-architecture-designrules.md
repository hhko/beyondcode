
```cs
[Fact]
public void ValueObjectClasses_ShouldBe_Sealed()
{
    ArchRuleDefinition
        .Classes()
        .That()
        .ImplementInterface(typeof(IValueObject))
        .Should().BeSealed()
        .WithoutRequiringPositiveResults()
        .Check(Architecture);
}

[Theory]
[InlineData(IValueObject.CreateMethodName)]
[InlineData(IValueObject.ValidateMethodName)]
public void ValueObjectClasses_ShouldHave_StaticMethod(string requiredMethodName)
{
    ArchRuleDefinition
        .Classes()
        .That()
        .ImplementInterface(typeof(IValueObject))
        .Should().HaveStaticMethod(requiredMethodName)
        .WithoutRequiringPositiveResults()
        .Check(Architecture);
}

[Fact]
public void ValueObjectClasses_ShouldHave_PrivateParameterlessConstructor()
{
    ArchRuleDefinition
        .Classes()
        .That()
        .ImplementInterface(typeof(IValueObject))
        .Should().HavePrivateParameterlessConstructor()
        .WithoutRequiringPositiveResults()
        .Check(Architecture);
}
```

```cs
[Fact]
public void ValueObject_ShouldSatisfy_DesignRules()
{
    // Arrange
    var provider = ArchRuleDefinition
        .Classes()
        .That()
        .ImplementInterface(typeof(IValueObject));

    //
    // 설계 규칙
    //
    List<IArchitectureRule> sut = [
        // public sealed 클래스
        Must.BeSealed(provider),

        // privatie 생성자()
        Must.HavePrivateParameterlessConstructor(provider),

        // public static Create/Validate
        Must.HaveStaticMethod(provider,
            IValueObject.CreateMethodName,
            IValueObject.ValidateMethodName),

        // TODO Immutable
    ];

    // Act
    var actual = sut.Evaluate(Architecture);

    // Assert
    actual.ShouldHaveNoViolations();
}
```

## 다이어그램
### 시퀀스 다이어그램

![](2025-05-17-16-25-12.png)

```mermaid
sequenceDiagram
    participant Test
    participant Must
    participant SyntaxConditionBuilder
    participant PredicateCondition
    participant SyntaxLevelRule
    participant ArchitectureRuleAssertionUtilities
    participant IObjectProvider
    participant RuleEvaluationResult
    participant ConditionResult

    %% 테스트 시작
    Test->>Must: BeSealed()
    Must->>SyntaxConditionBuilder: new
    SyntaxConditionBuilder->>PredicateCondition: MustSatisfy(Func, description)
    SyntaxConditionBuilder->>CompositeCondition: Build()
    SyntaxConditionBuilder-->>Must: returns ICondition
    Must->>SyntaxLevelRule: new (ruleName, condition, provider)
    Must-->>Test: returns IArchitectureRule

    %% Evaluate 호출
    Test->>ArchitectureRuleAssertionUtilities: Evaluate([IArchitectureRule], architecture)
    ArchitectureRuleAssertionUtilities->>SyntaxLevelRule: Evaluate(architecture)
    SyntaxLevelRule->>IObjectProvider: GetObjects()
    SyntaxLevelRule->>ICondition: Check(objects, architecture)
    ICondition->>ConditionResult: Check result
    ICondition-->>SyntaxLevelRule: IEnumerable<ConditionResult>
    SyntaxLevelRule->>RuleEvaluationResult: create with results
    SyntaxLevelRule-->>ArchitectureRuleAssertionUtilities: RuleEvaluationResult
    ArchitectureRuleAssertionUtilities-->>Test: returns List<RuleEvaluationResult>

    %% 결과 검증
    Test->>ArchitectureRuleAssertionUtilities: ShouldHaveNoViolations(results)
```

```mermaid
classDiagram
    %% 인터페이스
    class IArchitectureRule {
        +string RuleName
        +RuleEvaluationResult Evaluate(Architecture)
    }

    class ICondition {
        +string Description
        +IEnumerable Check(IEnumerable, Architecture)
        +bool CheckEmpty()
    }

    class IObjectProvider {
    }

    %% Must → Builder → Rule
    class Must {
        +IArchitectureRule HaveStaticMethod()
        +IArchitectureRule HavePrivateParameterlessConstructor()
        +IArchitectureRule BeSealed()
    }

    class SyntaxConditionBuilder {
        -List _conditions
        +MustSatisfy(Func, string)
        +ICondition Build()
    }

    class SyntaxLevelRule {
        -string _ruleName
        -ICondition _condition
        -IObjectProvider _provider
        +RuleName
        +RuleEvaluationResult Evaluate(Architecture)
    }

    Must ..> SyntaxConditionBuilder : uses
    Must ..> SyntaxLevelRule : creates

    %% Condition 구현
    class PredicateCondition {
        -Func _predicate
        -string _description
    }

    class CompositeCondition {
        -IEnumerable _conditions
    }

    PredicateCondition --|> ICondition
    CompositeCondition --|> ICondition
    SyntaxConditionBuilder --> PredicateCondition
    SyntaxConditionBuilder --> CompositeCondition

    %% Condition → Rule
    SyntaxLevelRule --> ICondition
    SyntaxLevelRule --> IObjectProvider
    SyntaxLevelRule --|> IArchitectureRule

    %% Evaluation
    class RuleEvaluationResult {
        +string RuleName
        +List Results
        +bool HasViolation
    }

    class ConditionResult {
    }

    SyntaxLevelRule ..> RuleEvaluationResult : creates
    RuleEvaluationResult --> ConditionResult

    %% Assertion 유틸
    class ArchitectureRuleAssertionUtilities {
        +List Evaluate(List, Architecture)
        +void ShouldHaveNoViolations(IEnumerable)
    }

    ArchitectureRuleAssertionUtilities ..> RuleEvaluationResult

    %% 스타일
    classDef right fill:#f0f8ff
```

- [ ] OR 조건
- [ ] Immutable 테스트
---
- [ ] 프로젝트 분리
---
- [ ] ReflectionLevelRules 코드 개선 작업
---
- [ ] 문서 정리
