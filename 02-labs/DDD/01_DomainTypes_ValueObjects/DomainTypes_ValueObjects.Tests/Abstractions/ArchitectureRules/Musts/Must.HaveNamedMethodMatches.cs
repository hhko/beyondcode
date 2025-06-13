using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using DomainTypes_ValueObjects.Tests.Abstractions.ArchitectureRules;
using DomainTypes_ValueObjects.Tests.Abstractions.ArchitectureRules.Conditions;
using System.Text.RegularExpressions;

namespace DomainTypes_ValueObjects.Tests.Abstractions.ArchitectureRules.Musts;

public static partial class Must
{
    public static IArchitectureRule HaveNamedMethodMatches<T>(
        IObjectProvider<T> provider,
        params (string methodName, Func<MethodMember, bool> methodCondition)[] matches)
        where T : Class
    {
        if (matches == null || matches.Length == 0)
            throw new ArgumentException("At least one method condition must be specified.", nameof(matches));

        SyntaxConditionBuilder<T> builder = new();

        foreach (var (methodName, methodCondition) in matches)
        {
            builder.MustSatisfy(
                @class =>
                {
                    MethodMember? method = FindMethodByName(@class, methodName);
                    return method != null
                        && methodCondition(method);
                },
                $"does not satisfy a method condition '{methodName}'");
        }

        string ruleName = matches.Length == 1
            ? $"must have a method matching condition '{matches[0].methodName}'"
            : $"must have method matching conditions {string.Join(", ", matches.Select(m => $"'{m.methodName}'"))}";

        return new ArchitectureRule<T>(
            ruleName: ruleName,
            provider: provider,
            condition: builder.Build());
    }

    private static MethodMember? FindMethodByName(Class @class, string methodName)
    {
        // 메서드 이름에 파라미터까지 포함되어 있습니다.
        //  예. Create(Systme.String, ...)
        return @class.GetMethodMembers()
                     .FirstOrDefault(m => Regex.Match(m.Name, @"^(.+?)\(").Groups[1].Value.Equals(methodName, StringComparison.Ordinal));
    }
}
