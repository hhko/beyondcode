using ConsoleApp1.Adapters.Sys.Traits;
using LanguageExt;
using LanguageExt.Pipes;
using LanguageExt.Traits;

namespace ConsoleApp1.Adapters.Sys.Sys;

public class MyConsole<RT>
    where RT : Has<Eff<RT>, IMyConsoleIO>
{
    ///// <summary>
    ///// Read a key from the console
    ///// </summary>
    //public static Eff<RT, ConsoleKeyInfo> readKey =>
    //    Console<Eff<RT>, RT>.readKey.As();

    ///// <summary>
    ///// Read keys from the console and push them downstream 
    ///// </summary>
    //public static Producer<RT, ConsoleKeyInfo, Unit> readKeys =>
    //    Console<Eff<RT>, RT>.readKeys;

    /// <summary>
    /// Clear the console
    /// </summary>
    public static Eff<RT, Unit> clear =>
        Console<Eff<RT>, RT>.clear.As();

    ///// <summary>
    ///// Read from the console
    ///// </summary>
    //public static Eff<RT, int> read =>
    //    Console<Eff<RT>, RT>.read.As();

    ///// <summary>
    ///// Read chars from the console and push them downstream 
    ///// </summary>
    //public static Producer<RT, int, Unit> reads =>
    //    Console<Eff<RT>, RT>.reads;

    /// <summary>
    /// Read from the console
    /// </summary>
    public static Eff<RT, string> readLine =>
        Console<Eff<RT>, RT>.readLine.As();

    public static Eff<RT, string> myAsync1 =>
        Console<Eff<RT>, RT>.myAsync1.As();

    public static Eff<RT, string> myAsync2 =>
        Console<Eff<RT>, RT>.myAsync2.As();

    public static Eff<RT, string> myAsync3 =>
        Console<Eff<RT>, RT>.myAsync3.As();

    ///// <summary>
    ///// Read lines from the console and push them downstream 
    ///// </summary>
    //public static Producer<RT, string, Unit> readLines =>
    //    Console<Eff<RT>, RT>.readLines;

    ///// <summary>
    ///// Write an empty line to the console
    ///// </summary>
    //public static Eff<RT, Unit> writeEmptyLine =>
    //    Console<Eff<RT>, RT>.writeEmptyLine.As();

    /// <summary>
    /// Write a line to the console (returning unit)
    /// </summary>
    public static Eff<RT, Unit> writeLine(string line) =>
        Console<Eff<RT>, RT>.writeLine(line).As();

    ///// <summary>
    ///// Write a line to the console (returning the original line)
    ///// </summary>
    //public static Eff<RT, string> writeLine2(string line) =>
    //    Console<Eff<RT>, RT>.writeLine2(line).As();

    ///// <summary>
    ///// Write a string to the console
    ///// </summary>
    //public static Eff<RT, Unit> write(string line) =>
    //    Console<Eff<RT>, RT>.write(line).As();

    ///// <summary>
    ///// Write a string to the console
    ///// </summary>
    //public static Eff<RT, Unit> write(char line) =>
    //    Console<Eff<RT>, RT>.write(line).As();

    //public static Eff<RT, Unit> setBgColour(ConsoleColor colour) =>
    //    Console<Eff<RT>, RT>.setBgColour(colour).As();

    //public static Eff<RT, Unit> setColour(ConsoleColor colour) =>
    //    Console<Eff<RT>, RT>.setColour(colour).As();

    //public static Eff<RT, Unit> resetColour =>
    //    Console<Eff<RT>, RT>.resetColour().As();

    //public static Eff<RT, ConsoleColor> bgColour =>
    //    Console<Eff<RT>, RT>.bgColour.As();

    //public static Eff<RT, ConsoleColor> color =>
    //    Console<Eff<RT>, RT>.colour.As();
}
