using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using Microsoft.Extensions.Options;
using static Crop.Hello.Api.Tests.Unit.Abstractions.Constants.Constants;

namespace Crop.Hello.Api.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed class NamingConventionsOptionsTests : ArchitectureBaseTest
{
    [Fact]
    public void OptionValidator_ShouldEndWith_OptionsValidator()
    {
        var suts = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValidateOptions<>));

        if (!suts.GetObjects(Architecture).Any())
            return;

        suts.Should().BeInternal()
            .AndShould().BeSealed()
            .AndShould().HaveNameEndingWith(NamingConvention.OptionsValidator)
            .Check(Architecture);
    }

    [Fact]
    public void OptionsSetup_ShouldEndWith_OptionsSetup()
    {
        var suts = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IConfigureOptions<>));

        if (!suts.GetObjects(Architecture).Any())
            return;

        suts.Should().BeInternal()
            .AndShould().BeSealed()
            .AndShould().HaveNameEndingWith(NamingConvention.OptionsSetup)
            .Check(Architecture);
    }
}