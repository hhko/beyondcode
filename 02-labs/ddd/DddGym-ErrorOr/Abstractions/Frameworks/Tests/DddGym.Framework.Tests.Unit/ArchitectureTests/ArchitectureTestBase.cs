using ArchUnitNET.Domain;
using ArchUnitNET.Loader;

namespace GymManagement.Tests.Unit.ArchitectureTests;

public abstract class ArchitectureTestBase
{
    // 테스트 대상 어셈블리
    protected static readonly Architecture Architecture = new ArchLoader()
         .LoadAssemblies(
            Framework.AssemblyReference.Assembly)
        .Build();
}
