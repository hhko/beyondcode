using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;

namespace DomainTypes_ValueObjects.Tests.Case08_ArchitectureTests;

public abstract class ArchitectureTestBase
{
    protected static readonly Architecture Architectures = BuildArchitecture();

    private static ArchUnitNET.Domain.Architecture BuildArchitecture()
    {
        List<System.Reflection.Assembly> assemblies = [];

        //
        // Services
        //
        assemblies.AddRange([ 
            DomainTypes_ValueObjects.AssemblyReference.Assembly
        ]);

        return new ArchLoader()
            .LoadAssemblies(assemblies.ToArray())
            .Build();
    }
}
