using ArchUnitNET.Fluent;

namespace GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules.Utilities;

public static class ArchUnitNetRuleUtilities
{
    public static IArchitectureRule ToArchitectureRule(this IArchRule archRule, string ruleName = "")
    {
        return new ArchUnitNetAdapterRule(archRule, ruleName);
    }
}