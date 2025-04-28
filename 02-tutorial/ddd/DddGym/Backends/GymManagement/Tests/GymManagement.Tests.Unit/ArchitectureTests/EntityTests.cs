using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Syntax.Elements;
using ArchUnitNET.Fluent.Syntax;
using ArchUnitNET.xUnit;
using FunctionalDdd.Framework.BaseTypes;
using GymManagement.Tests.Unit.ArchitectureTests.Conditions;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;
using ArchUnitNET.Fluent.Conditions;

namespace GymManagement.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class EntityTests : ArchitectureTestBase
{
    [Fact]
    public void EntityClasses_ShouldBe_Sealed()
    {
        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IEntity));

        var classes = sut.GetObjects(Architecture);
        if (!classes.Any())
            return;

        sut.Should()
            .BeSealed()
            .Check(Architecture);
    }

    [Theory]
    [InlineData(IEntity.CreateMethodName)]
    public void EntityClasses_ShouldBeSealed_And_HaveStaticMethod(string requiredMethodName)
    {
        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IEntity));

        var classes = sut.GetObjects(Architecture);
        if (!classes.Any())
            return;

        sut.Should().BeSealed()
            .AndShould().HaveStaticMethod(requiredMethodName)
            .Check(Architecture);
    }

    [Fact]
    public void EntityClasses_ShouldHave_PrivateParameterlessConstructor()
    {
        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IEntity));

        var classes = sut.GetObjects(Architecture);
        if (!classes.Any())
            return;

        sut.Should()
            .HavePrivateParameterlessConstructor()
            .Check(Architecture);
    }
}

