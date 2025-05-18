using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using GymDdd.Tests.Architecture.Abstractions.Rules;
using GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules;
using GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules.Conditions;

namespace GymDdd.Tests.Architecture.ArchitectureTests.Rules.SyntaxLevelRules;

public partial class Must
{
    // methodNames 1 ~ N개
    public static IArchitectureRule HaveStaticMethod<T>(IObjectProvider<T> provider, params string[] methodNames)
        where T : Class
    {
        if (methodNames == null || !methodNames.Any())
            throw new ArgumentException("At least one method name must be specified.", nameof(methodNames));

        SyntaxConditionBuilder<T> builder = new();

        foreach (var methodName in methodNames)
        {
            builder.MustSatisfy(
                @class =>
                {
                    return @class.GetMethodMembers()
                                 .Where(member => member.Name.StartsWith(methodName) &&
                                                  member.IsStatic == true &&
                                                  member.Visibility == Visibility.Public)
                                 .Any();
                },
                $"do not have a public static method '{methodName}'");
        }

        return new SyntaxLevelRule<T>(
            ruleName: methodNames.Length == 1
                    ? $"must have a public static method '{methodNames[0]}'"
                    : $"must have public static methods {string.Join(", ", methodNames.Select(n => $"'{n}'"))}",
            provider: provider,
            condition: builder.Build());
    }
}
