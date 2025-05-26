using ConsoleApp1.Adapters.Sys.Traits;
using LanguageExt;
using static LanguageExt.Prelude;

namespace ConsoleApp1.Adapters.Sys.Live.Implementations;

public class MyConsoleIO : IMyConsoleIO
{
    public static readonly IMyConsoleIO Default = new MyConsoleIO();

    //public IO<Option<ConsoleKeyInfo>> ReadKey() =>
    //    lift(() => Optional(Console.ReadKey()));

    public IO<Unit> Clear() =>
        lift(Console.Clear);

    //public IO<Unit> SetBgColor(ConsoleColor color) =>
    //    lift(() => { Console.BackgroundColor = color; });

    //public IO<Unit> SetColor(ConsoleColor color) =>
    //    lift(() => { Console.ForegroundColor = color; });

    //public IO<Unit> ResetColor() =>
    //    lift(Console.ResetColor);

    //public IO<ConsoleColor> BgColor =>
    //    lift(() => Console.BackgroundColor);

    //public IO<ConsoleColor> Color =>
    //    lift(() => Console.ForegroundColor);

    //public IO<Option<int>> Read() =>
    //    lift(() =>
    //    {
    //        var k = Console.Read();
    //        return k == -1
    //                   ? None
    //                   : Some(k);
    //    });

    public IO<Option<string>> ReadLine() =>
        lift(() => Optional(Console.ReadLine()));

    public IO<string> Async1() =>
        liftIO(() => MyAsync(1));

    public IO<string> Async2() =>
        liftIO(() => MyAsync(2));

    public IO<string> Async3() =>
        liftIO(() => MyAsync(3));

    private async Task<string> MyAsync(int index)
    {
        Console.WriteLine($"{DateTime.Now} 전 {index}, {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(1000);
        Console.WriteLine($"{DateTime.Now} 후 {index}, {Thread.CurrentThread.ManagedThreadId}");
        return $"Hello: {index}";
    }

    //public IO<Unit> WriteLine() =>
    //    lift(Console.WriteLine);

    public IO<Unit> WriteLine(string value) =>
        lift(() => Console.WriteLine($"{value} {Thread.CurrentThread.ManagedThreadId}"));

    //public IO<Unit> Write(string value) =>
    //    lift(() => Console.Write(value));
}
