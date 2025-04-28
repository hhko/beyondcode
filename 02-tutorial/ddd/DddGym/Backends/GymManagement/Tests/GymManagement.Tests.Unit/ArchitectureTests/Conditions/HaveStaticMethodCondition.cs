using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using ArchUnitNET.Fluent.Conditions;

namespace GymManagement.Tests.Unit.ArchitectureTests.Conditions;

internal sealed class HaveStaticMethodCondition<TRuleType> : ICondition<TRuleType>
    where TRuleType : ICanBeAnalyzed
{
    private readonly string _prefix;

    public HaveStaticMethodCondition(string prefix)
    {
        _prefix = prefix;
    }

    public string Description => $"does not have a method name starting with {_prefix}";

    public IEnumerable<ConditionResult> Check(IEnumerable<TRuleType> objects, Architecture architecture)
    {
        foreach (var @class in objects)
        {
            Class? classObject = @class as Class;
            if (classObject == null)
            {
                string actualType = @class?.GetType().FullName ?? "null";
                string expectedType = typeof(Class).FullName ?? "null";

                throw new InvalidCastException($"Type cast failed: actual type is '{actualType}', expected type was '{expectedType}'.");
            }

            bool hasMatchingMethod = classObject
                .GetMethodMembers()
                .Where(member =>
                    member.Name.StartsWith(_prefix) &&
                    member.IsStatic == true)
                .Any();

            yield return new ConditionResult(
                analyzedObject: @class,
                pass: hasMatchingMethod,
                failDescription: hasMatchingMethod
                    ? null
                    : Description);
        }
    }

    public bool CheckEmpty() =>
        true;
}
