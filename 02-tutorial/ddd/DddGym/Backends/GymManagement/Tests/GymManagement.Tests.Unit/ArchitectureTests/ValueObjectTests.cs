using ArchUnitNET.Fluent;
using FunctionalDdd.Framework.BaseTypes;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;
using ArchUnitNET.xUnit;
using GymManagement.Tests.Unit.ArchitectureTests.Conditions;

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
        // Arrange
        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValueObject));

        // Act
        var classes = sut.GetObjects(Architecture);
        if (!classes.Any())
            return;

        // Assert
        sut.Should()
            .BeSealed()
            .Check(Architecture);
    }

    [Theory]
    [InlineData(IValueObject.CreateMethodName)]
    [InlineData(IValueObject.ValidateMethodName)]
    public void ValueObjectClasses_ShouldHave_StaticMethod(string requiredMethodName)
    {
        // Arrange
        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValueObject));

        // Act
        var classes = sut.GetObjects(Architecture);
        if (!classes.Any())
            return;

        // Assert
        sut.Should()
            .HaveStaticMethod(requiredMethodName)
            .Check(Architecture);
    }

    [Fact]
    public void ValueObjectClasses_ShouldHave_PrivateParameterlessConstructor()
    {
        // Arrange
        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValueObject));

        // Act
        var classes = sut.GetObjects(Architecture);
        if (!classes.Any())
            return;

        // Assert
        sut.Should()
            .HavePrivateParameterlessConstructor()
            .Check(Architecture);
    }
}
