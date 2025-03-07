using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;

namespace Crop.Hello.Api.Tests.Unit.ArchitectureTests;

public abstract class ArchitectureBaseTest
{
    // 레이어 어셈블리 집합
    protected static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            Adapters.Infrastructure.AssemblyReference.Assembly,
            Adapters.Persistence.AssemblyReference.Assembly,
            Application.AssemblyReference.Assembly,
            Domain.AssemblyReference.Assembly)
        .Build();

    // Adapters Infrastructure 레이어
    protected static readonly IObjectProvider<IType> AdaptersInfrastructureLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Adapters.Infrastructure.AssemblyReference.Assembly)
        .As("Adapters.Infrastructure");

    // Adapters Persistence 레이어
    protected static readonly IObjectProvider<IType> AdaptersPersistenceLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Adapters.Persistence.AssemblyReference.Assembly)
        .As("Adapters.Persistence");

    // Application 레이어
    protected static readonly IObjectProvider<IType> ApplicationLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Application.AssemblyReference.Assembly)
        .As("Application");

    // Domain 레이어
    protected static readonly IObjectProvider<IType> DomainLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Domain.AssemblyReference.Assembly)
        .As("Domain");
}
