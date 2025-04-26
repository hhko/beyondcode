using LanguageExt;
using LanguageExt.Common;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace FunctionalDdd.Framework.BaseTypes;

public static class FinUtilities
{
    public static Fin<T> CombineErrors<T>(this Fin<T> fin, Error newError)
    {
        return fin.MapFail(error => error.Combine(newError));
    }
}

public static class ErrorUtilities
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
                error = error.Combine(error);
            }
        }

        return error;
    }

    public static Fin<TValueObject> CreateValueObject<TValueObject>(
        this Error error,
        Func<TValueObject> createValueObject) where TValueObject : IValueObject
    {
        if (!error.IsEmpty)
            return error;

        return createValueObject();
    }
}

//public static class ErrorCode
//{
//    // -                    -1000
//    // Failure,
//    // Unexpected,
//    // Validation,          -1001
//    // Conflict,
//    // NotFound,
//    // Unauthorized,
//    // Forbidden,

//    // 패턴                       예시                                          쓰임새
//    // 형용사 + 명사(상태 + 대상)  ExpiredToken, InvalidPassword, MissingField 객체의 상태 설명
//    // 명사 + 과거분사(대상 + 변화) TokenExpired, SessionClosed, UserLocked 변화 또는 이벤트
//    //
//    // 예. ExpiredSessionToken: "만료된 세션 토큰"이라는 객체를 설명하는 데 적합
//    // 예. SessionTokenExpired: "세션 토큰이 만료됐다"는 사건을 설명하는 데 적합

//    //"대상 + 상태" (Noun + Adjective/Verb): 대상이 주어가 되고 상태가 그 결과인 경우 → 이 어순이 자연스러움, “대상”에 무슨 일이 일어났는지 설명
//    //"상태 + 대상" (Adjective + Noun): 상태가 속성이나 특성으로 쓰일 때는 형용사 + 명사 구조가 더 자연스러움, "무언가를 확인했는데, 그게 형식이나 내용상 틀림"
//    //대상 + 상태/원인
//    //```
//    //// 대상 + 상태
//    //UserNotFound = "User is not found"
//    //SessionAlreadyExists = "Session already exists"
//    //FileDeleted = "File has been deleted"

//    //// 상태 + 대상
//    //InvalidToken = "Token is invalid"
//    //MissingField = "Field is missing"
//    //```

//    // 대상 + NotFound
//    // 대상 + AlreadyExists       : Conflict
//    // 
//    // 대상 + Exceeded            : 제한 초과
//    // 대상 + Timeout             : 시간 초과
//    // 대상 + Unavailable         : 서비스/리소스 이용 불가
//    //
//    // 대상 + Unauthorized        : 인증되지 않음
//    // 대상 + Forbidden           : 권한이 없음

//    // Invalid + 대상
//    // Missing + 대상


//    // 기준              | Validation                 | Operation
//    // 무엇을 검사하는가?	| 사전에 조건을 점검(상태 검증)  | 실제 동작 실행 중 실패
//    // 언제 실패하는가?   | 아직 아무 일도 일어나기 전     | 시스템 또는 도메인 로직 수행 중
//    // 예시              | 이미 예약된 세션인가?         | 예약 시도했지만 시간이 겹쳤다

//    //public static Error Unexpected(string errorCode, string message) =>
//    //    new ExpectedErrorCode(errorCode, -1000, message);

//    public static Error Validation(string errorCode, string message) =>
//        new ExpectedErrorCode(errorCode, -1001, message);

//    public static Error Operation(string errorCode, string message) =>
//        new ExpectedErrorCode(errorCode, -1002, message);
//}

public static class ErrorCodeFactory
{
    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Error Create(string errorCode, string message) =>
        new ExpectedErrorCode(errorCode, -1000, message);

    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Format(params string[] parts) =>
        string.Join('.', parts);
}

[DataContract]
public record ExpectedErrorCode(string ErrorCode, int Code, string Message, Option<Error> Inner = default) : Error
{
    [Pure]
    [DataMember]
    public override string Message { get; } =
        Message;

    [Pure]
    [DataMember]
    public override int Code { get; } =
        Code;

    [Pure]
    [DataMember]
    public string ErrorCode { get; } =
        ErrorCode;


    [Pure]
    [IgnoreDataMember]
    public override Option<Error> Inner { get; } =
        Inner;

    [Pure]
    public override string ToString() =>
        Message;

    [Pure]
    public override ErrorException ToErrorException() =>
        new WrappedErrorExpectedException(this);

    [Pure]
    public override bool HasException<E>() =>
        false;

    [Pure]
    [IgnoreDataMember]
    public override bool IsExceptional =>
        false;

    [Pure]
    [IgnoreDataMember]
    public override bool IsExpected =>
        true;
}
