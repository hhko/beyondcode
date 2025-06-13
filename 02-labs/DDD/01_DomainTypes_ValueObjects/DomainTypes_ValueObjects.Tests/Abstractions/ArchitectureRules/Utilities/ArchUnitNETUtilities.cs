using ArchUnitNET.Fluent;
using DomainTypes_ValueObjects.Tests.Abstractions.ArchitectureRules;

namespace DomainTypes_ValueObjects.Tests.Abstractions.ArchitectureRules.Utilities;

public static class ArchUnitNETUtilities
{
    public static IArchitectureRule ToArchitectureRule(this IArchRule archRule, string ruleName = "")
    {
        return new ArchitectureRuleAdapter(archRule, ruleName);
    }
}