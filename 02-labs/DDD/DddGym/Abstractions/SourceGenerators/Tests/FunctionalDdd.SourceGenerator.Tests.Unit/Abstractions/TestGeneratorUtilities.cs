using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FunctionalDdd.SourceGenerator.Tests.Unit.Abstractions;

public static class TestGeneratorUtilities
{
    public static string? Generate<TGenerator>(this TGenerator generator, string sourceCode)
        where TGenerator : IIncrementalGenerator, new()
    {
        // 소스 코드에서 Syntax Tree 생성
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);

        // 현재 로드된 어셈블리 중 동적이 아닌 것들을 참조로 변환
        var references = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .Where(assembly => !assembly.IsDynamic)
            .Select(assembly => MetadataReference.CreateFromFile(assembly.Location))
            .Cast<MetadataReference>();

        var compilation = CSharpCompilation.Create(
            "SourceGeneratorTests",     // 생성할 어셈블리 이름
            [syntaxTree],               // 소스
            references,                 // 참조
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        // 컴파일: IIncrementalGenerator 소스 생성기 호출
        CSharpGeneratorDriver
            .Create(generator)
            .RunGeneratorsAndUpdateCompilation(
                compilation,
                out var outputCompilation,          // 소스 생성기 결과: 소스
                out var diagnostics);               // 소스 생성기 진단: 경고, 에러

        // 소스 생성기 진단(컴파일러 에러)
        diagnostics
            .Where(d => d.Severity == DiagnosticSeverity.Error)
            .ShouldBeEmpty();

        // 소스 생성기 결과(컴파일러 결과)
        return outputCompilation
            .SyntaxTrees
            .Skip(1)                // [0] 원본 소스 SyntaxTree 제외
            .LastOrDefault()?
            .ToString();
    }
}
