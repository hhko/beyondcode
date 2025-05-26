// https://github.com/louthy/language-ext/wiki/How-to-deal-with-side-effects

using LanguageExt;

using static LanguageExt.Prelude;

namespace Functional.SideEffects;

public class SideEffect_01_IO
{
    // Lift     : sync
    // LiftIO   : async

    static IO<string> readAllText(string path) =>
        lift(() =>
        {
            return File.ReadAllText(path);
        });

    static IO<Unit> writeAllText(string path, string text) =>
        lift(() =>
        {
            File.WriteAllTextAsync(path, text);
            return unit;
        });

    static IO<string> readAllTextAsync(string path) =>
        liftIO(() =>
        {
            return File.ReadAllTextAsync(path);
        });

    static IO<Unit> writeAllTextAsync(string path, string text) =>
        liftIO(() =>
        {
            return File.WriteAllTextAsync(path, text);
            //return unit;
        });

    static string Capitalise(string text) =>
          new string(text.Select(x => char.IsLower(x) ? char.ToUpper(x) : x)
                         .ToArray());

    public async Task Execut_01()
    {
        string inpath = "";
        string outpath = "";

        var computation = from text in readAllText(inpath)
                          from _ in writeAllText(outpath, text)
                          select unit;

        Fin<Unit> result = await computation.RunAsync();
    }

    public async Task Execute_02_1_pure()
    {
        string inpath = "";
        string outpath = "";

        var computation = from text in readAllText(inpath)
                          from ntext in IO.pure(Capitalise(text))         // string -> IO<string>
                          from _ in writeAllText(outpath, ntext)
                          select unit;

        Fin<Unit> result = await computation.RunAsync();
    }

    public async Task Execute_02_2_Map()
    {
        string inpath = "";
        string outpath = "";

        var computation = from text in readAllText(inpath)
                                        .Map(Capitalise)                  // string -> string
                          from _ in writeAllText(outpath, text)
                          select unit;

        Fin<Unit> result = await computation.RunAsync();
    }

    public async Task Execute_02_3_let()
    {
        string inpath = "";
        string outpath = "";

        var computation = from text in readAllText(inpath)
                          let ntext = Capitalise(text)                      // string -> string
                          from _ in writeAllText(outpath, ntext)
                          select unit;

        Fin<Unit> result = await computation.RunAsync();
    }
}
