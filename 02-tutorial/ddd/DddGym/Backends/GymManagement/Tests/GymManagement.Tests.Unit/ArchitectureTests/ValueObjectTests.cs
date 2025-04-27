using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using FunctionalDdd.Framework.BaseTypes;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;
using ArchUnitNET.Domain.Extensions;

namespace GymManagement.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class ValueObjectTests : ArchitectureTestBase
{
    [Theory]
    //[InlineData(IValueObject.ValidateMethodName)]
    [InlineData(IValueObject.CreateMethodName)]
    public void IValueObjects_ShouldDefineMethod(string requiredMethodName)
    {
        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValueObject));

        var classes = sut.GetObjects(Architecture);
        if (!classes.Any())
            return;

        foreach (var classType in classes)
        {
            var hasRequiredMethod = classType.Members
                .OfType<MethodMember>()
                .Any(m => 
                    m.NameStartsWith(requiredMethodName) && 
                    m.IsStatic.HasValue);

            //sut.Should().HaveMethodMemberWithName
            Assert.True(
                hasRequiredMethod,
                $"클래스 '{classType.FullName}'는 메서드 '{requiredMethodName}'를 가지고 있어야 합니다."
            );
        }
    }
}
