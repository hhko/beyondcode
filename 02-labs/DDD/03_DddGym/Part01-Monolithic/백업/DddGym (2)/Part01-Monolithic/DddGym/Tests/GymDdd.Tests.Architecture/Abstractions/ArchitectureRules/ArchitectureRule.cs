using ArchUnitNET.Domain;
using ArchUnitNET.Fluent.Conditions;

namespace GymDdd.Tests.Architecture.Abstractions.ArchitectureRules;

public class ArchitectureRule<T>
    : IArchitectureRule
      where T : ICanBeAnalyzed
{
    private readonly string _ruleName;
    private readonly ICondition<T> _condition;
    private readonly IObjectProvider<T> _provider;

    public ArchitectureRule(string ruleName, IObjectProvider<T> provider, ICondition<T> condition)
    {
        _ruleName = ruleName;
        _provider = provider;
        _condition = condition;
    }

    public string RuleName => _ruleName;

    public RuleEvaluationResult Evaluate(ArchUnitNET.Domain.Architecture architecture)
    {
        var objects = _provider.GetObjects(architecture);
        var failedResults = _condition
            .Check(objects, architecture)
            .Where(r => !r.Pass)
            .ToList();

        return new RuleEvaluationResult(_ruleName, failedResults);
    }
}
