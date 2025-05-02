using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using FluentValidation;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class NamingConventionOptionsTests : ArchitectureTestBase
{
    [Fact]
    public void OptionsClasses_ShouldHave_SectionName()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .HaveNameEndingWith(NamingConvention.Options)
            .And().AreSealed()
            .And().ArePublic()
            .Should()
            .HaveFieldMemberWithName(NamingConvention.SectionName)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void OptionsClass_ShouldHave_NestedValidator()
    {
        // Arrange
        var rules = new[]
        {
            new NestedClassRule(
                NamingConvention.Validator,                 // Validator 클래스 이름
                (outer, nested) =>
                    !nested.IsNestedPublic &&               // internal
                    nested.IsSealed &&                      // sealed
                    nested.GetInterfaces().Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition().Name == typeof(IValidator<>).Name &&   // IValidator<T> 상속
                        i.GenericTypeArguments[0] == outer)),                               // IValidator<T>의 T 타입
        };

        // Act
        var violations = CheckNestedClassRules(NamingConvention.Options, rules);

        // Assert
        violations.ShouldBeEmpty("All Options classes must have required nested classes that fulfill the design rules.");
    }
}
