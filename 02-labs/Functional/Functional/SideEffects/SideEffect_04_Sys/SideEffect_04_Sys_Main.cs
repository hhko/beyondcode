using ConsoleApp1.Adapters.Sys.Sys;
using ConsoleApp1.Adapters.Sys.Traits;
using LanguageExt;
using LanguageExt.Traits;
using static LanguageExt.Prelude;

namespace ConsoleApp1.Adapters;

public static class SideEffect_04_Sys_Main
{
    public static Eff<RT, string> AskUser<RT>() where RT : Has<Eff<RT>, IMyConsoleIO>
    {
        return from l in MyConsole<RT>.readLine
               let v = Capitalise(l)
               //from _ in liftIO(() => throw new Exception("crash"))
               from _ in MyConsole<RT>.writeLine(v)
               select v;
    }

    public static Eff<RT, string> AskUserAsync<RT>() where RT : Has<Eff<RT>, IMyConsoleIO>
    {
        return from l1 in MyConsole<RT>.myAsync1
               from l2 in MyConsole<RT>.myAsync2
               from l3 in MyConsole<RT>.myAsync3
               from _1 in MyConsole<RT>.writeLine(l1)
               from _2 in MyConsole<RT>.writeLine(l2)
               from _3 in MyConsole<RT>.writeLine(l3)
               select "";
    }

    private static string Capitalise(string text) =>
        new string(text.Select(x => Char.IsLower(x) ? Char.ToUpper(x) : x)
                       .ToArray());
}
