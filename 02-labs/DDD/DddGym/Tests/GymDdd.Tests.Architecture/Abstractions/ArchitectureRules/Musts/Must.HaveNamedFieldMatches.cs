using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.Conditions;
using GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.SyntaxLevelRules;

namespace GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.Musts;

public static partial class Must
{
    public static IArchitectureRule HaveNamedFieldMatches<T>(
        IObjectProvider<T> provider,
        params (string fieldName, Func<FieldMember, bool> fieldCondition)[] matches)
        where T : Class
    {
        if (matches == null || matches.Length == 0)
            throw new ArgumentException("At least one field condition must be specified.", nameof(matches));

        SyntaxConditionBuilder<T> builder = new();

        foreach (var (fieldName, fieldCondition) in matches)
        {
            builder.MustSatisfy(
                @class =>
                {
                    FieldMember? field = FindFieldByName(@class, fieldName);
                    return field != null
                        && fieldCondition(field);
                },
                $"does not satisfy a method condition '{fieldName}'");
        }

        string ruleName = matches.Length == 1
            ? $"must have a field matching condition '{matches[0].fieldName}'"
            : $"must have field matching conditions {string.Join(", ", matches.Select(m => $"'{m.fieldName}'"))}";

        return new ArchitectureRule<T>(
            ruleName: ruleName,
            provider: provider,
            condition: builder.Build());
    }

    private static FieldMember? FindFieldByName(Class @class, string fieldName)
    {
        return @class.GetFieldMembers()
                     .FirstOrDefault(f => f.Name == fieldName);
    }
}


