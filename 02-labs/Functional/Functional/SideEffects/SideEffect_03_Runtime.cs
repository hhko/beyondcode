using LanguageExt;

using static Functional.SideEffects.SideEffect_03_Runtime;
using static LanguageExt.Prelude;

namespace Functional.SideEffects;

public class SideEffect_03_Runtime
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
        readonly Dictionary<string, string> files;

        public TestFileIO(Dictionary<string, string> files) =>
            this.files = files;

        public string ReadAllText(string path)
        {
            return files.ContainsKey(path)
                ? files[path]
                : throw new FileNotFoundException(path);
        }

        public Unit WriteAllText(string path, string text)
        {
            if (files.ContainsKey(path))
            {
                files[path] = text;
            }
            else
            {
                files.Add(path, text);
            }
            return unit;
        }
    }

    public class LiveFileIO : IFileIO
    {
        public static readonly IFileIO Default = new LiveFileIO();

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
    // 2. 런타임: 의존성 주입 클래스
    //

    public interface IHasFile<RT>
        where RT : struct, IHasFile<RT>
    {
        Eff<RT, IFileIO> FileEff { get; }
    }

    public struct LiveRuntime : IHasFile<LiveRuntime>
    {
        // SuccessEff: LanguageExt.Eff<IFileIO>.Pure(LiveFileIO.Default);
        public Eff<LiveRuntime, IFileIO> FileEff => SuccessEff(LiveFileIO.Default);
    }

    public struct TestRuntime : IHasFile<TestRuntime>
    {
        readonly TestRuntimeEnv Env;

        public TestRuntime(TestRuntimeEnv env) =>
            Env = env;

        public Eff<TestRuntime, IFileIO> FileEff =>
            liftEff<TestRuntime, IFileIO>(static rt => (IFileIO)new TestFileIO(rt.Env.Files));
        //public Eff<TestRuntimeEnv, FileIO> FileEff => 
        //    Eff<TestRuntime; FileIO>
    }

    public record TestRuntimeEnv(Dictionary<string, string> Files);

    //
    // 3. 인터페이스 Proxy 클래스
    //
    public static class File<RT>
        where RT : struct, IHasFile<RT>
    {
        //          string ReadAllText(string path)
        // Eff<RT, string> readAllText(string path)
        public static Eff<RT, string> ReadAllText(string path) =>
            default(RT).FileEff.Map(rt => rt.ReadAllText(path));

        public static Eff<RT, Unit> WriteAllText(string path, string text) =>
            default(RT).FileEff.Map(rt => rt.WriteAllText(path, text));
    }

    //
    // 사용
    //

    // default(RT).FileEff 노출
    //public static Eff<RT, Unit> CopyFile1<RT>(string source, string dest)
    //   where RT : struct, IHasFile<RT> =>
    //       from text in default(RT).FileEff.Map(rt => rt.ReadAllText(source))
    //       let ntext = Capitalise(text)
    //       from _ in default(RT).FileEff.Map(rt => rt.WriteAllText(dest, ntext))
    //       select unit;

    // default(RT).FileEff 은닉
    //
    // 개선된 점
    //  1. 클래스화: File<RT>
    //  2. IFileIO 인터페이스을 은닉하고, Generic로 대체
    public static Eff<RT, Unit> CopyFile2<RT>(string source, string dest)
        where RT : struct, IHasFile<RT> =>
            from text in File<RT>.ReadAllText(source)       // vs. readAllText<IFileIO>(inpath)
            let ntext = Capitalise(text)
            from _ in File<RT>.WriteAllText(dest, ntext)    // vs. writeAllText<IFileIO>(outpath, ntext)
            select unit;

    static string Capitalise(string text) =>
          new string(text.Select(x => char.IsLower(x) ? char.ToUpper(x) : x)
                         .ToArray());

    public static async Task Execute_01_Live()
    {
        string src = "E:\\Workspace\\Temp\\In.txt";
        string dest = "E:\\Workspace\\Temp\\Out.txt";

        Eff<LiveRuntime, Unit> result = CopyFile2<LiveRuntime>(src, dest);

        LiveRuntime rt = new();
        Fin<Unit> x = await result.RunAsync(rt);
    }

    public static async Task Execute_02_Test()
    {
        string src = "1";
        string dest = "2";

        Eff<TestRuntime, Unit> result = CopyFile2<TestRuntime>(src, dest);

        Dictionary<string, string> files = new();
        files["1"] = "aA";
        files["2"] = "bB";
        files["3"] = "cC";
        TestRuntimeEnv env = new(files);
        TestRuntime rt = new TestRuntime(env);
        Fin<Unit> x = await result.RunAsync(rt);
    }
}
