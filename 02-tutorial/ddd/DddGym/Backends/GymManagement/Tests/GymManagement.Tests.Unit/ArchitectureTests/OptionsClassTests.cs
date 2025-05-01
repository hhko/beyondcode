using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using FluentValidation;
using GymManagement.Tests.Unit.ArchitectureTests.Conditions;
using Shouldly;
using System.Reflection;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class OptionsClassTests : ArchitectureTestBase
{
    private const string Options = nameof(Options);
    private const string SectionName = nameof(SectionName);
    private const string Validator = nameof(Validator);

    [Fact]
    public void OptionsClasses_Should_Have_SectionName_ConstField()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .HaveNameEndingWith(Options)
            .And().AreSealed()
            .And().ArePublic()
            .Should()
            .HaveFieldMemberWithName(SectionName)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void OptionsClass_ShouldHave_NestedValidator()
    {
        var rules = new[]
        {
            new NestedClassRule(
                Validator,
                (outer, nested) =>
                    nested.IsSealed &&                      // sealed
                    !nested.IsNestedPublic &&               // internal
                    nested.GetInterfaces().Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition().Name == typeof(IValidator<>).Name &&   // "IValidator`1" 
                        i.GenericTypeArguments[0] == outer)),                               // IValidator<Outer>
        };

        var violations = CheckNestedClassRules(Options, rules);

        violations.ShouldBeEmpty("All Options classes must have required nested classes that fulfill the design rules (e.g., Validator, Mapper).");
    }
}
