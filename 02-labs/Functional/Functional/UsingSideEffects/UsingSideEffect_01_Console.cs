using LanguageExt;
using LanguageExt.Sys;
using LanguageExt.Sys.Traits;
using LanguageExt.Traits;

using static LanguageExt.Prelude;

namespace Functional.UsingSideEffects;

public class UsingSideEffect_01_Console
{
    //// ----------------------------------------------------------
    //// Case 1. 기본 구성
    //// ----------------------------------------------------------

    //public static Eff<RT, Unit> main<RT>() where RT : Has<Eff<RT>, ConsoleIO> =>
    //    repeat(
    //        from l in Console<RT>.readLine
    //        from _ in Console<RT>.writeLine(l)
    //        select unit)
    //    .As();

    //// ----------------------------------------------------------
    //// Case 2. Fluet: .ToRepeatIO() 왼쪽에서 오른쪽으로 읽기
    //// ----------------------------------------------------------

    //public static Eff<RT, Unit> main<RT>() where RT : Has<Eff<RT>, ConsoleIO> =>
    //    (
    //        from l in Console<RT>.readLine
    //        from _ in Console<RT>.writeLine(l)
    //        select unit
    //    )
    //    .RepeatIO()
    //    .As();

    //// ----------------------------------------------------------
    //// Case 3. 순수 함수 통합: let
    ////    - SuccessEff
    ////    - FailEff
    ////    - unitEff
    //// ----------------------------------------------------------

    //public static Eff<RT, Unit> main<RT>() where RT : Has<Eff<RT>, ConsoleIO> =>
    //    (
    //        from l in Console<RT>.readLine
    //            //from v in SuccessEff(Capitalise(l))
    //        let v = Capitalise(l)
    //        from _ in Console<RT>.writeLine(v)
    //        select unit
    //    )
    //    .RepeatIO()
    //    .As();

    //private static string Capitalise(string text) =>
    //    new string(text.Select(x => char.IsLower(x) ? char.ToUpper(x) : x)
    //                   .ToArray());

    //// ----------------------------------------------------------
    //// Case 4. 불순 함수 통합: ToEff(Error.New 실패 메시지)
    //// ----------------------------------------------------------

    //public static Eff<RT, Unit> main<RT>() where RT : Has<Eff<RT>, ConsoleIO> =>
    //    (
    //        from l in Console<RT>.readLine
    //        from v in readInteger(l)
    //        from _ in Console<RT>.writeLine($"{v}")
    //        select unit
    //    )
    //    .RepeatIO()
    //    .As();

    //static Eff<int> readInteger(string line) =>
    //    parseInt(line)
    //        .ToEff(Error.New("Integers only please"));
    //        //.ToFin(Error.New("Integers only please"));

    //// ----------------------------------------------------------
    //// Case 5.
    ////  - SuccessEff
    ////  - FailEff
    ////  - unitEff
    //// ----------------------------------------------------------

    //public static Eff<RT, Unit> main<RT>() where RT : Has<Eff<RT>, ConsoleIO> =>
    //    (
    //        from l in Console<RT>.readLine
    //        from v in l == "" ? FailEff<Unit>(Error.New("User exited"))
    //                          : unitEff
    //        from _ in Console<RT>.writeLine(l)
    //        select unit
    //    )
    //    .RepeatIO()
    //    .As();

    //// ----------------------------------------------------------
    //// Case 6.1 guard(거짓이면 실패, Error)
    //// ----------------------------------------------------------

    //public static Eff<RT, Unit> main<RT>() where RT : Has<Eff<RT>, ConsoleIO> =>
    //    (
    //        from l in Console<RT>.readLine
    //        from v in guard(l != "", Error.New("User exited"))
    //        from _ in Console<RT>.writeLine(l)
    //        select unit
    //    )
    //    .RepeatIO()
    //    .As();

    //// ----------------------------------------------------------
    //// Case 6.2 guardnot(참이면 실패, Error)
    //// ----------------------------------------------------------

    //public static Eff<RT, Unit> main<RT>() where RT : Has<Eff<RT>, ConsoleIO> =>
    //    (
    //        from l in Console<RT>.readLine
    //        from v in guardnot(l == "", Error.New("User exited"))
    //        from _ in Console<RT>.writeLine(l)
    //        select unit
    //    )
    //    .RepeatIO()
    //    .As();



    // TODO

    //public static Eff<RT, Unit> AskUser<RT>() where RT : Has<Eff<RT>, ConsoleIO> =>
    //    (
    //        from l in Console<RT>.readLine
    //        from v in guardnot(l == "", Error.New("User exited"))
    //        from _ in Console<RT>.writeLine(l)
    //        select unit
    //    )
    //    .RepeatIO()
    //    .As();

    //public static Eff<RT, Unit> main<RT>() where RT : Has<Eff<RT>, ConsoleIO> =>
    //    from r in AskUser<RT>().Match(Succ: x => "success",
    //                                  Fail: x => "failure")
    //    from _ in Console<RT>.writeLine(r)
    //    select unit;

    //public static Eff<RT, Unit> main2<RT>() where RT : Has<Eff<RT>, ConsoleIO> =>
    //    AskUser<RT>().IfFailEff(e => Console<RT>.writeLine($"{e}"));

    // SuccessEff
    // FailEff
    // unitEff
    // .Match
    // .IfFailEff
}

//public class AffTests<RT>
//    where RT : Has<Eff<RT>, ConsoleIO>
//{
//    static readonly Error UserExited = Error.New(100, "user exited");
//    static readonly Error SafeError = Error.New(200, "there was a problem");

//    public static Eff<RT, Unit> main =>
//        from _1 in askUser
//                      | @catch(ex => ex is SystemException, Console<RT>.writeLine("system error >>>>"))
//                      | SafeError
//        from _2 in Console<RT>.writeLine("goodbye")
//        select unit;

//    static Eff<RT, Unit> askUser =>
//        repeat(from ln in Console<RT>.readLine
//               from _1 in guard(notEmpty(ln), UserExited)
//               from _2 in guardnot(ln == "sys", () => throw new SystemException())
//               from _3 in guardnot(ln == "err", () => throw new Exception())
//               from _4 in Console<RT>.writeLine(ln)
//               select unit).As()
//        | @catch(UserExited, pure<Eff<RT>, Unit>(unit));
//}
