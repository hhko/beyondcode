using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using FunctionalDdd.Framework.BaseTypes;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using ArchUnitNET.Fluent.Conditions;
using Microsoft.AspNetCore.Routing;
using ArchUnitNET.Domain.Extensions;

namespace GymManagement.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class ValueObjectTests : ArchitectureTestBase
{
    [Theory]
    //[InlineData(IValueObject.ValidateMethodName)]
    [InlineData(IValueObject.CreateMethodName)]
    public void IValueObjects_ShouldDefineMethod(string methodName)
    {
        //Arrange
        //var assembly = Domain.AssemblyReference.Assembly;

        ////Act
        //var result = Types
        //    .InAssembly(assembly)
        //    .That()
        //    .ImplementInterface(typeof(IValueObject))
        //    .And()
        //    .AreNotAbstract()
        //    .Should()
        //    .DefineMethod(methodName)
        //    .GetResult();

        ////Assert
        //result.IsSuccessful.Should().BeTrue();

        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValueObject));

        if (!sut.GetObjects(Architecture).Any())
            return;

        //sut
        //    .Should().HaveMethodMemberWithName(
        //    .Check(Architecture);
    }

    
}

public class Hello : ArchitectureTestBase
{

    [Fact]
    public void ClassesImplementing_YourInterface_Should_Have_SpecificMethod()
    {
        var interfaceType = typeof(IValueObject);
        const string requiredMethodName = "Create";

        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(interfaceType);

        var classes = sut.GetObjects(Architecture);

        if (!classes.Any())
            return;

        foreach (var classType in classes)
        {
            //bool hasRequiredMethod = false;
            //var x = classType.Members.OfType<MethodMember>();
            //foreach (var y in x)
            //{
            //    if (y.NameStartsWith(requiredMethodName))
            //    {
            //        hasRequiredMethod = true; 
            //        break;
            //    }
            //}

            var hasRequiredMethod = classType.Members
                .OfType<MethodMember>()
                .Any(m => m.NameStartsWith(requiredMethodName) && m.IsStatic.HasValue);

            Assert.True(
                hasRequiredMethod,
                $"클래스 '{classType.FullName}'는 메서드 '{requiredMethodName}'를 가지고 있어야 합니다."
            );
        }
    }
}