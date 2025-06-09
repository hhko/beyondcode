using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;

namespace GymDdd.Tests.Architecture;

public abstract class ArchitectureTestBase
{
    protected static readonly ArchUnitNET.Domain.Architecture AllArchitecture = BuildArchitecture(includeFramework: true);
    protected static readonly ArchUnitNET.Domain.Architecture ServiceArchitecture = BuildArchitecture(includeFramework: false);

    private static ArchUnitNET.Domain.Architecture BuildArchitecture(bool includeFramework)
    {
        List<System.Reflection.Assembly> assemblies = [];

        if (includeFramework)
        {
            assemblies.AddRange([
                // Framework
                GymDdd.Framework.AssemblyReference.Assembly,
                GymDdd.Framework.WebApi.AssemblyReference.Assembly,

                // SourceGenerator
                GymDdd.SourceGenerator.AssemblyReference.Assembly
            ]);
        }

        //
        // Services
        //
        assemblies.AddRange([ 
            // GymManagement Service
            GymManagement.Adapters.Infrastructure.AssemblyReference.Assembly,
            GymManagement.Adapters.Persistence.AssemblyReference.Assembly,
            GymManagement.Adapters.Presentation.AssemblyReference.Assembly,
            GymManagement.Application.AssemblyReference.Assembly,
            GymManagement.Domain.AssemblyReference.Assembly

            // TODO Service
        ]);

        return new ArchLoader()
            .LoadAssemblies(assemblies.ToArray())
            .Build();
    }

    protected class GymManagementService
    {
        public static readonly IObjectProvider<IType> AdapterInfrastructureLayer = ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(GymManagement.Adapters.Infrastructure.AssemblyReference.Assembly)
            .As($"{nameof(GymManagement)}." +
                $"{nameof(GymManagement.Adapters)}." +
                $"{nameof(GymManagement.Adapters.Infrastructure)}");

        public static readonly IObjectProvider<IType> AdapterPersistenceLayer = ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(GymManagement.Adapters.Persistence.AssemblyReference.Assembly)
            .As($"{nameof(GymManagement)}." +
                $"{nameof(GymManagement.Adapters)}." +
                $"{nameof(GymManagement.Adapters.Persistence)}");

        public static readonly IObjectProvider<IType> AdapterPresentationLayer = ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(GymManagement.Adapters.Presentation.AssemblyReference.Assembly)
            .As($"{nameof(GymManagement)}." +
                $"{nameof(GymManagement.Adapters)}." +
                $"{nameof(GymManagement.Adapters.Presentation)}");

        public static readonly IObjectProvider<IType> ApplicationLayer = ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(GymManagement.Application.AssemblyReference.Assembly)
            .As($"{nameof(GymManagement)}." +
                $"{nameof(GymManagement.Application)}");

        public static readonly IObjectProvider<IType> DomainLayer = ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(GymManagement.Domain.AssemblyReference.Assembly)
            .As($"{nameof(GymManagement)}." +
                $"{nameof(GymManagement.Domain)}");
    }
}
