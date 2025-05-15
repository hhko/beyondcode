using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using FunctionalDdd.Framework.BaseTypes;
using GymManagement.Tests.Unit.ArchitectureTests.Conditions;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class EntityTests : ArchitectureTestBase
{
    [Fact]
    public void EntityClasses_ShouldBe_Sealed()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IEntity))
            .Should().BeSealed()
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Theory]
    [InlineData(IEntity.CreateMethodName)]
    public void EntityClasses_ShouldBeSealed_And_HaveStaticMethod(string requiredMethodName)
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IEntity))
            .Should().BeSealed()
            .AndShould().HaveStaticMethod(requiredMethodName)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void EntityClasses_ShouldHave_PrivateParameterlessConstructor()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IEntity))
            .Should().HavePrivateParameterlessConstructor()
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }
}

