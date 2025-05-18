using ArchUnitNET.Domain;
using ArchUnitNET.Fluent.Conditions;

namespace GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules.Conditions;

public sealed class CompositeCondition<T>
    : ICondition<T>
      where T : ICanBeAnalyzed
{
    private readonly IEnumerable<ICondition<T>> _conditions;

    public CompositeCondition(IEnumerable<ICondition<T>> conditions)
    {
        _conditions = conditions;
    }

    public string Description => "One or more conditions failed";

    public IEnumerable<ConditionResult> Check(IEnumerable<T> objects, ArchUnitNET.Domain.Architecture architecture)
    {
        foreach (var obj in objects)
        {
            foreach (var condition in _conditions)
            {
                foreach (var result in condition.Check([obj], architecture))
                {
                    if (!result.Pass)
                        yield return result;
                }
            }
        }
    }

    public bool CheckEmpty() => true;
}

