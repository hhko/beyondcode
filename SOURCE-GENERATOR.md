```cs
public interface IAdapter
{

}

public interface IXxx : IAdapter
{
    IResult DoSomething();
}

// XxxProxy
public class Xxx : IXxx
{
    public IResult DoSomething()
    {
        // ...
    }
}
```

## 소스 생성기
- [x] 프로젝트 속성
  ```xml
  <TargetFramework>netstandard2.0</TargetFramework>
  <EnforceExtendedAnalyzerRules>true</EnforceExtendedAnalyzerRules>
  ```
- [x] 패키지
  - Microsoft.CodeAnalysis.Analyzers
  - Microsoft.CodeAnalysis.CSharp
- [x] IIncrementalGenerator 인터페이스
- [ ] 디버깅: 빌드할 때
  ```cs
  Debugger.Launch();
  ```
- [ ] 대상 클래스 선정
  ```cs
  public void Initialize(IncrementalGeneratorInitializationContext context)
  {
      //Debugger.Launch();
  
      var provider = context.SyntaxProvider
          .CreateSyntaxProvider(
              // 클래스
              predicate: (syntaxNode, _) => syntaxNode is ClassDeclarationSyntax classDeclaration,
              // IAdapter 인터페이스 상속 클래스
              transform: (ctx, _) => TransformToClassDeclaration(ctx))
          .Where(classDeclaration => classDeclaration is not null);
  
      var compilation = context.CompilationProvider.Combine(provider.Collect());
      context.RegisterSourceOutput(compilation, Execute);
  }
  
  private ClassDeclarationSyntax? TransformToClassDeclaration(GeneratorSyntaxContext ctx)
  {
      var classDeclaration = (ClassDeclarationSyntax)ctx.Node;
      var semanticModel = ctx.SemanticModel;
      var classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration);
  
      // IAdapter 인터페이스를 구현하는지 확인
      return classSymbol?.AllInterfaces
          .Any(i => i.Name == "IAdapter") is true
              ? classDeclaration 
              : null;
  }
  ```
- [x] 대상 클래스의 네임스페이스 추출
  ```cs
  private string GetNamespace(ClassDeclarationSyntax classDeclaration)
  {
      var namespaceDeclaration = classDeclaration.AncestorsAndSelf()
          .OfType<BaseNamespaceDeclarationSyntax>()
          .FirstOrDefault();
  
      return namespaceDeclaration switch
      {
          // 파일 스코프
          FileScopedNamespaceDeclarationSyntax fileScopedNamespace => fileScopedNamespace.Name.ToString(),
          // 블럭 스코드
          NamespaceDeclarationSyntax regularNamespace => regularNamespace.Name.ToString(),
          // 없을 때
          _ => "GlobalNamespace" //string.Empty // Global namespace
      };
  }
  ```
- [x] 대상 클래스의 메서드 선정: public __static__, IResult, IResult<T>, IResult 상속, async
  ```cs
  var allMethods = classSymbol.GetMembers()
    .OfType<IMethodSymbol>()
    .Where(m => m.DeclaredAccessibility == Accessibility.Public 
        && !m.IsStatic
        && m.MethodKind != MethodKind.Constructor
        && m.IsVirtual
        && IsIResultOrDerivedType(m.ReturnType))
    .ToList();

  private bool IsIResultOrDerivedType(ITypeSymbol returnType)
  {
      // 검사할 타입 이름: IResult 또는 IResult<T>
      const string resultTypeName = "IResult";
  
      // 직접적으로 IResult 또는 IResult<T>인지 확인
      if (returnType.Name == resultTypeName)
      {
          return true;
      }
  
      // 제네릭 타입 검사
      if (returnType is INamedTypeSymbol namedType && namedType.IsGenericType)
      {
          if (namedType.ConstructedFrom.Name == resultTypeName)
          {
              return true;
          }
      }
  
      // IResult 또는 IResult<T>를 상속받았는지 확인
      if (returnType.AllInterfaces.Any(i => i.Name == resultTypeName))
      {
          return true;
      }
  
      // 4. Task<IResult> 또는 Task<IResult<T>> 검사
      if (returnType is INamedTypeSymbol taskType && taskType.Name == "Task" && taskType.TypeArguments.Length == 1)
      {
          var typeArgument = taskType.TypeArguments[0];
          // Task<IResult> 또는 Task<IResult<T>>에 대해 재귀적으로 검사
          if (IsIResultOrDerivedType(typeArgument))
          {
              return true;
          }
      }
  
      return false;
  }
  ```
- [ ] Pipleline 로그: 시간
- [ ] Pipleline 로그: 입출력 값?
- [ ] Pipleline 예외
- [ ] Pipleline 구조화
- [ ] NuGet 패키지화
- [ ] Source Generator 테스트
- [ ] virtual, override 제거?

## 적용 프로젝트
- [x] .g.cs 파일 명시적 생성
  ```
  <EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
  ```
- [x] Source Generator 프로젝트 참조
  ```xml
  <ProjectReference Include=" ... .csproj" OutputItemType="Analyzer" ReferenceOutputAssembly="false" />
  ```
- [x] 디버깅: .g.cs 파일 브레이크 포인트
  ```
  ClassListGenerator\ClassListGenerator.AdapterSourceGenerator
  obj\Debug\net9.0\
    generated\
        {어셈블리}\
            {네임스페이스}.{IIncrementalGenerator 구현 클래스}\
                {className}Proxy.g.cs
  ```
- [x] `IAdapter` 인터페이스 상속
- [x] `IAdapter` 인터페이스 상속 구현 클래스
  - virtual
  - 반환 타입
    - IResult
    - IResult<T>
    - IResult 상속
- [x] 의존성 등록 확장 메서드
  ```cs
  public static IServiceCollection AddAdapterProxy<TInterface, TImplementation>(
      this IServiceCollection services)
      where TInterface : class, IAdapter
      where TImplementation : class, TInterface
  {
      Type? proxyType = typeof(TImplementation).Assembly.GetTypes()
          .FirstOrDefault(t => t.Name == $"{typeof(TImplementation).Name}Proxy" && typeof(TInterface).IsAssignableFrom(t));
  
      if (proxyType == null)
      {
          throw new InvalidOperationException($"Proxy class for {typeof(TInterface).Name} not found.");
      }
  
      services.AddTransient(typeof(TInterface), proxyType);
      services.AddTransient<TImplementation>();
  
      return services;
  }  
  ```
- [x] 의존성 등록
  ```cs
  .AddAdapterProxy<IXyz, XyzAdapter>()

  // XyzAdapterProxy
  var xyz = serviceProvider.GetRequiredService<IXyz>();
  ```
- [ ] 인텔리센스 오동작, 에러 인식?


```cs
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Text;

namespace ClassListGenerator2;


[Generator]
public class AdapterSourceGenerator2 : IIncrementalGenerator
{
    public static bool AttachDebugger { get; set; } = false;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //if (AttachDebugger)
        //{
        //    Debugger.Launch();
        //}
        //Debugger.Launch();

        var provider = context.SyntaxProvider
            .CreateSyntaxProvider(
                // 클래스
                predicate: (syntaxNode, _) => syntaxNode is ClassDeclarationSyntax classDeclaration,
                // IAdapter 클래스
                transform: (ctx, _) => TransformToClassDeclaration(ctx))
            .Where(classDeclaration => classDeclaration is not null);

        var compilation = context.CompilationProvider.Combine(provider.Collect());
        context.RegisterSourceOutput(compilation, Execute);
    }

    private ClassDeclarationSyntax? TransformToClassDeclaration(GeneratorSyntaxContext ctx)
    {
        var classDeclaration = (ClassDeclarationSyntax)ctx.Node;
        var semanticModel = ctx.SemanticModel;
        var classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration);

        // IAdapter 인터페이스를 구현하는지 확인
        return classSymbol?.AllInterfaces
            .Any(i => i.Name == "IAdapter") is true
                ? classDeclaration 
                : null;
    }

    private void Execute(SourceProductionContext context, (Compilation Left, ImmutableArray<ClassDeclarationSyntax?> Right) tuple)
    {
        var (compilation, classes) = tuple;

        foreach (var classDeclaration in classes)
        {
            var className = classDeclaration!.Identifier.Text;
            var namespaceName = GetNamespace(classDeclaration);
            var proxyCode = GenerateProxyClass(compilation, namespaceName, className, classDeclaration);

            context.AddSource($"{className}Proxy.g.cs", SourceText.From(proxyCode, Encoding.UTF8));
        }
    }

    private string GetNamespace(ClassDeclarationSyntax classDeclaration)
    {
        var namespaceDeclaration = classDeclaration.AncestorsAndSelf()
            .OfType<BaseNamespaceDeclarationSyntax>()
            .FirstOrDefault();

        return namespaceDeclaration switch
        {
            // 파일 스코프
            FileScopedNamespaceDeclarationSyntax fileScopedNamespace => fileScopedNamespace.Name.ToString(),
            // 블럭 스코드
            NamespaceDeclarationSyntax regularNamespace => regularNamespace.Name.ToString(),
            // 없을 때
            _ => "GlobalNamespace" //string.Empty // Global namespace
        };
    }

    private string GenerateProxyClass(Compilation compilation, string namespaceName, string className, ClassDeclarationSyntax classDeclaration)
    {
        var builder = new StringBuilder();
        
        if (!string.IsNullOrEmpty(namespaceName))
        {
            builder.AppendLine($"namespace {namespaceName};");
            builder.AppendLine();
        }

        builder.AppendLine($"public sealed class {className}Proxy : {className}");
        builder.AppendLine("{");
        builder.AppendLine($"    private readonly {className} _inner;");
        builder.AppendLine();
        builder.AppendLine($"    public {className}Proxy({className} inner)");
        builder.AppendLine("    {");
        builder.AppendLine("        _inner = inner;");
        builder.AppendLine("    }");

        // 가로챌 메서드 생성 (클래스 및 인터페이스 메서드 포함)
        var allMethods = GetAllPublicMethods(compilation, classDeclaration);
        foreach (var method in allMethods)
        {
            AppendMethod(builder, method);
        }

        builder.AppendLine("}");
        return builder.ToString();
    }

    private void AppendMethod(StringBuilder builder, IMethodSymbol method)
    {
        var returnType = method.ReturnType.ToDisplayString();
        var methodName = method.Name;
        var parameters = string.Join(", ", method.Parameters.Select(p => $"{p.Type.ToDisplayString()} {p.Name}"));
        var arguments = string.Join(", ", method.Parameters.Select(p => p.Name));

        if (method.IsAsync)
        {
            builder.AppendLine($"    public override async {returnType} {methodName}({parameters})");
        }
        else
        {
            builder.AppendLine($"    public override {returnType} {methodName}({parameters})");
        }
            
        builder.AppendLine("    {");
        builder.AppendLine("        Console.WriteLine(\"Before method execution\");");

        if (method.IsAsync)
        {
            builder.AppendLine($"        var result = await _inner.{methodName}({arguments});");
        }
        else
        {
            builder.AppendLine($"        var result = _inner.{methodName}({arguments});");
        }

        builder.AppendLine("        Console.WriteLine(\"After method execution\");");
        builder.AppendLine("        return result;");

        //if (returnType == "void")
        //{
        //    builder.AppendLine($"        _inner.{methodName}({arguments});");
        //    builder.AppendLine("        Console.WriteLine(\"After method execution\");");
        //}
        //else
        //{
        //    builder.AppendLine($"        var result = _inner.{methodName}({arguments});");
        //    builder.AppendLine("        Console.WriteLine(\"After method execution\");");
        //    builder.AppendLine("        return result;");
        //}

        builder.AppendLine("    }");
    }

    private IEnumerable<IMethodSymbol> GetAllPublicMethods(Compilation compilation, ClassDeclarationSyntax classDeclaration)
    {
        var semanticModel = compilation.GetSemanticModel(classDeclaration.SyntaxTree);
        var classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration);

        if (classSymbol == null)
        {
            return Enumerable.Empty<IMethodSymbol>();
        }

        // 클래스 메서드 + 상속된 인터페이스 메서드 모두 가져오기
        var allMethods = classSymbol.GetMembers()
            .OfType<IMethodSymbol>()
            .Where(m => m.DeclaredAccessibility == Accessibility.Public 
                && !m.IsStatic
                && m.MethodKind != MethodKind.Constructor
                //&& m.IsVirtual                                 // .g.cs 파일에서 virtual이 없을 때 컴퍼일러 에러 제공
                && IsIResultOrDerivedType(m.ReturnType))
            .ToList();

        //// 인터페이스 메서드 포함
        //foreach (var interfaceSymbol in classSymbol.AllInterfaces)
        //{
        //    allMethods.AddRange(interfaceSymbol.GetMembers()
        //        .OfType<IMethodSymbol>()
        //        .Where(m => m.DeclaredAccessibility == Accessibility.Public
        //            && m.IsVirtual
        //            && IsIResultOrDerivedType(m.ReceiverType!)));
        //}

        //return allMethods.Distinct();
        return allMethods;
    }

    private bool IsIResultOrDerivedType(ITypeSymbol returnType)
    {
        // 검사할 타입 이름: IResult 또는 IResult<T>
        const string resultTypeName = "IResult";

        // 직접적으로 IResult 또는 IResult<T>인지 확인
        if (returnType.Name == resultTypeName)
        {
            return true;
        }

        // 제네릭 타입 검사
        if (returnType is INamedTypeSymbol namedType && namedType.IsGenericType)
        {
            if (namedType.ConstructedFrom.Name == resultTypeName)
            {
                return true;
            }
        }

        // IResult 또는 IResult<T>를 상속받았는지 확인
        if (returnType.AllInterfaces.Any(i => i.Name == resultTypeName))
        {
            return true;
        }

        // 4. Task<IResult> 또는 Task<IResult<T>> 검사
        if (returnType is INamedTypeSymbol taskType && taskType.Name == "Task" && taskType.TypeArguments.Length == 1)
        {
            var typeArgument = taskType.TypeArguments[0];
            // Task<IResult> 또는 Task<IResult<T>>에 대해 재귀적으로 검사
            if (IsIResultOrDerivedType(typeArgument))
            {
                return true;
            }
        }

        return false;
    }
}
```


## HAProxy
- [한글, D115. HAProxy installation and testing (check this before moving on to Spring Cloud!!)](https://www.youtube.com/watch?v=dZ-vKnACuaM&list=PLogzC_RPf25Fx3eNZzxLVw3dOL7r4XIUk&index=18)
- [한글, D116. HAProxy high availability configuration with keepalived](https://www.youtube.com/watch?v=LXo-sfckeWA&list=PLogzC_RPf25Fx3eNZzxLVw3dOL7r4XIUk&index=19)
- [5. switching & routing 라우터 동작원리와 L3스위치](https://www.youtube.com/watch?v=HnxaDoSo6mo)

## Full 코드 동영상
- [ ] 07:05:22 | [ASP.NET Core - Clean Architecture - Full Course](https://www.youtube.com/watch?v=gGa7SLk1-0Q)
- [ ] 06:21:19 | [Clean Architecture Fundaments in .NET Core MVC (.NET 8)](https://www.youtube.com/watch?v=CAwpmoA7sno)
---
- [ ] **02:22:40 | [Completely 🚀Master .NET 8 Microservices with Ocelot : implement Auth, Gateway, Caching & More🌟](https://www.youtube.com/watch?v=m9gUf7OdLmA&t=6464s)**
- [ ] 07:06:31 | [.NET Microservices – Full Course for Beginners](https://www.youtube.com/watch?v=CqCDOosvZIk)
- [ ] 09:28:18 | [Completely build 🛒 eCommerce Microservice in .NET 8: Implement 🚀 Gateway, Rate Limiting & more](https://www.youtube.com/watch?v=L2L_qe-FnUY)
- [ ] 11:05:57 | [.NET Microservices – Full Course](https://www.youtube.com/watch?v=DgVjEo3OGBI)
- [ ] 07:56:54 | [Introduction to .NET Microservices (.NET 8)](https://www.youtube.com/watch?v=Nw4AZs1kLAs&t=26579s)

## 테스트
- [ ] [Testing.Platform, the new way to run .NET tests](https://www.youtube.com/watch?v=9Jz47Ze9LOI)