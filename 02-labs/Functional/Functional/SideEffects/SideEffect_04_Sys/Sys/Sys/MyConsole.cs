using ConsoleApp1.Adapters.Sys.Traits;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.Pipes;
using LanguageExt.Traits;

namespace ConsoleApp1.Adapters.Sys.Sys;

/// <summary>
/// Time IO 
/// </summary>
public static class Console<M, RT>
    where M :
        MonadIO<M>,
        Fallible<Error, M>
    where RT :
        Has<M, IMyConsoleIO>
{
    static K<M, IMyConsoleIO> consoleIO => Has<M, RT, IMyConsoleIO>.ask;

    ///// <summary>
    ///// Read a key from the console
    ///// </summary>
    //public static K<M, ConsoleKeyInfo> readKey =>
    //    from t in consoleIO
    //    from k in t.ReadKey()
    //    from r in k.Match(Some: M.Pure,
    //                      None: M.Fail<ConsoleKeyInfo>(Errors.EndOfStream))
    //    select r;

    ///// <summary>
    ///// Read keys from the console and push them downstream 
    ///// </summary>
    //public static ProducerT<ConsoleKeyInfo, M, Unit> readKeys =>
    //    from ln in readKey
    //    from __ in ProducerT.yield<M, ConsoleKeyInfo>(ln)
    //    select unit;

    /// <summary>
    /// Clear the console
    /// </summary>
    public static K<M, Unit> clear =>
        consoleIO.Bind(e => e.Clear());

    ///// <summary>
    ///// Read from the console
    ///// </summary>
    //public static K<M, int> read =>
    //    from t in consoleIO
    //    from k in t.Read()
    //    from r in k.Match(Some: M.Pure,
    //                      None: M.Fail<int>(Errors.EndOfStream))
    //    select r;

    ///// <summary>
    ///// Read chars from the console and push them downstream 
    ///// </summary>
    //public static ProducerT<int, M, Unit> reads =>
    //    from ln in read
    //    from __ in ProducerT.yield<M, int>(ln)
    //    select unit;

    /// <summary>
    /// Read from the console
    /// </summary>
    public static K<M, string> readLine =>
        from t in consoleIO
        from k in t.ReadLine()
        from r in k.Match(Some: M.Pure,
                          None: M.Fail<string>(Errors.EndOfStream))
        select r;

    public static K<M, string> myAsync1 =>
        from t in consoleIO
        from k in t.Async1()
        select k;

    public static K<M, string> myAsync2 =>
        from t in consoleIO
        from k in t.Async2()
        select k;

    public static K<M, string> myAsync3 =>
        from t in consoleIO
        from k in t.Async3()
        select k;

    ///// <summary>
    ///// Read lines from the console and push them downstream 
    ///// </summary>
    //public static ProducerT<string, M, Unit> readLines =>
    //    from ln in readLine
    //    from _ in ProducerT.yield<M, string>(ln)
    //    select unit;

    ///// <summary>
    ///// Write an empty line to the console
    ///// </summary>
    //public static K<M, Unit> writeEmptyLine =>
    //    consoleIO.Bind(e => e.WriteLine());

    /// <summary>
    /// Write a line to the console (returning unit)
    /// </summary>
    public static K<M, Unit> writeLine(string line) =>
        consoleIO.Bind(e => e.WriteLine(line));

    ///// <summary>
    ///// Write a line to the console (returning the original line)
    ///// </summary>
    //public static K<M, string> writeLine2(string line) =>
    //    consoleIO.Bind(e => e.WriteLine(line)).Map(_ => line);

    ///// <summary>
    ///// Write a string to the console
    ///// </summary>
    //public static K<M, Unit> write(string line) =>
    //    consoleIO.Bind(e => e.Write(line));

    ///// <summary>
    ///// Write a string to the console
    ///// </summary>
    //public static K<M, Unit> write(char line) =>
    //    consoleIO.Bind(e => e.Write(line.ToString()));

    //public static K<M, Unit> setBgColour(ConsoleColor col) =>
    //    consoleIO.Bind(e => e.SetBgColor(col));

    //public static K<M, Unit> setColour(ConsoleColor col) =>
    //    consoleIO.Bind(e => e.SetColor(col));

    //public static K<M, Unit> resetColour() =>
    //    consoleIO.Bind(e => e.ResetColor());

    //public static K<M, ConsoleColor> bgColour =>
    //    consoleIO.Bind(e => e.BgColor);

    //public static K<M, ConsoleColor> colour =>
    //    consoleIO.Bind(e => e.Color);
}
