namespace DomainTypes_ValueObjects.Tests.Abstractions.ArchitectureRules;

public interface IArchitectureRule
{
    string RuleName { get; }
    RuleEvaluationResult Evaluate(ArchUnitNET.Domain.Architecture architecture);
}
