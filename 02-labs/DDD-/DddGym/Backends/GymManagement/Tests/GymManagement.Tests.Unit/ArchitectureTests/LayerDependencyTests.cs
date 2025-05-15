using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class LayerDependencyTests : ArchitectureTestBase
{
    [Fact]
    public void DomainLayer_ShouldNotHave_Dependencies_OnAnyOtherLayer()
    {
        IObjectProvider<IType>[] layers = [
            AdapterInfrastructureLayer,
            AdapterPersistenceLayer,
            AdapterPresentationLayer,
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
            AdapterPresentationLayer
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