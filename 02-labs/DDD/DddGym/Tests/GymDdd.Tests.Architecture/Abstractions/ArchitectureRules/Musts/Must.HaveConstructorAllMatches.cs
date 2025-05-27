using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.Conditions;

namespace GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.Musts;

public static partial class Must
{
    public static IArchitectureRule HaveConstructorAllMatches<T>(
        IObjectProvider<T> provider,
        params Func<MethodMember, bool>[] matches)
        where T : Class
    {
        if (matches == null || matches.Length == 0)
            throw new ArgumentException("At least one constructor condition must be specified.", nameof(matches));

        SyntaxConditionBuilder<T> builder = new();

        foreach (var ctorCondition in matches)
        {
            builder.MustSatisfy(
                @class =>
                {
                    foreach (MethodMember ctor in @class.GetConstructors())
                    {
                        if (!ctorCondition(ctor))
                            return false;
                    }

                    return true;
                },
                $"does not satisfy all constructor conditions");
        }

        string ruleName = $"must have all matching conditions";

        return new ArchitectureRule<T>(
            ruleName: ruleName,
            provider: provider,
            condition: builder.Build());
    }
}
