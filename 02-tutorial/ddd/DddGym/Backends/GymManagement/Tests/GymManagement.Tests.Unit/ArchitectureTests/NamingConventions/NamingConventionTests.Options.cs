using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using FluentValidation;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests.NamingConventions;

// Options 규칙
//
//public sealed class {섹션이름}Options
//{
//    public const string SectionName = "{섹션이름}";
//
//    public required {옵션_타입} {옵션} { get; init; }
//
//    internal sealed class Validator : AbstractValidator<{섹션이름}Options>
//    {
//        public Validator()
//        {
//            {옵션_유효성검사}
//        }
//    }
//}

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
            .Should().BePublic()
            .AndShould().BeSealed()
            .AndShould().HaveFieldMemberWithName(NamingConvention.SectionName)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void OptionsClass_ShouldHave_NestedValidator()
    {
        // Arrange
        var rules = new[]
        {
            new NestedClassRuleBuilder(NamingConvention.Validator)
                .MustBeInternal()
                .MustBeSealed()
                .MustImplementsGenericInterfaceWithOuterTypeArguments(typeof(IValidator<>))
                .Build(),
        };

        // Act
        var violations = CheckNestedClassRules(NamingConvention.Options, rules);

        // Assert
        violations.ShouldBeEmpty("All Options classes must have required nested classes that fulfill the design rules.");
    }
}
