using LanguageExt;

using static LanguageExt.Prelude;

namespace Functional.Disposing;

internal static class Dispose_03_Implicit_Exception
{
    public static async Task<Fin<Unit>> main()
    {
        Eff<Unit> effect =
            from r in use(() => new DisposableClass("4"))
            from x in liftIO(() => throw new Exception("crash"))
            from _ in release(r)
            select unit;

        return await effect.RunAsync();
    }   // 명시적 IDisposable, IAsyncDisposable 호출

    public class DisposableClass(string Id) : IDisposable, IAsyncDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"- Dispose {Id}");
        }

        public ValueTask DisposeAsync()
        {
            Console.WriteLine($"- DisposeAsync {Id}");
            return ValueTask.CompletedTask;
        }
    }
}
