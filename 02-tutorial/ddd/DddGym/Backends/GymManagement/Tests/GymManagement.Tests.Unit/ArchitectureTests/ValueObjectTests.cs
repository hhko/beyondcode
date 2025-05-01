using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using FunctionalDdd.Framework.BaseTypes;
using GymManagement.Tests.Unit.ArchitectureTests.Conditions;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class ValueObjectTests : ArchitectureTestBase
{
    //[Fact]
    //public void IValueObjects_ShouldBe_Immutable()
    //{
    //    ////Arrange
    //    //var assembly = Domain.AssemblyReference.Assembly;

    //    ////Act
    //    //var result = Types
    //    //    .InAssembly(assembly)
    //    //    .That()
    //    //    .ImplementInterface(typeof(IValueObject))
    //    //    .And()
    //    //    .AreNotAbstract()
    //    //    .Should()
    //    //    .BeImmutable()
    //    //    .GetResult();

    //    ////Assert
    //    //result.IsSuccessful.Should().BeTrue();

    //    var sut = ArchRuleDefinition
    //        .Classes()
    //        .That()
    //        .ImplementInterface(typeof(IValueObject));

    //    var classes = sut.GetObjects(Architecture);
    //    if (!classes.Any())
    //        return;

    //    sut.Should().BeImmutable();
    //}

    [Fact]
    public void ValueObjectClasses_ShouldBe_Sealed()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValueObject))
            .Should().BeSealed()
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Theory]
    [InlineData(IValueObject.CreateMethodName)]
    [InlineData(IValueObject.ValidateMethodName)]
    public void ValueObjectClasses_ShouldHave_StaticMethod(string requiredMethodName)
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValueObject))
            .Should().HaveStaticMethod(requiredMethodName)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void ValueObjectClasses_ShouldHave_PrivateParameterlessConstructor()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValueObject))
            .Should().HavePrivateParameterlessConstructor()
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }
}
