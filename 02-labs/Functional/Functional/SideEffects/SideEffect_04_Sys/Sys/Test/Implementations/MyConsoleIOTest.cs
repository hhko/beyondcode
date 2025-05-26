using ConsoleApp1.Adapters.Sys.Traits;
using LanguageExt;
using static LanguageExt.Prelude;

namespace ConsoleApp1.Adapters.Sys.Test.Implementations;

public class MyConsoleIOTest(MyRuntimeEnvTest Env) : IMyConsoleIO
{
    public IO<Unit> Clear()
    {
        return unitIO;
    }

    public IO<Option<string>> ReadLine()
    {
        return lift(() => Option<string>.Some("xx"));
    }

    public IO<Unit> WriteLine(string value)
    {
        return unitIO;
    }

    public IO<string> Async1()
    {
        return lift(() => "1");
    }

    public IO<string> Async2()
    {
        return lift(() => "2");
    }

    public IO<string> Async3()
    {
        return lift(() => "3");
    }
}
