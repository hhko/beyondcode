# 설계 규칙 테스트

## 개요
- 소프트웨어를 개발하다 보면 팀 또는 프로젝트마다 지켜야 할 **설계 규칙이** 존재합니다.
- 예를 들어,
  - ValueObject는 반드시 public sealed여야 한다
  - 생성자는 반드시 private이어야 한다
  - 정적 생성 메서드(Create)는 public static이어야 한다
- 이러한 규칙들은 사람이 직접 코드를 보고 확인하기 어렵고, 실수로 어기기 쉽습니다.
- 이런 문제를 해결하기 위해 코드로 설계 규칙을 정의하고, 자동으로 테스트하는 방법을 소개합니다.

<br/>

## 지침
- 설계 규칙을 테스트 자동화합니다.
- 테스트 코드를 보면 어떤 설계 규칙이 적용되어 있는지 이해할 수 있어야 합니다.
- 테스트 실패 시, 메시지를 통해 어떤 설계 규칙이 위반되었는지 식별할 수 있어야 하며, 해당 정보를 기반으로 코드를 올바르게 수정할 수 있어야 합니다.

<br/>

## 구성 요소
- `IArchitectureRule` 인터페이스는 `ArchUnitNET과` 함께 동작하며,
  - 여러 설계 규칙을 하나의 논리적 단위로 묶어 테스트 메서드로 구성할 수 있도록 지원합니다.
  - 이를 통해 설계 검증 코드를 보다 명확하고 일관되게 구성할 수 있습니다.

  구분     | 테스트 메서드 구성 방식
  ----------|--------------------------
  개선 전  | 논리적 설계 규칙 : 테스트 메서드 = 1 : `N`
  개선 후  | 논리적 설계 규칙 : 테스트 메서드 = 1 : `1`

### 주요 클래스
구성 요소                    | 설명
-------------------------   | -------------------------------------
`IArchitectureRule`         | 하나의 설계 규칙을 나타내는 인터페이스입니다.
`ArchitectureRule<T>`       | 제네릭을 활용해 특정 타입에 대해 조건을 검사합니다.
`ArchitectureRuleAdapter`   | ArchUnitNET에서 제공하는 DSL 규칙을 래핑해 사용합니다.
`Must`                      | 자주 쓰는 규칙들을 손쉽게 만들 수 있도록 도와주는 유틸입니다.


### 규칙 검증 주요 메서드 (Must 정적 메서드)

메서드 이름                    | 설명
----------------------------- | -------------------------
`HaveConstructorAllMatches`   | 생성자가 **모두 특정 조건**을 만족해야 함
`HaveConstructorAnyMatches`   | 생성자 중 **하나라도 조건**을 만족하면 됨
`HaveNamedFieldMatches`       | 이름으로 **특정 필드 조건** 검사
`HaveNamedMethodMatches`      | 이름으로 **특정 메서드 조건** 검사
`HaveNamedNestedClassMatches` | 이름으로 **특정 중첩 클래스 조건** 검사

<br/>

## 예제
### 생성자 규칙
```cs
// 생성자는 모두 private이어야 함
Must.HaveConstructorAllMatches(provider, Must.IsPrivateConstructor)
```

### 메서드 규칙
```cs
// Create, Validate 메서드는 public static 이어야 함
Must.HaveNamedMethodMatches(provider,
  // Create 메서드 이름의 설계 규칙: Must.IsPublicStaticMethod)
  ("Create", Must.IsPublicStaticMethod),

  // Validate 메서드 이름의 설계 규칙: Must.IsPublicStaticMethod)
  ("Validate", Must.IsPublicStaticMethod)
)
```

### ArchUnitNET 규칙 통합
```cs
IList<IArchitectureRule> sut = [
  provider
    .Should().BePublic()          // ArchUnitNET
    .AndShould().BeAbstract()
    .AndShould().BeSealed()
    .AndShould().NotBeNested()
    .ToArchitectureRule()         // IArchitectureRule 변환
];
```

<br/>

## 적용 예제
### Value Object 설계 규칙
- IValueObject를 구현한 클래스들이 설계 규칙을 지키는지 확인하는 테스트입니다.

```cs
[Fact]
public void ValueObject_ShouldSatisfy_DesignRules()
{
  // 규칙을 적용할 클래스 대상: IValueObject 인터페이스를 구현한 클래스
  var provider = ArchRuleDefinition
      .Classes()
      .That()
      .ImplementInterface(typeof(IValueObject));

  // 검사할 규칙 목록
  List<IArchitectureRule> rules = [
    // 1. 클래스는 public + sealed
    provider.Should().BePublic().AndShould().BeSealed().ToArchitectureRule(),

    // 2. 매개변수 없는 private 생성자 하나라도 있어야 함
    Must.HaveConstructorAnyMatches(provider, Must.IsPrivateParameterlessConstructor),

    // 3. 모든 생성자가 private 이어야 함
    Must.HaveConstructorAllMatches(provider, Must.IsPrivateConstructor),

    // 4. Create, Validate 메서드는 public static 이어야 함
    Must.HaveNamedMethodMatches(provider,
        ("Create", Must.IsPublicStaticMethod),
        ("Validate", Must.IsPublicStaticMethod)
    )
  ];

  // 규칙을 실제 서비스 어셈블리(ServiceArchitecture)에 적용해서 검사
  var results = rules.Evaluate(ServiceArchitecture);

  // 규칙을 어긴 항목이 있으면 테스트 실패 처리
  results.ShouldHaveNoViolationRules();
}
```

### Value Object 설계 규칙 실패 예시

```
Architecture rule violations found (2 rules violated, 3 total violations):

must have a method matching condition 'Create':
  [MyNamespace.MyValueObject]
    - does not satisfy a method condition 'Create'

must have all matching conditions:
  [MyNamespace.MyValueObject]
    - does not satisfy all constructor conditions
```