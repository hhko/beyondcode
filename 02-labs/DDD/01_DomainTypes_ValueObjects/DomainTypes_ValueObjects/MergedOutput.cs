// ===== AssemblyReference.cs =====

namespace DomainTypes_ValueObjects;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

// ===== MergedOutput.cs =====
// ===== AssemblyReference.cs =====

namespace DomainTypes_ValueObjects;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

// ===== Program.cs =====

var y = from x in NonZeroInt5.Create(3)
        select 2025 / x;

int YY = 302;

////Do_Case6_Input_NonZeroInt_Equality();
////Do_Case7_Input_NonZeroInt_SRP();

//void Do_Case6_Input_NonZeroInt_Equality()
//{
//    var b1 = from x in NonZeroInt.Create(6)
//             select x == 6;

//    var b2 = from x in NonZeroInt.Create(6)
//             select x != 6;

//    var b3 = from x in NonZeroInt.Create(6)
//             select 6 == x;

//    var b4 = from x in NonZeroInt.Create(6)
//             select 6 != x;

//    Console.WriteLine($"{b1}, {b2}, {b3}, {b4}");
//}

////void Do_Case7_Input_NonZeroInt_SRP()
////{
////    var result = HandsOnLabs.Case7_Input_NonZeroInt_SRP.NonZeroInt.Create(0);

////    result.IfFail(error =>
////    {
////        Console.WriteLine($"{error}");
////    });
////}

// ===== Using.cs =====
//global using LanguageExt.Traits;
//global using LanguageExt.Effects;
//global using LanguageExt.Pipes;
//global using LanguageExt.Pretty;
//global using LanguageExt.Traits.Domain;

global using LanguageExt;
global using LanguageExt.Common;

// ===== MathOperations1.cs =====
namespace DomainTypes_ValueObjects.Case01;

// Case 1. 기본 구현
internal class MathOperations1
{
    public int Divide(int numerator, int denominator)
    {
        return numerator / denominator;
    }
}

// ===== MathOperations2.cs =====
namespace DomainTypes_ValueObjects.Case02_Output_Throw;

// Case 2. 출력 개선: 명시적 예외로 출력
internal class MathOperations2
{
    public int Divide(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new ArgumentException("0으로 나눌 수 없습니다");

        return numerator / denominator;
    }
}

// ===== MathOperations3.cs =====
namespace DomainTypes_ValueObjects.Case03_Output_Error;

// Case 3. 출력 개선: 예측할 수 없는 예외 대신, 예측 가능한 값으로 반환
internal class MathOperations3
{
    // finite
    public Fin<int> Divide(int numerator, int denominator)
    {
        if (denominator == 0)
            return Error.New("0으로 나눌 수 없습니다");

        return numerator / denominator;
    }
}

// ===== MathOperations4.cs =====

namespace DomainTypes_ValueObjects.Case04_Input_NonZeroInt_AlwaysValid;

// Case 4. 입력 개선
//  - "0이 아님"이라는 비즈니스 규칙을 포함한 의미 있는 타입(Primitive Obsession: NonZeroInt)
//  - 입력 유효성 검사(validate)를 객체 생성 시점으로 위임하여 이후 비즈니스 로직에서의 복잡성을 줄임(Always Valid: Create)
internal partial class MathOperations4
{
    public int Divide(int numerator, NonZeroInt4 denominator)
    {
        return numerator / denominator.Value;
    }
}

// ===== MathOperations5.cs =====

namespace DomainTypes_ValueObjects.Case05_Input_NonZeroInt_InformationHding;

// Case 5. 정보 은닉
//  - 연산자 오버로드(operator overloading)을 통해 값 노출 없이 사용
//  - NonZeroInt는 마치 int처럼 보이지만, 내부적으로는 안전한 규칙을 강제하는 구조
internal partial class MathOperations5
{
    public int Divide(int numerator, NonZeroInt5 denominator)
    {
        // 암시적 타입 변환: 갑 추출
        //int x = denominator;

        // 명시적 타입 변환: 값 추출
        //int x = (int)denominator;

        return numerator / denominator;
    }
}

// ===== MathOperations6.cs =====

namespace DomainTypes_ValueObjects.Case06_Input_NonZeroInt_Equality;

internal partial class MathOperations6
{
    public int Divide(int numerator, NonZeroInt6 denominator)
    {
        return numerator / denominator;
    }
}

// ===== MathOperations7.cs =====
namespace DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP;

internal class MathOperations7
{
    public int Divide(int numerator, NonZeroInt7 denominator)
    {
        return numerator / denominator;
    }
}

// ===== MathOperations8.cs =====
namespace DomainTypes_ValueObjects.Case08_ArchitectureTests;

internal class MathOperations8
{
    public int Divide(int numerator, NonZeroInt8 denominator)
    {
        return numerator / denominator;
    }
}

// ===== NonZeroInt4.cs =====
namespace DomainTypes_ValueObjects.Case04_Input_NonZeroInt_AlwaysValid.ValueObjects;

public readonly struct NonZeroInt4
{
    public int Value { get; }

    private NonZeroInt4(int value)
    {
        Value = value;
    }

    // 유효성 검증 포함된 생성 메서드
    public static Fin<NonZeroInt4> Create(int value) =>
        value == 0
            ? Error.New("0은 허용되지 않습니다")
            : new NonZeroInt4(value);

    public override string ToString() =>
        Value.ToString();
}

// ===== NonZeroInt5.cs =====
namespace DomainTypes_ValueObjects.Case05_Input_NonZeroInt_InformationHding.ValueObjects;

public readonly struct NonZeroInt5
{
    private int Value { get; init; }

    private NonZeroInt5(int value)
    {
        Value = value;
    }

    // 유효성 검증 포함된 생성 메서드
    public static Fin<NonZeroInt5> Create(int value) =>
        value == 0
            ? Error.New("0은 허용되지 않습니다")
            : new NonZeroInt5(value);

    // 타입 변환
    //  - implicit 
    //  - explicit 
    //public static implicit operator int(NonZeroInt x) =>
    //    x.Value;
    public static explicit operator int(NonZeroInt5 x) =>
        x.Value;

    // x / NonZeroInt 연산을 지원하는 operator 오버로드
    public static int operator /(int numerator, NonZeroInt5 denominator) =>
        numerator / denominator.Value;

    public override string ToString() =>
        Value.ToString();
}

// ===== NonZeroInt6.cs =====
namespace DomainTypes_ValueObjects.Case06_Input_NonZeroInt_Equality.ValueObjects;

public readonly struct NonZeroInt6 : IEquatable<NonZeroInt6>
{
    private int Value { get; init; }

    private NonZeroInt6(int value)
    {
        Value = value;
    }

    public static Fin<NonZeroInt6> Create(int value) =>
        value == 0
            ? Error.New("0은 허용되지 않습니다")
            : new NonZeroInt6(value);

    public static explicit operator int(NonZeroInt6 x) =>
        x.Value;

    public static int operator /(int numerator, NonZeroInt6 denominator) =>
        numerator / denominator.Value;

    public override string ToString() =>
        Value.ToString();

    //
    // 동등성
    //  - IEquatable<T>
    //  - object overriding
    //  - operator overloading
    //

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
}

// ===== ErrorExtensions7.cs =====
namespace DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP.Framework;

public static class ErrorExtensions7
{
    public static Error If(
        this Error error,
        bool condition,
        params Error[] errorsToAdd)
    {
        if (condition)
        {
            foreach (Error errorToAdd in errorsToAdd)
            {
                error = error.Combine(errorToAdd);
            }
        }

        return error;
    }

    public static Fin<TValueObject> CreateValueObject<TValueObject>(
        this Error error,
        Func<TValueObject> createValueObject) where TValueObject : IValueObject7
    {
        if (!error.IsEmpty)
            return error;

        return createValueObject();
    }
}

// ===== IValueObject7.cs =====
namespace DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP.Framework;

public interface IValueObject7;

// ===== NonZeroInt7.cs =====

namespace DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP;

public readonly partial struct NonZeroInt7
    : IEquatable<NonZeroInt7>
    , IValueObject7
{
    private int Value { get; init; }

    private NonZeroInt7(int value)
    {
        Value = value;
    }

    // 개선 전
    //public static Fin<NonZeroInt> Create(int value) =>
    //    value == 0
    //        ? Error.New("0은 허용되지 않습니다")
    //        : new NonZeroInt(value);

    // 개선 후
    //  - 유효성 검사: Validate
    //  - 값 객체 생성: Create
    public static Fin<NonZeroInt7> Create(int value)
    {
        return Validate(value)
            .CreateValueObject(() => new NonZeroInt7(value));
    }

    public static Error Validate(int value)
    {
        return Error
            .Empty
            .If(value == 0, NonZeroInt7Errors.Invalid());
    }

    public static explicit operator int(NonZeroInt7 x) =>
        x.Value;

    public static int operator /(int numerator, NonZeroInt7 denominator) =>
        numerator / denominator.Value;

    public override string ToString() =>
        Value.ToString();

    bool IEquatable<NonZeroInt7>.Equals(NonZeroInt7 other) =>
        Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is NonZeroInt7 other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public static bool operator ==(NonZeroInt7 left, NonZeroInt7 right) =>
        left.Equals(right);

    public static bool operator !=(NonZeroInt7 left, NonZeroInt7 right) =>
        !(left == right);

    public static bool operator ==(NonZeroInt7 left, int right) =>
        left.Value == right;

    public static bool operator !=(NonZeroInt7 left, int right) =>
        !(left == right);

    public static bool operator ==(int left, NonZeroInt7 right) =>
        left == right.Value;

    public static bool operator !=(int left, NonZeroInt7 right) =>
        !(left == right);
}

// ===== ErrorExtensions8.cs =====
namespace DomainTypes_ValueObjects.Case08_ArchitectureTests.Framework;

public static class ErrorExtensions8
{
    public static Error If(
        this Error error,
        bool condition,
        params Error[] errorsToAdd)
    {
        if (condition)
        {
            foreach (Error errorToAdd in errorsToAdd)
            {
                error = error.Combine(errorToAdd);
            }
        }

        return error;
    }

    public static Fin<TValueObject> CreateValueObject<TValueObject>(
        this Error error,
        Func<TValueObject> createValueObject) where TValueObject : IValueObject8
    {
        if (!error.IsEmpty)
            return error;

        return createValueObject();
    }
}

// ===== IValueObject8.cs =====
namespace DomainTypes_ValueObjects.Case08_ArchitectureTests.Framework;

public interface IValueObject8
{
    const string CreateMethodName = "Create";
    const string ValidateMethodName = "Validate";
}

// ===== NonZeroInt8.cs =====

namespace DomainTypes_ValueObjects.Case08_ArchitectureTests;

public sealed class NonZeroInt8
    : IEquatable<NonZeroInt8>
    , IValueObject8
{
    private int Value { get; init; }

    private NonZeroInt8(int value)
    {
        Value = value;
    }

    private NonZeroInt8()
    {

    }

    // 개선 전
    //public static Fin<NonZeroInt> Create(int value) =>
    //    value == 0
    //        ? Error.New("0은 허용되지 않습니다")
    //        : new NonZeroInt(value);

    // 개선 후
    //  - 유효성 검사: Validate
    //  - 값 객체 생성: Create
    public static Fin<NonZeroInt8> Create(int value)
    {
        return Validate(value)
            .CreateValueObject(() => new NonZeroInt8(value));
    }

    public static Error Validate(int value)
    {
        return Error
            .Empty
            .If(value == 0, NonZeroInt8Errors.Invalid());
    }

    public static explicit operator int(NonZeroInt8 x) =>
        x.Value;

    public static int operator /(int numerator, NonZeroInt8 denominator) =>
        numerator / denominator.Value;

    public override string ToString() =>
        Value.ToString();

    bool IEquatable<NonZeroInt8>.Equals(NonZeroInt8 other) =>
        Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is NonZeroInt8 other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public static bool operator ==(NonZeroInt8 left, NonZeroInt8 right) =>
        left.Equals(right);

    public static bool operator !=(NonZeroInt8 left, NonZeroInt8 right) =>
        !(left == right);

    public static bool operator ==(NonZeroInt8 left, int right) =>
        left.Value == right;

    public static bool operator !=(NonZeroInt8 left, int right) =>
        !(left == right);

    public static bool operator ==(int left, NonZeroInt8 right) =>
        left == right.Value;

    public static bool operator !=(int left, NonZeroInt8 right) =>
        !(left == right);
}

// ===== NonZeroInt7Errors.Invalid.cs =====
namespace DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP.Errors;

public readonly partial struct NonZeroInt7Errors
{
    public static Error Invalid() =>
        Error.New("0은 허용되지 않습니다");
}

// ===== NonZeroInt8Errors.Invalid.cs =====
namespace DomainTypes_ValueObjects.Case08_ArchitectureTests.Errors;

public sealed partial class NonZeroInt8Errors
{
    public static Error Invalid() =>
        Error.New("0은 허용되지 않습니다");
}

// ===== .NETCoreApp,Version=v8.0.AssemblyAttributes.cs =====
// <autogenerated />
[assembly: global::System.Runtime.Versioning.TargetFrameworkAttribute(".NETCoreApp,Version=v8.0", FrameworkDisplayName = ".NET 8.0")]

// ===== DomainTypes_ValueObjects.AssemblyInfo.cs =====
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


[assembly: System.Reflection.AssemblyCompanyAttribute("DomainTypes_ValueObjects")]
[assembly: System.Reflection.AssemblyConfigurationAttribute("Debug")]
[assembly: System.Reflection.AssemblyFileVersionAttribute("1.0.0.0")]
[assembly: System.Reflection.AssemblyInformationalVersionAttribute("1.0.0+2d392a3511773261339317af40a9e6de1cbb9dad")]
[assembly: System.Reflection.AssemblyProductAttribute("DomainTypes_ValueObjects")]
[assembly: System.Reflection.AssemblyTitleAttribute("DomainTypes_ValueObjects")]
[assembly: System.Reflection.AssemblyVersionAttribute("1.0.0.0")]

// Generated by the MSBuild WriteCodeFragment class.


// ===== DomainTypes_ValueObjects.GlobalUsings.g.cs =====
// <auto-generated/>
global using global::System;
global using global::System.Collections.Generic;
global using global::System.IO;
global using global::System.Linq;
global using global::System.Net.Http;
global using global::System.Threading;
global using global::System.Threading.Tasks;


// ===== Program.cs =====

var y = from x in NonZeroInt5.Create(3)
        select 2025 / x;

int YY = 302;

////Do_Case6_Input_NonZeroInt_Equality();
////Do_Case7_Input_NonZeroInt_SRP();

//void Do_Case6_Input_NonZeroInt_Equality()
//{
//    var b1 = from x in NonZeroInt.Create(6)
//             select x == 6;

//    var b2 = from x in NonZeroInt.Create(6)
//             select x != 6;

//    var b3 = from x in NonZeroInt.Create(6)
//             select 6 == x;

//    var b4 = from x in NonZeroInt.Create(6)
//             select 6 != x;

//    Console.WriteLine($"{b1}, {b2}, {b3}, {b4}");
//}

////void Do_Case7_Input_NonZeroInt_SRP()
////{
////    var result = HandsOnLabs.Case7_Input_NonZeroInt_SRP.NonZeroInt.Create(0);

////    result.IfFail(error =>
////    {
////        Console.WriteLine($"{error}");
////    });
////}

// ===== Using.cs =====
//global using LanguageExt.Traits;
//global using LanguageExt.Effects;
//global using LanguageExt.Pipes;
//global using LanguageExt.Pretty;
//global using LanguageExt.Traits.Domain;

global using LanguageExt;
global using LanguageExt.Common;

// ===== MathOperations1.cs =====
namespace DomainTypes_ValueObjects.Case01;

// Case 1. 기본 구현
internal class MathOperations1
{
    public int Divide(int numerator, int denominator)
    {
        return numerator / denominator;
    }
}

// ===== MathOperations2.cs =====
namespace DomainTypes_ValueObjects.Case02_Output_Throw;

// Case 2. 출력 개선: 명시적 예외로 출력
internal class MathOperations2
{
    public int Divide(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new ArgumentException("0으로 나눌 수 없습니다");

        return numerator / denominator;
    }
}

// ===== MathOperations3.cs =====
namespace DomainTypes_ValueObjects.Case03_Output_Error;

// Case 3. 출력 개선: 예측할 수 없는 예외 대신, 예측 가능한 값으로 반환
internal class MathOperations3
{
    // finite
    public Fin<int> Divide(int numerator, int denominator)
    {
        if (denominator == 0)
            return Error.New("0으로 나눌 수 없습니다");

        return numerator / denominator;
    }
}

// ===== MathOperations4.cs =====

namespace DomainTypes_ValueObjects.Case04_Input_NonZeroInt_AlwaysValid;

// Case 4. 입력 개선
//  - "0이 아님"이라는 비즈니스 규칙을 포함한 의미 있는 타입(Primitive Obsession: NonZeroInt)
//  - 입력 유효성 검사(validate)를 객체 생성 시점으로 위임하여 이후 비즈니스 로직에서의 복잡성을 줄임(Always Valid: Create)
internal partial class MathOperations4
{
    public int Divide(int numerator, NonZeroInt4 denominator)
    {
        return numerator / denominator.Value;
    }
}

// ===== MathOperations5.cs =====

namespace DomainTypes_ValueObjects.Case05_Input_NonZeroInt_InformationHding;

// Case 5. 정보 은닉
//  - 연산자 오버로드(operator overloading)을 통해 값 노출 없이 사용
//  - NonZeroInt는 마치 int처럼 보이지만, 내부적으로는 안전한 규칙을 강제하는 구조
internal partial class MathOperations5
{
    public int Divide(int numerator, NonZeroInt5 denominator)
    {
        // 암시적 타입 변환: 갑 추출
        //int x = denominator;

        // 명시적 타입 변환: 값 추출
        //int x = (int)denominator;

        return numerator / denominator;
    }
}

// ===== MathOperations6.cs =====

namespace DomainTypes_ValueObjects.Case06_Input_NonZeroInt_Equality;

internal partial class MathOperations6
{
    public int Divide(int numerator, NonZeroInt6 denominator)
    {
        return numerator / denominator;
    }
}

// ===== MathOperations7.cs =====
namespace DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP;

internal class MathOperations7
{
    public int Divide(int numerator, NonZeroInt7 denominator)
    {
        return numerator / denominator;
    }
}

// ===== MathOperations8.cs =====
namespace DomainTypes_ValueObjects.Case08_ArchitectureTests;

internal class MathOperations8
{
    public int Divide(int numerator, NonZeroInt8 denominator)
    {
        return numerator / denominator;
    }
}

// ===== NonZeroInt4.cs =====
namespace DomainTypes_ValueObjects.Case04_Input_NonZeroInt_AlwaysValid.ValueObjects;

public readonly struct NonZeroInt4
{
    public int Value { get; }

    private NonZeroInt4(int value)
    {
        Value = value;
    }

    // 유효성 검증 포함된 생성 메서드
    public static Fin<NonZeroInt4> Create(int value) =>
        value == 0
            ? Error.New("0은 허용되지 않습니다")
            : new NonZeroInt4(value);

    public override string ToString() =>
        Value.ToString();
}

// ===== NonZeroInt5.cs =====
namespace DomainTypes_ValueObjects.Case05_Input_NonZeroInt_InformationHding.ValueObjects;

public readonly struct NonZeroInt5
{
    private int Value { get; init; }

    private NonZeroInt5(int value)
    {
        Value = value;
    }

    // 유효성 검증 포함된 생성 메서드
    public static Fin<NonZeroInt5> Create(int value) =>
        value == 0
            ? Error.New("0은 허용되지 않습니다")
            : new NonZeroInt5(value);

    // 타입 변환
    //  - implicit 
    //  - explicit 
    //public static implicit operator int(NonZeroInt x) =>
    //    x.Value;
    public static explicit operator int(NonZeroInt5 x) =>
        x.Value;

    // x / NonZeroInt 연산을 지원하는 operator 오버로드
    public static int operator /(int numerator, NonZeroInt5 denominator) =>
        numerator / denominator.Value;

    public override string ToString() =>
        Value.ToString();
}

// ===== NonZeroInt6.cs =====
namespace DomainTypes_ValueObjects.Case06_Input_NonZeroInt_Equality.ValueObjects;

public readonly struct NonZeroInt6 : IEquatable<NonZeroInt6>
{
    private int Value { get; init; }

    private NonZeroInt6(int value)
    {
        Value = value;
    }

    public static Fin<NonZeroInt6> Create(int value) =>
        value == 0
            ? Error.New("0은 허용되지 않습니다")
            : new NonZeroInt6(value);

    public static explicit operator int(NonZeroInt6 x) =>
        x.Value;

    public static int operator /(int numerator, NonZeroInt6 denominator) =>
        numerator / denominator.Value;

    public override string ToString() =>
        Value.ToString();

    //
    // 동등성
    //  - IEquatable<T>
    //  - object overriding
    //  - operator overloading
    //

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
}

// ===== ErrorExtensions7.cs =====
namespace DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP.Framework;

public static class ErrorExtensions7
{
    public static Error If(
        this Error error,
        bool condition,
        params Error[] errorsToAdd)
    {
        if (condition)
        {
            foreach (Error errorToAdd in errorsToAdd)
            {
                error = error.Combine(errorToAdd);
            }
        }

        return error;
    }

    public static Fin<TValueObject> CreateValueObject<TValueObject>(
        this Error error,
        Func<TValueObject> createValueObject) where TValueObject : IValueObject7
    {
        if (!error.IsEmpty)
            return error;

        return createValueObject();
    }
}

// ===== IValueObject7.cs =====
namespace DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP.Framework;

public interface IValueObject7;

// ===== NonZeroInt7.cs =====

namespace DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP;

public readonly partial struct NonZeroInt7
    : IEquatable<NonZeroInt7>
    , IValueObject7
{
    private int Value { get; init; }

    private NonZeroInt7(int value)
    {
        Value = value;
    }

    // 개선 전
    //public static Fin<NonZeroInt> Create(int value) =>
    //    value == 0
    //        ? Error.New("0은 허용되지 않습니다")
    //        : new NonZeroInt(value);

    // 개선 후
    //  - 유효성 검사: Validate
    //  - 값 객체 생성: Create
    public static Fin<NonZeroInt7> Create(int value)
    {
        return Validate(value)
            .CreateValueObject(() => new NonZeroInt7(value));
    }

    public static Error Validate(int value)
    {
        return Error
            .Empty
            .If(value == 0, NonZeroInt7Errors.Invalid());
    }

    public static explicit operator int(NonZeroInt7 x) =>
        x.Value;

    public static int operator /(int numerator, NonZeroInt7 denominator) =>
        numerator / denominator.Value;

    public override string ToString() =>
        Value.ToString();

    bool IEquatable<NonZeroInt7>.Equals(NonZeroInt7 other) =>
        Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is NonZeroInt7 other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public static bool operator ==(NonZeroInt7 left, NonZeroInt7 right) =>
        left.Equals(right);

    public static bool operator !=(NonZeroInt7 left, NonZeroInt7 right) =>
        !(left == right);

    public static bool operator ==(NonZeroInt7 left, int right) =>
        left.Value == right;

    public static bool operator !=(NonZeroInt7 left, int right) =>
        !(left == right);

    public static bool operator ==(int left, NonZeroInt7 right) =>
        left == right.Value;

    public static bool operator !=(int left, NonZeroInt7 right) =>
        !(left == right);
}

// ===== ErrorExtensions8.cs =====
namespace DomainTypes_ValueObjects.Case08_ArchitectureTests.Framework;

public static class ErrorExtensions8
{
    public static Error If(
        this Error error,
        bool condition,
        params Error[] errorsToAdd)
    {
        if (condition)
        {
            foreach (Error errorToAdd in errorsToAdd)
            {
                error = error.Combine(errorToAdd);
            }
        }

        return error;
    }

    public static Fin<TValueObject> CreateValueObject<TValueObject>(
        this Error error,
        Func<TValueObject> createValueObject) where TValueObject : IValueObject8
    {
        if (!error.IsEmpty)
            return error;

        return createValueObject();
    }
}

// ===== IValueObject8.cs =====
namespace DomainTypes_ValueObjects.Case08_ArchitectureTests.Framework;

public interface IValueObject8
{
    const string CreateMethodName = "Create";
    const string ValidateMethodName = "Validate";
}

// ===== NonZeroInt8.cs =====

namespace DomainTypes_ValueObjects.Case08_ArchitectureTests;

public sealed class NonZeroInt8
    : IEquatable<NonZeroInt8>
    , IValueObject8
{
    private int Value { get; init; }

    private NonZeroInt8(int value)
    {
        Value = value;
    }

    private NonZeroInt8()
    {

    }

    // 개선 전
    //public static Fin<NonZeroInt> Create(int value) =>
    //    value == 0
    //        ? Error.New("0은 허용되지 않습니다")
    //        : new NonZeroInt(value);

    // 개선 후
    //  - 유효성 검사: Validate
    //  - 값 객체 생성: Create
    public static Fin<NonZeroInt8> Create(int value)
    {
        return Validate(value)
            .CreateValueObject(() => new NonZeroInt8(value));
    }

    public static Error Validate(int value)
    {
        return Error
            .Empty
            .If(value == 0, NonZeroInt8Errors.Invalid());
    }

    public static explicit operator int(NonZeroInt8 x) =>
        x.Value;

    public static int operator /(int numerator, NonZeroInt8 denominator) =>
        numerator / denominator.Value;

    public override string ToString() =>
        Value.ToString();

    bool IEquatable<NonZeroInt8>.Equals(NonZeroInt8 other) =>
        Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is NonZeroInt8 other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public static bool operator ==(NonZeroInt8 left, NonZeroInt8 right) =>
        left.Equals(right);

    public static bool operator !=(NonZeroInt8 left, NonZeroInt8 right) =>
        !(left == right);

    public static bool operator ==(NonZeroInt8 left, int right) =>
        left.Value == right;

    public static bool operator !=(NonZeroInt8 left, int right) =>
        !(left == right);

    public static bool operator ==(int left, NonZeroInt8 right) =>
        left == right.Value;

    public static bool operator !=(int left, NonZeroInt8 right) =>
        !(left == right);
}

// ===== NonZeroInt7Errors.Invalid.cs =====
namespace DomainTypes_ValueObjects.Case07_Input_NonZeroInt_SRP.Errors;

public readonly partial struct NonZeroInt7Errors
{
    public static Error Invalid() =>
        Error.New("0은 허용되지 않습니다");
}

// ===== NonZeroInt8Errors.Invalid.cs =====
namespace DomainTypes_ValueObjects.Case08_ArchitectureTests.Errors;

public sealed partial class NonZeroInt8Errors
{
    public static Error Invalid() =>
        Error.New("0은 허용되지 않습니다");
}

// ===== .NETCoreApp,Version=v8.0.AssemblyAttributes.cs =====
// <autogenerated />
[assembly: global::System.Runtime.Versioning.TargetFrameworkAttribute(".NETCoreApp,Version=v8.0", FrameworkDisplayName = ".NET 8.0")]

// ===== DomainTypes_ValueObjects.AssemblyInfo.cs =====
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


[assembly: System.Reflection.AssemblyCompanyAttribute("DomainTypes_ValueObjects")]
[assembly: System.Reflection.AssemblyConfigurationAttribute("Debug")]
[assembly: System.Reflection.AssemblyFileVersionAttribute("1.0.0.0")]
[assembly: System.Reflection.AssemblyInformationalVersionAttribute("1.0.0+2d392a3511773261339317af40a9e6de1cbb9dad")]
[assembly: System.Reflection.AssemblyProductAttribute("DomainTypes_ValueObjects")]
[assembly: System.Reflection.AssemblyTitleAttribute("DomainTypes_ValueObjects")]
[assembly: System.Reflection.AssemblyVersionAttribute("1.0.0.0")]

// Generated by the MSBuild WriteCodeFragment class.


// ===== DomainTypes_ValueObjects.GlobalUsings.g.cs =====
// <auto-generated/>
global using global::System;
global using global::System.Collections.Generic;
global using global::System.IO;
global using global::System.Linq;
global using global::System.Net.Http;
global using global::System.Threading;
global using global::System.Threading.Tasks;

