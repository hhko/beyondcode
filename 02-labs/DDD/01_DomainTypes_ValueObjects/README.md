# 의미 있는 "값 객체" 타입 설계

> "우리가 만든 타입이 곧 도메인입니다."

실마리를 찾기 위해 단순한 나눗셈 함수를 출발점으로, **의미 있는 값 객체(Value Object)**를 어떻게 점진적으로 발전시켜 나갈 수 있는지 함께 알아보려 합니다.
- 질문: 코드를 설계할 때 도메인 규칙을 어떻게 자연스럽게 반영할 수 있을까?
- 가정: 값 하나에도 의미를 부여할 수 있다면, 우리의 코드도 도메인을 이야기할 수 있지 않을까?
  - 우리가 만든 타입으로 도메인을 표현할 수 있지 않을까?
 - 우리가 만든 타입이 곧 도메인이다

## 목차
1. [시작은 단순한 나눗셈에서](#1-시작은-단순한-나눗셈에서)
2. [예외를 예방하려는 우리의 첫 시도](#2-예외를-예방하려는-우리의-첫-시도)
3. [실패도 값이다](#3-실패도-값이다)
4. [그 값이 문제라면, 애초에 들어오지 못하게 하자 (Always Valid)](#4-그-값이-문제라면-애초에-들어오지-못하게-하자-always-valid)
5. [코드의 표현력을 더 풍부하게 (연산자 오버로딩)](#5-코드의-표현력을-더-풍부하게-연산자-오버로딩)
6. [값이 같다면 객체도 같아야 한다 (값의 동등성)](#6-값이-같다면-객체도-같아야-한다-값의-동등성)
7. [객체의 책임을 분리하자 (단일 책임 원칙: SRP)](#7-객체의-책임을-분리하자-단일-책임-원칙-srp)
8. [설계 규칙을 자동으로 검증하기](#8-설계-규칙을-자동으로-검증하기)
9. [타입으로 말하는 도메인](#9-타입으로-말하는-도메인)


<br/>

## 1. 시작은 단순한 나눗셈에서
누구나 처음 프로그래밍을 배우면서 Divide라는 이름의 함수를 만들어본 경험이 있을 것이다. 두 개의 정수를 입력받아 나눗셈을 수행하는 함수. 아래와 같은 아주 간단한 형태 말이다.

```cs
public int Divide(int numerator, int denominator)
{
  return numerator / denominator;
}
```

코드만 보면 아무런 문제가 없어 보인다. 입력받은 두 정수를 그대로 나눠 결과를 반환한다. 하지만 우리는 곧 이 함수가 위험하다는 사실을 깨닫는다. 왜일까? 바로, 두 번째 매개변수인 `denominator`가 0일 경우, 프로그램은 `DivideByZeroException`이라는 예외를 발생시키며 종료되기 때문이다.  

이런 함수를 우리는 **비결정적(Non-deterministic)이고, 부작용이 있는 함수라고** 부릅니다. 입력에 따라 항상 같은 결과를 주지 않고, 때때로 프로그램을 종료시켜 버릴 수도 있으니까요.  

초보 개발자들은 이런 예외 상황을 처음 맞닥뜨릴 때 당황한다. 그리고 이 경험을 통해 하나의 교훈을 얻게 된다. **“입력값은 항상 의심하라”는** 것입니다.

<br/>

## 2. 예외를 예방하려는 우리의 첫 시도
그래서 대부분의 초보 개발자가 처음 배우는 개선 방법은 "방어적 프로그래밍"입니다. 0인지 먼저 확인하고 예외를 던지도록 바꾸는 것입니다:

대부분의 개발자들이 처음으로 배우는 패턴이 바로 **"방어적 프로그래밍(defensive programming)"이다.** 값이 0인지 미리 확인하고, 문제가 있다면 예외를 던지는 방식이다.

```cs
if (denominator == 0)
  throw new ArgumentException("0으로 나눌 수 없습니다");
```

이제 코드가 갑자기 종료되지는 않습니다. 하지만 이 방식에도 문제가 있습니다. 이 함수는 여전히 예외를 사용해 실패를 표현하고 있습니다. 호출자 입장에서는 `try-catch` 문으로 감싸지 않으면, 여전히 프로그램이 멈출 수 있습니다. 게다가 호출자가 이 함수를 사용할 때마다 "혹시 0일지도 모르니 조심해야지" 하고 일일이 예외를 처리해야 합니다.  

말 그대로 **'예외적인 상황'이 아닌, '예상 가능한 실패'를 예외로 다루고** 있는 셈입니다.

<br/>

## 3. 실패도 값이다
함수형 프로그래밍에서는 실패를 예외로 다루지 않습니다. 대신 성공과 실패를 모두 하나의 값으로 표현합니다. 이러한 방식은 코드의 흐름을 더 예측 가능하게 만들며, 테스트와 유지보수에 강점을 제공합니다.  

예를 들어, `Fin<T>`라는 결과 타입을 사용하면 다음과 같이 함수를 개선할 수 있습니다.

```cs
public Fin<int> Divide(int numerator, int denominator)
{
    if (denominator == 0)
        return Error.New("0은 허용되지 않습니다");

    return numerator / denominator;
}
```

이 함수는 이제 항상 같은 형태의 결과를 반환합니다. 입력 값이 올바르면 `Fin<int>.Succ(`결과`)`를, 실패했다면 `Fin<int>.Fail(`오류`)`를 반환합니다. 예외를 발생시키지 않고, 실패를 타입 시스템의 일부로서 다룰 수 있게 된 것입니다.  

호출자도 예외가 아닌 값을 통해 오류를 다루기 때문에, 흐름을 예측하고 테스트하기 훨씬 수월해집니다.

<br/>

## 4. 그 값이 문제라면, 애초에 들어오지 못하게 하자 (Always Valid)
이쯤에서 한 가지 질문이 또오릅니다.

> "왜 우리는 매번 denominator가 0인지 확인해야 할까?"

반대로 생각해보시다. 처음부터 0이 들어올 수 없게 만들면 되지 않을까요?

바로 이 지점에서 우리는 도메인 주도 설계(`Domain-Driven Design`)의 핵심 개념 중 하나인 **"의미 있는 타입(Value Object)"을** 만나게 됩니다. 단순한 정수가 아니라, 0이 아닌 정수만을 표현하는 타입을 새롭게 정의하는 것입니다.  

이제 `NonZeroInt`라는 타입을 정의해볼 수 있습니다. 이 타입은 0이 아닌 정수만을 나타내며, 생성자에서 유효성 검사를 수행하여 0인 값이 들어오지 못하도록 차단합니다.

```cs
public static Fin<NonZeroInt> Create(int value) =>
    value == 0
        ? Error.New("0은 허용되지 않습니다")
        : new NonZeroInt(value);
```

이제 나눗셈 함수는 이렇게 바뀝니다

```cs
public int Divide(int numerator, NonZeroInt denominator)
{
  return numerator / denominator.Value;
}
```

놀랍게도, 이 함수 내부에서는 더 이상 0인지 확인하지 않아도 됩니다. 왜냐하면 `NonZeroInt`라는 타입을 만든 시점에서 이미 0이 아닌 값만 생성되도록 보장했기 때문입니다.  

이것이 바로 타입을 이용한 도메인 제약 조건의 내재화입니다. 즉, 타입 그 자체가 규칙을 담고 있으며, 나머지 로직은 그 규칙을 신뢰해도 된다는 뜻입니다.

<br/>

## 5. 코드의 표현력을 더 풍부하게 (연산자 오버로딩)

```cs
return numerator / denominator.Value;
```

다만 아직 한 가지 개선할 점이 있습니다. `denominator.Value`처럼 내부 값을 꺼내는 방식은 캡슐화를 위반하고 코드의 직관성을 떨어뜨릴 수 있습니다. 그래서 연산자 오버로딩을 도입해볼 수 있습니다.

```cs
public static int operator /(int numerator, NonZeroInt denominator) =>
  numerator / denominator.Value;
```

이제 Divide 함수는 이렇게 쓸 수 있다.

```cs
public int Divide(int numerator, NonZeroInt denominator)
{
  return numerator / denominator;
}
```

겉보기엔 여전히 단순한 나눗셈처럼 보이지만, **내부적으로는 “0이 아님”이라는 도메인 규칙이** 보장된 안전한 연산이 수행되고 있는 것입니다.

<br/>

## 6. 값이 같다면 객체도 같아야 한다 (값의 동등성)
의미 있는 타입을 정의할 때 또 하나 고려해야 할 점이 있습니다. 바로 **값의 동등성(Equality)입니다.** 일반적인 객체는 메모리 주소가 같아야 같다고 판단하지만, `NonZeroInt`와 같은 값 객체는 “값이 같으면 같다”고 봐야 합니다.

이를 위해 `Equals`, `GetHashCode`, `==`, `!=` 연산자 등을 적절히 구현해야 합니다. 이렇게 하면 테스트 코드에서 두 값을 비교하거나 컬렉션에서 동등한 항목을 찾을 때에도 일관된 결과를 얻을 수 있습니다.

```cs
public readonly struct NonZeroInt6
  : IEquatable<NonZeroInt6>
{
  // 동등성: IEquatable<T>
  bool IEquatable<NonZeroInt6>.Equals(NonZeroInt6 other) =>
      Value == other.Value;

  // 동등성: object 기본 구현 재정의(overriding)
  public override bool Equals(object? obj) =>
      obj is NonZeroInt6 other && Equals(other);

  public override int GetHashCode() =>
      Value.GetHashCode();

  // 동등성: 비교 연산자 오버로드(operator overloading)
  // 1. NonZeroInt <-> NonZeroInt 비교
  //      x == y
  public static bool operator ==(NonZeroInt6 left, NonZeroInt6 right) =>
      left.Equals(right);

  public static bool operator !=(NonZeroInt6 left, NonZeroInt6 right) =>
      !(left == right);

  // 2. NonZeroInt == int 비교
  //      x == 6
  public static bool operator ==(NonZeroInt6 left, int right) =>
      left.Value == right;

  public static bool operator !=(NonZeroInt6 left, int right) =>
      !(left == right);

  // 3. int == NonZeroInt 비교
  //      6 == x
  public static bool operator ==(int left, NonZeroInt6 right) =>
      left == right.Value;

  public static bool operator !=(int left, NonZeroInt6 right) =>
      !(left == right);
```

<br/>

## 7. 객체의 책임을 분리하자 (단일 책임 원칙: SRP)
이제 `NonZeroInt` 타입은 점점 더 진화합니다. 우리는 다음 세 가지 책임을 분리합니다.

- 유효성 검사 로직: `Validate`
- 값 객체 생성: `Create`
- 규칙 정의: `Errors` 클래스

시간이 흐르면 `NonZeroInt` 타입도 점점 진화하게 된다. 유효성 검사, 객체 생성, 에러 메시지 관리 등 다양한 책임이 생기기 때문입니다.
이럴 때 **단일 책임 원칙(Single Responsibility Principle)을** 적용하여 각 역할을 나눠야 합니다.

- **Validate 메서드**: 값이 유효한지 확인
- **Create 메서드**: 유효하면 객체를 생성
- **Error 클래스**: 오류 메시지를 중앙 집중화

이러한 분리는 테스트를 쉽게 만들고, 변경에 유연하게 대처할 수 있는 구조를 만듭니니다.

```cs
public sealed partial class NonZeroInt8Errors
{
  public static Error Invalid() =>
    Error.New("0은 허용되지 않습니다");
}

public readonly partial struct NonZeroInt7
  : IEquatable<NonZeroInt7>
  , IValueObject7
{
  public static Fin<NonZeroInt7> Create(int value)        // SRP: 객체 생성
  {
    return Validate(value)
      .CreateValueObject(() => new NonZeroInt7(value));
  }

  public static Error Validate(int value)                 // SRP: 유소성 검사
  {
    return Error.Empty
      .If(value == 0, NonZeroInt7Errors.Invalid());       // SRP: 유효성 검사 규칙
  }
```

이렇게 하면 코드 재사용성과 테스트 용이성이 모두 향상됩니다. 예외 메시지 등도 Errors 클래스 한곳에서 관리할 수 있어 유지보수가 쉬워집니다.

<br/>

## 8. 설계 규칙을 자동으로 검증하기
이제 수많은 값 객체를 만들게 될 것입니다. 그때마다 설계를 하나하나 눈으로 검토하는 것은 매우 번거로운 일입니다.  

그래서 테스트 코드로 설계 규칙을 자동으로 검증할 수 있어야 합니다. 예를 들어 아래와 같은 조건들을 검사합니다.

- `sealed class`인가?
- `private constructor`인가?
- `Create`, `Validate` 메서드를 가졌는가?

```cs
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
```

이런 방식으로 설계 일관성을 유지할 수 있으며, 실수가 생길 여지를 줄일 수 있습니다.

<br/>

## 9. 타입으로 말하는 도메인
단순한 나눗셈 함수에서 출발해, 도메인을 반영하는 의미 있는 타입 `NonZeroInt`를 만들어보았습니다. 이 과정을 통해 다음과 같은 개선을 거쳤습니다:

- **부작용 없는 순수 함수로 전환**
  - 개선 전의 `Divide(int x, int y)`는 `y == 0`일 때 예외가 발생하는 비결정적이며 예외 유발 가능성이 있는 함수입니다.
  - 개선 후 `int Divide(int x, NonZeroInt y)`는 입력 단계에서 오류 가능성을 제거하고, 함수 자체는 입력에 대해 항상 동일한 결과를 보장하는 **순수 함수(Pure Function)로** 전환되었습니다.
- **도메인 타입(Value Object) 도입**
  - `NonZeroInt`는 단순한 `int`가 아닌, "0이 아님"이라는 비즈니스 규칙을 포함한 의미 있는 타입입니다.
  - 이는 도메인 지식이 코드에 직접 반영되는 형태로, 도메인 주도 설계에서 말하는 **도메인 언어(Ubiquitous Language)를** 구체적으로 구현합니다.
  - `NonZeroInt`를 사용함으로써, 입력 유효성 검사(validate)를 객체 생성 시점으로 위임하여 이후 비즈니스 로직에서의 복잡성을 줄일 수 있습니다.
- **명확한 오류 처리 (Fin 타입 도입)**
  - `Fin<int>`는 성공 또는 실패를 표현할 수 있는 타입으로, 예외(Exception) 기반이 아닌 **값 기반의 오류 표현(Value-based error handling)을** 제공합니다.
  - 이로 인해 에러의 존재가 타입에 명시적으로 드러나며, 호출자 측에서도 이를 컴파일 타임에 인지하고 대응할 수 있습니다.

이러한 방식은 도메인 주도 설계와 함수형 프로그래밍이 만나는 지점에서 빛을 발합니다. 도메인 타입(`NonZeroInt`)은 단순한 데이터가 아니라, 비즈니스 규칙을 담고 있는 살아있는 도메인 언어가 됩니다.  

> `NonZeroInt` 클래스 이름과 행위 그리고 상태까지 비즈니스 규칙이 됩니다. 코드가 곧 문서이고, 코드가 곧 도메인 언어가 됩니다.  

이 과정은 **의미 있는 타입 설계, 입력 유효성 보장, 오류 처리의 명확화, 도메인 언어 강화** 등을 아우르며, 유지보수성과 안정성이 높은 코드를 구성하는 기반을 마련합니다.  

지금 개발 중인 도메인에서 규칙이 반복적으로 등장하는 값은 무엇인가요? `Value Object` 코드는 더 안전해지고, 명확해지고, 의미를 갖는 코드 개발을 유도하게 될 것입니다.