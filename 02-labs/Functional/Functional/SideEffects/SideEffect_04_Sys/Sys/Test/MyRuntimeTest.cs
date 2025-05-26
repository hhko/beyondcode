using ConsoleApp1.Adapters.Sys.Traits;
using LanguageExt;
using LanguageExt.Traits;

namespace ConsoleApp1.Adapters.Sys.Test;

public record MyRuntimeTest(MyRuntimeEnvTest Env)
    : Has<Eff<MyRuntimeTest>, IMyConsoleIO>
{
    public static MyRuntimeTest New() =>
        new (new MyRuntimeEnvTest());

    static K<Eff<MyRuntimeTest>, A> asks<A>(Func<MyRuntimeTest, A> f) =>
        Readable.asks<Eff<MyRuntimeTest>, MyRuntimeTest, A>(f);


    //static K<Eff<MyRuntimeTest>, IMyConsoleIO>
    static K<Eff<MyRuntimeTest>, IMyConsoleIO> Has<Eff<MyRuntimeTest>, IMyConsoleIO>.Ask =>
        asks<IMyConsoleIO>(rt => new Implementations.MyConsoleIOTest(rt.Env));
}

public record MyRuntimeEnvTest
{
    public MyRuntimeEnvTest()
    {
        
    }
}
