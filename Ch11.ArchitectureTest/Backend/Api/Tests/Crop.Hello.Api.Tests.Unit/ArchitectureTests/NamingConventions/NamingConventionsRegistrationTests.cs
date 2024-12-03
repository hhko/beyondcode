using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Conditions;
using ArchUnitNET.xUnit;
using Crop.Hello.Framework.Contracts.CQRS;
using Microsoft.Extensions.Options;
using static Crop.Hello.Api.Tests.Unit.Abstractions.Constants.Constants;

namespace Crop.Hello.Api.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed class NamingConventionsRegistrationTests : ArchitectureBaseTest
{
    [Fact]
    public void OptionValidator_ShouldComplyWith_DesignRules()
    {
        // TODO: static
        //ArchRuleDefinition
        //    .Classes()
        //    .That()
        //    .HaveNameEndingWith("Registration1")
        //    .Should()
        //    . static
    }
}