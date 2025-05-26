using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LanguageExt;

using static LanguageExt.Prelude;

namespace Functional.SideEffects;

public class SideEffect_02_Eff
{
    //
    // 1. 인터페이스
    //
    public interface IFileIO
    {
        string ReadAllText(string path);
        Unit WriteAllText(string path, string text);
    }

    public class TestFileIO : IFileIO
    {
        public string ReadAllText(string path)
        {
            return "File.ReadAllText(path);";
        }

        public Unit WriteAllText(string path, string text)
        {
            return unit;
        }
    }

    public class LiveFileIO : IFileIO
    {
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public Unit WriteAllText(string path, string text)
        {
            File.WriteAllText(path, text);
            return unit;
        }
    }

    //
    // 2. 인터페이스 Proxy 클래스
    //

    Eff<Runtime, string> readAllText<Runtime>(string path)
            where Runtime : IFileIO
    {
        return liftEff<Runtime, string>(runtime =>
        {
            return runtime.ReadAllText(path);
        });
    }

    static Eff<Runtime, Unit> writeAllText<Runtime>(string path, string text)
        where Runtime : IFileIO
    {
        return liftEff<Runtime, Unit>(runtime =>
        {
            runtime.WriteAllText(path, text);
            return unit;
        });
    }

    static string Capitalise(string text) =>
          new string(text.Select(x => char.IsLower(x) ? char.ToUpper(x) : x)
                         .ToArray());

    //
    // 사용
    //

    public async Task Execute_01_Live()
    {
        string inpath = "";
        string outpath = "";

        Eff<IFileIO, Unit> computation = from text in readAllText<IFileIO>(inpath)
                                         let ntext = Capitalise(text)                      // string -> string
                                         from _ in writeAllText<IFileIO>(outpath, ntext)
                                         select unit;

        LiveFileIO env = new();
        Fin<Unit> result = await computation.RunAsync(env);
    }

    public async Task Execute_02_Test()
    {
        string inpath = "";
        string outpath = "";

        Eff<IFileIO, Unit> computation = from text in readAllText<IFileIO>(inpath)
                                         let ntext = Capitalise(text)                      // string -> string
                                         from _ in writeAllText<IFileIO>(outpath, ntext)
                                         select unit;

        TestFileIO env = new();
        Fin<Unit> result = await computation.RunAsync(env);
    }
}
