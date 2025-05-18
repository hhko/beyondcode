using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using GymDdd.Tests.Architecture.Abstractions.Rules;
using GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules;
using GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules.Conditions;

namespace GymDdd.Tests.Architecture.ArchitectureTests.Rules.SyntaxLevelRules;

public partial class Must
{
    public static IArchitectureRule HaveAllPrivateConstructors<T>(IObjectProvider<T> provider)
        where T : Class
    {
        return new SyntaxLevelRule<T>(
            ruleName: "must have only private constructors",
            provider: provider,
            condition: new SyntaxConditionBuilder<T>()
                .MustSatisfy(
                    @class =>
                    {
                        // 모든 생성자가 private 인지 확인
                        return @class
                            .GetConstructors()
                            .All(c => c.Visibility == Visibility.Private);
                    },
                    "not all constructors are private")
                .Build()
        );
    }
}
