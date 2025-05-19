using ArchUnitNET.Fluent;

namespace GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.SyntaxLevelRules.Utilities;

public static class ArchUnitNETUtilities
{
    public static IArchitectureRule ToArchitectureRule(this IArchRule archRule, string ruleName = "")
    {
        return new ArchitectureRuleAdapter(archRule, ruleName);
    }
}