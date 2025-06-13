using ArchUnitNET.Domain;
using ArchUnitNET.Fluent.Conditions;

namespace DomainTypes_ValueObjects.Tests.Abstractions.ArchitectureRules.Conditions;

public sealed class PredicateCondition<T>
    : ICondition<T>
      where T : ICanBeAnalyzed
{
    private readonly Func<T, bool> _predicate;
    private readonly string _description;

    public PredicateCondition(Func<T, bool> predicate, string description)
    {
        _predicate = predicate;
        _description = description;
    }

    public string Description => _description;

    public IEnumerable<ConditionResult> Check(IEnumerable<T> objects, ArchUnitNET.Domain.Architecture architecture)
    {
        foreach (var obj in objects)
        {
            bool result = _predicate(obj);

            yield return new ConditionResult(
                analyzedObject: obj,
                pass: result,
                failDescription: result ? string.Empty : _description);
        }
    }

    public bool CheckEmpty() => true;
}

