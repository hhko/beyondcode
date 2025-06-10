using ArchUnitNET.Domain;
using ArchUnitNET.Fluent.Conditions;

namespace GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.Conditions;

public sealed class SyntaxConditionBuilder<T>
    where T : ICanBeAnalyzed
{
    private readonly List<ICondition<T>> _conditions = [];

    public SyntaxConditionBuilder<T> MustSatisfy(Func<T, bool> predicate, string description)
    {
        _conditions.Add(new PredicateCondition<T>(predicate, description));
        return this;
    }

    public ICondition<T> Build()
    {
        return new CompositeCondition<T>(_conditions);
    }
}

