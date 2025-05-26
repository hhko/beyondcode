using LanguageExt.Traits;
using LanguageExt;
using ConsoleApp1.Adapters.Sys.Traits;

namespace ConsoleApp1.Adapters.Sys.Live;

/// <summary>
/// Live IO runtime
/// </summary>
public record MyRuntime(MyRuntimeEnv Env) :
    //Local<Eff<MyRuntime>, ActivityEnv>,
    //Has<Eff<MyRuntime>, ActivitySourceIO>,
    Has<Eff<MyRuntime>, IMyConsoleIO>
    //Has<Eff<MyRuntime>, FileIO>,
    //Has<Eff<MyRuntime>, TextReadIO>,
    //Has<Eff<MyRuntime>, TimeIO>,
    //Has<Eff<MyRuntime>, EnvironmentIO>,
    //Has<Eff<MyRuntime>, DirectoryIO>,
    //Has<Eff<MyRuntime>, EncodingIO>
{
    /// <summary>
    /// Constructor function
    /// </summary>
    public static MyRuntime New() =>
        //new(new RuntimeEnv(ActivityEnv.Default));
        new(new MyRuntimeEnv());

    //static K<Eff<Runtime>, A> asks<A>(Func<Runtime, A> f) =>
    //    Readable.asks<Eff<Runtime>, Runtime, A>(f);

    //static K<Eff<Runtime>, A> local<A>(Func<Runtime, Runtime> f, K<Eff<Runtime>, A> ma) =>
    //    Readable.local(f, ma);

    static K<Eff<MyRuntime>, A> pure<A>(A value) =>
        Eff<MyRuntime, A>.Pure(value);

    ///// <summary>
    ///// Activity
    ///// </summary>
    //static K<Eff<Runtime>, ActivitySourceIO> Has<Eff<Runtime>, ActivitySourceIO>.Ask =>
    //    asks<ActivitySourceIO>(rt => new Implementations.ActivitySourceIO(rt.Env.Activity));

    /// <summary>
    /// Access the console environment
    /// </summary>
    /// <returns>Console environment</returns>
    static K<Eff<MyRuntime>, IMyConsoleIO> Has<Eff<MyRuntime>, IMyConsoleIO>.Ask { get; } =
        pure(Implementations.MyConsoleIO.Default);

    /// <summary>
    ///// Access the file environment
    ///// </summary>
    ///// <returns>File environment</returns>
    //static K<Eff<Runtime>, FileIO> Has<Eff<Runtime>, FileIO>.Ask { get; } =
    //    pure(Implementations.FileIO.Default);

    ///// <summary>
    ///// Access the TextReader environment
    ///// </summary>
    ///// <returns>TextReader environment</returns>
    //static K<Eff<Runtime>, TextReadIO> Has<Eff<Runtime>, TextReadIO>.Ask { get; } =
    //    pure(Implementations.TextReadIO.Default);

    ///// <summary>
    ///// Access the time environment
    ///// </summary>
    ///// <returns>Time environment</returns>
    //static K<Eff<Runtime>, TimeIO> Has<Eff<Runtime>, TimeIO>.Ask { get; } =
    //    pure(Implementations.TimeIO.Default);

    ///// <summary>
    ///// Access the operating-system environment
    ///// </summary>
    ///// <returns>Operating-system environment environment</returns>
    //static K<Eff<Runtime>, EnvironmentIO> Has<Eff<Runtime>, EnvironmentIO>.Ask { get; } =
    //    pure(Implementations.EnvironmentIO.Default);

    ///// <summary>
    ///// Access the directory environment
    ///// </summary>
    ///// <returns>Directory environment</returns>
    //static K<Eff<Runtime>, DirectoryIO> Has<Eff<Runtime>, DirectoryIO>.Ask { get; } =
    //    pure(Implementations.DirectoryIO.Default);

    ///// <summary>
    ///// Access the directory environment
    ///// </summary>
    ///// <returns>Directory environment</returns>
    //static K<Eff<Runtime>, EncodingIO> Has<Eff<Runtime>, EncodingIO>.Ask { get; } =
    //    pure(Implementations.EncodingIO.Default);

    ///// <summary>
    ///// Run with a local ActivityEnv 
    ///// </summary>
    //static K<Eff<Runtime>, A> Local<Eff<Runtime>, ActivityEnv>.With<A>(Func<ActivityEnv, ActivityEnv> f, K<Eff<Runtime>, A> ma) =>
    //    local(rt => rt with { Env = rt.Env with { Activity = f(rt.Env.Activity) } }, ma);

    ///// <summary>
    ///// Read the current ActivityEnv
    ///// </summary>
    //static K<Eff<Runtime>, ActivityEnv> Has<Eff<Runtime>, ActivityEnv>.Ask =>
    //    asks(rt => rt.Env.Activity);
}

//public record RuntimeEnv(ActivityEnv Activity) : IDisposable
//{
//    public void Dispose() =>
//        Activity.Dispose();
//}

public record MyRuntimeEnv() : IDisposable
{
    public void Dispose()
    {

    }
}
