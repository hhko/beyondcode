using LanguageExt;

using static LanguageExt.Prelude;

namespace Functional.Disposing;

internal class Dispose_01_Explicit
{
    public static async Task<Fin<Unit>> main()
    {
        Eff<Unit> effect =
            from r in use(() => new DisposableClass("4"))
            from _ in release(r)    // 명시적 IDisposable, IAsyncDisposable 호출
            select unit;

        return await effect.RunAsync();
    }

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
