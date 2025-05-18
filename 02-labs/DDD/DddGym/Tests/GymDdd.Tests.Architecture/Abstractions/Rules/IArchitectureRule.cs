namespace GymDdd.Tests.Architecture.Abstractions.Rules;

public interface IArchitectureRule
{
    string RuleName { get; }
    RuleEvaluationResult Evaluate(ArchUnitNET.Domain.Architecture architecture);
}
