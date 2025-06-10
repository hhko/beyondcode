namespace GymDdd.Tests.Architecture.Abstractions.ArchitectureRules;

public interface IArchitectureRule
{
    string RuleName { get; }
    RuleEvaluationResult Evaluate(ArchUnitNET.Domain.Architecture architecture);
}
