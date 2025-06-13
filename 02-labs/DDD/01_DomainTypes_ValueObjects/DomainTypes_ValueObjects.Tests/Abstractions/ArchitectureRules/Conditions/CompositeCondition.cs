using ArchUnitNET.Domain;
using ArchUnitNET.Fluent.Conditions;

namespace DomainTypes_ValueObjects.Tests.Abstractions.ArchitectureRules.Conditions;

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
                    //yield return new ConditionResult(obj, false, $"{condition.Description}: {result.FailDescription}"); // 실패 정보 구체화
                }
            }
        }
    }

    public bool CheckEmpty() => true;

    //TODO: CheckEmpty 의미 파악
    //public bool CheckEmpty() => _conditions.All(c => c.CheckEmpty());
}

