using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Conditions;

namespace DomainTypes_ValueObjects.Tests.Abstractions.ArchitectureRules;

public sealed class ArchitectureRuleAdapter : IArchitectureRule
{
    private readonly IArchRule _archRule;
    private readonly string _ruleName;

    public ArchitectureRuleAdapter(IArchRule archRule, string ruleName)
    {
        _archRule = archRule ?? throw new ArgumentNullException(nameof(archRule));

        _ruleName = string.IsNullOrWhiteSpace(ruleName)
            ? _archRule.ToString()!
            : ruleName;
    }

    public string RuleName => _ruleName;

    public RuleEvaluationResult Evaluate(ArchUnitNET.Domain.Architecture architecture)
    {
        IEnumerable<EvaluationResult> evaluationResults = _archRule.Evaluate(architecture);

        var violations = evaluationResults
            .Where(r => !r.Passed)
            .Select(r =>
            {
                return new ConditionResult(
                    analyzedObject: r.EvaluatedObject as ICanBeAnalyzed,
                    pass: false,
                    failDescription: r.Description);
            })
            .ToList();

        return new RuleEvaluationResult(RuleName, violations);
    }
}
