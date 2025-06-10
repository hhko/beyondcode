using ArchUnitNET.Fluent.Conditions;

namespace GymDdd.Tests.Architecture.Abstractions.ArchitectureRules;

public sealed class RuleEvaluationResult
{
    public string RuleName { get; }
    public List<ConditionResult> Results { get; }

    public RuleEvaluationResult(string ruleName, List<ConditionResult> results)
    {
        RuleName = ruleName;
        Results = results;
    }

    public bool HasViolation => Results.Any();
}
