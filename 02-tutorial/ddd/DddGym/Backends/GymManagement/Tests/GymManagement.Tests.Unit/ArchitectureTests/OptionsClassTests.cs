using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using FunctionalDdd.Framework.BaseTypes;
using GymManagement.Tests.Unit.ArchitectureTests.Conditions;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class OptionsClassTests : ArchitectureTestBase
{
    [Fact]
    public void OptionsClasses_Should_Have_SectionName_ConstField()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .HaveNameEndingWith("Options")
            .Should()
            .HaveSectionNameConstField()
            .Check(Architecture);
    }
}
