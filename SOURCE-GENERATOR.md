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

