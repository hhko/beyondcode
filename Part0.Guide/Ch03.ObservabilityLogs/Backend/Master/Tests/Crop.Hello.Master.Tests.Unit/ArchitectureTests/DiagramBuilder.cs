using ArchUnitNET.Domain;
using ArchUnitNET.Domain.PlantUml.Export;
using ArchUnitNET.Fluent.PlantUml;
using ArchUnitNET.Fluent.Slices;
using ArchUnitNET.Loader;

namespace Crop.Hello.Master.Tests.Unit.ArchitectureTests;

public class DiagramBuilder
{
    [Fact]
    public void Draw()
    {
        string pattern = "Crop.(**)";
        //GivenSlices sliceRule = SliceRuleDefinition.Slices().Matching(pattern);
        GivenSlices sliceRule = SliceRuleDefinition.Slices().MatchingWithPackages(pattern);
        //Replace ArchUnitNET.Domain.Architecture with any class from your pattern
        Architecture arch = new ArchLoader().LoadAssemblies(
            Adapters.Infrastructure.AssemblyReference.Assembly,
            Adapters.Persistence.AssemblyReference.Assembly,
            Application.AssemblyReference.Assembly,
            Domain.AssemblyReference.Assembly)
        .Build();

        // IncludeDependenciesToOther
        // IncludeNodesWithoutDependencies
        // LimitDependencies
        GenerationOptions g = new GenerationOptions() { C4Style = true };

        string path = "C:\\Temp\\diagram.puml";

        PlantUmlDefinition.ComponentDiagram().WithDependenciesFromSlices(sliceRule.GetObjects(arch), g).WriteToFile(path);
    }
}
