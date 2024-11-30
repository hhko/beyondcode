using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using static Crop.Hello.Api.Tests.Unit.Abstractions.Constants.Constants;

namespace Crop.Hello.Api.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class LayerDependencyTests : ArchitectureBaseTest
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

    [Fact]
    public void AdapterLayer_ShouldNotHave_Dependencies_OnDomainLayer()
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
                .Are(adapterLayer)
                .Should()
                .NotDependOnAny(DomainLayer)
                .Check(Architecture);
        }
    }
}
