using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using ArchUnitNET.Fluent.Conditions;

internal sealed class HaveSectionNameConstFieldCondition<TRuleType>
    : ICondition<TRuleType>
      where TRuleType : ICanBeAnalyzed
{
    public string Description => "does not declare a public const string field named 'SectionName'";

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

            bool hasConstField = classObject
                .GetFieldMembers()
                .Any(f =>
                    f.Name == "SectionName" &&
                    f.Visibility == Visibility.Public &&
                    f.IsStatic == true &&
                    //f.Visibility == Visibility.Public &&
                    //f.IsStatic &&
                    //f.IsConst &&
                    f.Type.FullName == typeof(string).FullName);

            yield return new ConditionResult(
                analyzedObject: classObject,
                pass: hasConstField,
                failDescription: hasConstField ? null : Description);
        }
    }

    public bool CheckEmpty() =>
        true;
}
