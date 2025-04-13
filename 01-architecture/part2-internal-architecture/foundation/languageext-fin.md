---
outline: deep
---

# 성공/실패 타입 Fin&lt;T&gt;

- language-ext 패키지
- Fin&lt;T&gt 추상 타입을 통해 성공/실패을 처리합니다.
  - 실패는 Error 타입으로 지정되어 있어 명시적으로 실패 타입을 지정할 필요가 없습니다.

## 클래스 구조
```cs
// Fin 클래스: 성공 및 실패 부모 클래스스
public abstract class Fin<A> : ...

// Succ 클래스: 성공 A 값
public sealed class Succ<A>(A Value) : Fin<A>

// Fail 클래스: 실패 Error 값
public sealed class Fail<A>(Error Error) : Fin<A>
```

## 생성
### 성공 생성
```cs
// 암시적 성공 생성: operator
//  - Fin<int> succ = 1;
//  - Fin<Foo> succ = new Foo();
[Pure, MethodImpl(Opt.Default)]
public static implicit operator Fin<A>(A value) =>
    new Fin.Succ<A>(value);

// 암시적 성공 생성: operator
//  -
//  -
[Pure, MethodImpl(Opt.Default)]
public static implicit operator Fin<A>(Pure<A> value) =>
    new Fin.Succ<A>(value.Value);

// 명시적 성공 생성
//  - Fin<int> succ = Fin<int>.Succ(1);
//  - Fin<int> succ = Fin<Foo>.Succ(new Foo());
[Pure, MethodImpl(Opt.Default)]
public static Fin<A> Succ(A value) =>
    new Fin.Succ<A>(value);
```

### 실패 생성
```cs
// 암시적 실패 생성: operator
//  - Fin<int> fail = Error.New("실패");
//  - Fin<Foo> fail = Error.New("실패");
[Pure, MethodImpl(Opt.Default)]
public static implicit operator Fin<A>(Error error) =>
    new Fin.Fail<A>(error);

// 암시적 실패 생성: operator
//  - Fin<int> fail = Fail...
//  - Fin<Foo> fail = ...
[Pure, MethodImpl(Opt.Default)]
public static implicit operator Fin<A>(Fail<Error> value) =>
    new Fin.Fail<A>(value.Value);

// 명시적 실패 생성
//  - Fin<int> fail = Fin<int>.Fail(Error.New("실패"));
//  - Fin<Foo> fail = Fin<Foo>.Fail(Error.New("실패"));
[Pure, MethodImpl(Opt.Default)]
public static Fin<A> Fail(Error error) =>
    new Fin.Fail<A>(error);

// 명시적 실패 생성
//  - Foo
//  - 
[Pure, MethodImpl(Opt.Default)]
public static Fin<A> Fail(string error) =>
    new Fin.Fail<A>(Error.New(error));
```

### Many 실패 생성
```cs
// 명시적 실패 생성
//  - Fin<Foo> fail = Error.Many(Error.New("실패 1"), Error.New("실패 2"));
[Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
public static Error Many(params Error[] errors) =>
    errors.Length == 0
        ? Errors.None
        : errors.Length == 1
            ? errors[0]
            : new ManyErrors(errors.AsIterable().ToSeq());
```

## 값 접근
### 성공 T 값
```cs
//  - int value = (int)succ;        // Fin<int>
//  - Foo value = (Foo)succ;        // Fin<Foo>

public abstract class Fin<A> : ...
{
    [Pure, MethodImpl(Opt.Default)]
    public static explicit operator A(Fin<A> ma) =>
        ma.SuccValue;

    internal abstract A SuccValue { get; }
}

public sealed class Succ<A>(A Value) : Fin<A>
{
    internal override A SuccValue =>
        Value;
}
```

### 실패 Error 값
```cs
//  - Error value = (Erorr)fail;        // Fin<int>
//  - Error value = (Error)fail;        // Fin<Foo>

public abstract class Fin<A> : ...
{
    [Pure, MethodImpl(Opt.Default)]
    public static explicit operator Error(Fin<A> ma) =>
        ma.FailValue;

    internal abstract Error FailValue { get; }
}

public sealed class Fail<A>(Error Error) : Fin<A>
{
    internal override Error FailValue =>
        Error;
}
```