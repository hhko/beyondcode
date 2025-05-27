using ArchUnitNET.Domain;
using GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.Conditions;
using System.Reflection;

namespace GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.Musts;

public static partial class Must
{
    public static IArchitectureRule HaveNamedNestedClassMatches<T>(
        IObjectProvider<T> provider,
        params (string nestedClassName, Func<Type, bool> nestedClassCondition)[] matches)
        where T : Class
    {
        if (matches == null || matches.Length == 0)
            throw new ArgumentException("At least one nested class condition must be specified.", nameof(matches));

        SyntaxConditionBuilder<T> builder = new();

        foreach (var (nestedClassName, nestedClassCondition) in matches)
        {
            builder.MustSatisfy(
                @class =>
                {
                    var type = FindTypeByFullName(@class.FullName);
                    if (type == null)
                        return false;

                    var nestedClass = FindNestedClassByName(type, nestedClassName);
                    return nestedClass != null
                        && nestedClassCondition(nestedClass);
                },
                $"does not satisfy a nested class condition '{nestedClassName}'");
        }

        string ruleName = matches.Length == 1
            ? $"must have a nested class matching condition '{matches[0].nestedClassName}'"
            : $"must have nested classes matching conditions {string.Join(", ", matches.Select(m => $"'{m.nestedClassName}'"))}";

        return new ArchitectureRule<T>(
            ruleName: ruleName,
            provider: provider,
            condition: builder.Build());
    }

    private static Type? FindTypeByFullName(string? fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return null;

        return AppDomain.CurrentDomain
            .GetAssemblies()
            .Select(a => a.GetType(fullName, throwOnError: false))
            .FirstOrDefault(t => t != null);
    }

    private static Type? FindNestedClassByName(Type parentType, string nestedClassName)
    {
        return parentType
            .GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic)
            .FirstOrDefault(n => n.Name.Equals(nestedClassName, StringComparison.Ordinal));
    }
}



