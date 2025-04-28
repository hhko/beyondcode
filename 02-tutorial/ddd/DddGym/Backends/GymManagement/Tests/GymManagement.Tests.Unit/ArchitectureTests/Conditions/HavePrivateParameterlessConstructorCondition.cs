using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using ArchUnitNET.Fluent.Conditions;

namespace GymManagement.Tests.Unit.ArchitectureTests.Conditions;

internal sealed class HavePrivateParameterlessConstructorCondition<TRuleType> : ICondition<TRuleType>
    where TRuleType : ICanBeAnalyzed
{
    public string Description => $"does not have private parameter less constructor";

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

            bool hasPrivateParameterlessConstructor = classObject
                .GetConstructors()
                .Any(c => 
                    !c.Parameters.Any() && 
                    c.Visibility == Visibility.Private);

            yield return new ConditionResult(
                analyzedObject: @class,
                pass: hasPrivateParameterlessConstructor,
                failDescription: hasPrivateParameterlessConstructor
                    ? null
                    : Description);
        }
    }

    public bool CheckEmpty() =>
        true;
}