
using LanguageExt.Common;

Error r = Error.New("hello");
Expected x = new Expected("xyz", 101);

r.Append(x);

int x2 = 2;
// public abstract record   Error : Monoid<Error>
// public record            Expected(string Message, int Code, Option<Error> Inner = default) : Error
// public record            Exceptional(string Message, int Code) : Error
// public sealed record     BottomError() : Exceptional(BottomException.Default)
// public sealed record     ManyErrors([property: DataMember] Seq<Error> Errors) : Error

//Exceptional - 예기치 않은 오류
//  OutOfMemoryException은 예외적이며, 절대 발생해서는 안 되는 오류로 취급해야 합니다.
//  예외적인 오류가 발생하면 즉시 중단하고 싶어 합니다.
//Expected - 예상된 오류
//  "사용자를 찾을 수 없음" 오류는 예외적인 것이 아니라 충분히 예상할 수 있는 오류입니다.
//  예상된 오류에 대해 합리적인 처리를 원합니다.
//BottomError
//  논리적으로 발생할 수 없는 오류를 나타내는 특별한 Error 타입입니다.
//ManyErrors - 다수의 오류(0개 이상)


// BottomError
//
//1. 오류의 "바닥(bottom)"을 의미
//   - 어떤 상황에서도 발생해서는 안 되는 오류를 나타내며, Error 타입의 기본값(default) 역할을 하기도 합니다.
//   - 즉, 코드에서 이 오류가 나오면 "여기에 도달하면 안 된다"는 의미를 가집니다.
//2. 주로 기본값 및 초기화 용도로 사용
//   - Error를 반환해야 하지만 아직 특정한 오류가 정해지지 않았을 때 BottomError를 사용할 수 있습니다.
//   - 예를 들어, Error를 처리하는 함수에서 기본적으로 BottomError를 반환하고, 실제 오류가 발생하면 다른 Error 타입으로 대체하는 방식입니다.
//3. 패턴 매칭에서 "처리되지 않은 오류"를 잡아낼 때 유용
//   - BottomError를 사용하여 예상되지 않은 오류가 발생했을 때 감지할 수 있습니다.


//IsExceptional – 예외적인 오류라면 true를 반환합니다. (ManyErrors의 경우, 하나라도 예외적이면 true)
//IsExpected – 예상된 오류라면 true를 반환합니다. (ManyErrors의 경우, 모두 예상된 오류여야 true)
//Is<E>(E exception) – Error가 예외적이며, 내부 Exception 중 하나가 E 타입이면 true를 반환합니다.
//Is(Error error) – 주어진 Error와 일치하면 true를 반환합니다. (error.Is(Errors.TimedOut))



// Error.Many(Error.New("error one"), Error.New("error two"));
// Error.New("error one") + Error.New("error two");

//사용자 정의 오류 타입 생성
//public record BespokeError(bool MyData)
//    : Expected("Something bespoke", 100, None);

//직렬화 가능한 사용자 정의 오류
//public record BespokeError([property: DataMember] bool MyData)
//    : Expected("Something bespoke", 100, None);
