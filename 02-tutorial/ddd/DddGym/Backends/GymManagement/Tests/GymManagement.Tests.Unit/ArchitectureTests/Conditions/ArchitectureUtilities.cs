using ArchUnitNET.Domain;
using ArchUnitNET.Fluent.Syntax.Elements;
using ArchUnitNET.Fluent.Syntax;

namespace GymManagement.Tests.Unit.ArchitectureTests.Conditions;

// Enforcing Architecture Rules In .NET
// https://honesdev.com/enforcing-architecture-rules-in-dotnet/

public static partial class ArchitectureUtilities
{
    public static TRuleTypeShouldConjunction HaveStaticMethod<TRuleTypeShouldConjunction, TRuleType>(
        this ObjectsShould<TRuleTypeShouldConjunction, TRuleType> should,
        string prefix)
            where TRuleType : ICanBeAnalyzed
            where TRuleTypeShouldConjunction : SyntaxElement<TRuleType>
    {
        var condition = new HaveStaticMethodCondition<TRuleType>(prefix);
        return should.FollowCustomCondition(condition);
    }

    public static TRuleTypeShouldConjunction HavePrivateParameterlessConstructor<TRuleTypeShouldConjunction, TRuleType>(
        this ObjectsShould<TRuleTypeShouldConjunction, TRuleType> should)
            where TRuleType : ICanBeAnalyzed
            where TRuleTypeShouldConjunction : SyntaxElement<TRuleType>
    {
        var condition = new HavePrivateParameterlessConstructorCondition<TRuleType>();
        return should.FollowCustomCondition(condition);
    }

    public static TRuleTypeShouldConjunction HaveSectionNameField<TRuleTypeShouldConjunction, TRuleType>(
        this ObjectsShould<TRuleTypeShouldConjunction, TRuleType> should)
            where TRuleType : ICanBeAnalyzed
            where TRuleTypeShouldConjunction : SyntaxElement<TRuleType>
    {
        var condition = new HaveSectionNameFieldCondition<TRuleType>();
        return should.FollowCustomCondition(condition);
    }
}
