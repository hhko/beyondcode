using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using Crop.Hello.Api.Domain;
using static Crop.Hello.Api.Tests.Unit.Abstractions.Constants.Constants;

namespace Crop.Hello.Api.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class AssemblyTests : ArchitectureBaseTest
{
    [Fact]
    public void EveryAssembly_ShouldHave_ExactlyOneAssemblyReference()
    {
        // Arrange
        var assemblyReferences = Architecture
            .Classes
            .Where(cls => cls.Name == nameof(AssemblyReference))
            .ToList();

        var groupedByAssembly = assemblyReferences
            .GroupBy(cls => cls.Assembly)
            .Select(group => new { Assembly = group.Key, Count = group.Count() })
            .ToList();

        // Assert
        groupedByAssembly
            .Count
            .Should().Be(Architecture.Assemblies.Count());

        groupedByAssembly
            .Should().OnlyContain(assembly => assembly.Count == 1);
    }

    [Fact]
    public void AssemblyReference_ShouldHave_PublicAccessModifier()
    {
        // TODO: static 접근 제어자
        ArchRuleDefinition
            .Classes()
            .That()
            .HaveName(nameof(AssemblyReference))
            .Should().BePublic();
    }
}
