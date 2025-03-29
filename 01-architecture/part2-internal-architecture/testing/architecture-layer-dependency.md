# 레이어 의존성 테스트

## 패키지
- xUnit.v3
- ArchUnitNET.xUnit

## 테스트
```cs
using System.Reflection;

namespace GymManagement.Adapters.Persistence;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
```
- 어셈블리 단위로 `AssemblyReference` 클래스를 구현합니다.

```cs
public abstract class ArchitectureTestBase
{
    protected static readonly Architecture Architecture = new ArchLoader()
        // 어셈블리
        .LoadAssemblies(
            Adapters.Infrastructure.AssemblyReference.Assembly,
            Adapters.Persistence.AssemblyReference.Assembly,
            Application.AssemblyReference.Assembly,
            Domain.AssemblyReference.Assembly)
        .Build();

    protected static readonly IObjectProvider<IType> AdapterInfrastructureLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Adapters.Infrastructure.AssemblyReference.Assembly)
        .As("Adapters.Infrastructure");

    protected static readonly IObjectProvider<IType> AdapterPersistenceLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Adapters.Persistence.AssemblyReference.Assembly)
        .As("Adapters.Persistence");

    protected static readonly IObjectProvider<IType> ApplicationLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Application.AssemblyReference.Assembly)
        .As("Application");

    protected static readonly IObjectProvider<IType> DomainLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Domain.AssemblyReference.Assembly)
        .As("Domain");
}
```

```cs
public class LayerDependencyTests : ArchitectureTestBase
{
    [Fact]
    public void DomainLayer_ShouldNotHave_Dependencies_OnAnyOtherLayer()
    {
        IObjectProvider<IType>[] layers = [
            AdapterInfrastructureLayer,
            AdapterPersistenceLayer,
            ApplicationLayer
        ];

        foreach (var layer in layers)
        {
            ArchRuleDefinition
                .Types()
                .That()
                .Are(DomainLayer)
                .Should()
                .NotDependOnAny(layer)
                .Check(Architecture);
        }
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHave_Dependencies_OnAdapterLayer()
    {
        IObjectProvider<IType>[] adapterLayers = [
            AdapterInfrastructureLayer,
            AdapterPersistenceLayer,
        ];

        foreach (var adapterLayer in adapterLayers)
        {
            ArchRuleDefinition
                .Types()
                .That()
                .Are(ApplicationLayer)
                .Should()
                .NotDependOnAny(adapterLayer)
                .Check(Architecture);
        }
    }
}
```