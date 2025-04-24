using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;

namespace GymManagement.Tests.Unit.ArchitectureTests;

public abstract class ArchitectureTestBase
{
    // 테스트 대상 어셈블리
    protected static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            Adapters.Infrastructure.AssemblyReference.Assembly,
            Adapters.Persistence.AssemblyReference.Assembly,
            Adapters.Presentation.AssemblyReference.Assembly,
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

    protected static readonly IObjectProvider<IType> AdapterPresentationLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Adapters.Presentation.AssemblyReference.Assembly)
        .As("Adapters.Presentation");

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
