using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using GymDdd.Tests.Architecture.Abstractions.Rules;
using GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules;
using GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules.Conditions;

namespace GymDdd.Tests.Architecture.ArchitectureTests.Rules.SyntaxLevelRules;

public partial class Must
{
    public static IArchitectureRule HavePrivateParameterlessConstructor<T>(IObjectProvider<T> provider)
        where T : Class //ICanBeAnalyzed
    {
        return new SyntaxLevelRule<T>(
            ruleName: "must have a private parameterless constructor",
            provider: provider,
            condition: new SyntaxConditionBuilder<T>()
                .MustSatisfy(
                    @class =>
                    {
                        return @class
                            .GetConstructors()
                            .Any(c =>
                                !c.Parameters.Any() &&
                                c.Visibility == Visibility.Private);
                    },
                    "do not have a private parameterless constructor")
                .Build()
        );
    }
}
