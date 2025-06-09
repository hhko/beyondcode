using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using static GymDdd.Tests.Architecture.Abstractions.Constants.Constants;

namespace GymDdd.Tests.Architecture.DesignRuleTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class GymManagementDependencyTests : ArchitectureTestBase
{
    [Fact]
    public void DomainLayer_ShouldNotHave_Dependencies_OnAnyOtherLayer()
    {
        IObjectProvider<IType>[] layers = [
            GymManagementService.AdapterInfrastructureLayer,
            GymManagementService.AdapterPersistenceLayer,
            GymManagementService.AdapterPresentationLayer,
            GymManagementService.ApplicationLayer
        ];

        foreach (var layer in layers)
        {
            ArchRuleDefinition
                .Types()
                .That()
                .Are(GymManagementService.DomainLayer)
                .Should()
                .NotDependOnAny(layer)
                .Check(AllArchitecture);
        }
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHave_Dependencies_OnAdapterLayer()
    {
        IObjectProvider<IType>[] adapterLayers = [
            GymManagementService.AdapterInfrastructureLayer,
            GymManagementService.AdapterPersistenceLayer,
            GymManagementService.AdapterPresentationLayer
        ];

        foreach (var adapterLayer in adapterLayers)
        {
            ArchRuleDefinition
                .Types()
                .That()
                .Are(GymManagementService.ApplicationLayer)
                .Should()
                .NotDependOnAny(adapterLayer)
                .Check(AllArchitecture);
        }
    }
}