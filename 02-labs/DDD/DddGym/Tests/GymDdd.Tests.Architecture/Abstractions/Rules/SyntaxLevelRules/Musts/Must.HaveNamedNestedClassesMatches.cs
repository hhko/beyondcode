using ArchUnitNET.Domain;
using GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules.Conditions;
using System.Reflection;

namespace GymDdd.Tests.Architecture.Abstractions.Rules.SyntaxLevelRules.Musts;

public partial class Must
{
    //public static IArchitectureRule HaveInternalSealedNestedClass<T>(
    //    IObjectProvider<T> provider,
    //    string nestedClassName)
    //    where T : Class
    //{
    //    return new SyntaxLevelRule<T>(
    //        ruleName: $"must have a nested class named '{nestedClassName}' that is internal and sealed",
    //        provider: provider,
    //        condition: new SyntaxConditionBuilder<T>()
    //            .MustSatisfy(
    //                @class =>
    //                {
    //                    // 실제 Type 정보 가져오기
    //                    Type? type = AppDomain.CurrentDomain
    //                        .GetAssemblies()
    //                        .Select(a => a.GetType(@class.FullName, false))
    //                        .FirstOrDefault(t => t != null);

    //                    if (type == null)
    //                        return false;

    //                    // 내부 클래스들 (non-public 포함)
    //                    var nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic);

    //                    // 지정된 이름의 중첩 클래스 찾기
    //                    var nested = nestedTypes
    //                        .FirstOrDefault(n => n.Name.Equals(nestedClassName, StringComparison.Ordinal));

    //                    //foreach (var @interface in nested.GetInterfaces())
    //                    //{
    //                    //    bool isGenericType = @interface.IsGenericType == true;
    //                    //    var type1 = @interface.GetGenericTypeDefinition();      // Parenttype<GenericType> : Parenttype
    //                    //    var type2 = @interface.GenericTypeArguments[0];         // Parenttype<GenericType> : GenericType
    //                    //}

    //                    // 조건 검사: internal & sealed
    //                    return nested is not null &&
    //                           nested.IsNestedAssembly &&  // internal
    //                           nested.IsSealed;
    //                },
    //                $"missing a nested class named '{nestedClassName}' that is internal and sealed")
    //            .Build()
    //    );
    //}

    public static IArchitectureRule HaveNamedNestedClassesMatches<T>(
        IObjectProvider<T> provider,
        Dictionary<string, Func<Type, bool>> namedClassConditions)
        where T : Class
    {
        return new SyntaxLevelRule<T>(
            ruleName: $"must have a nested class",
            provider: provider,
            condition: new SyntaxConditionBuilder<T>()
                .MustSatisfy(
                    @class =>
                    {
                        var type = AppDomain.CurrentDomain
                            .GetAssemblies()
                            .Select(a => a.GetType(@class.FullName, false))
                            .FirstOrDefault(t => t != null);

                        if (type == null)
                            return false;

                        var nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic);

                        // 모든 조건을 개별적으로 검사
                        foreach ((string expectedName, Func<Type, bool> condition) in namedClassConditions)
                        {
                            var match = nestedTypes.FirstOrDefault(n => n.Name.Equals(expectedName, StringComparison.Ordinal));
                            if (match == null || !condition(match))
                            {
                                return false;
                            }
                        }

                        return true;
                    },
                    $"does not satisfy nested class condition(s)")
                .Build()
        );
    }

    public static IArchitectureRule HaveInternalSealedNestedClass<T>(
        IObjectProvider<T> provider,
        params string[] nestedClassNames)
        where T : Class
    {
        if (nestedClassNames == null || !nestedClassNames.Any())
            throw new ArgumentException("At least one nested class name must be specified.", nameof(nestedClassNames));

        SyntaxConditionBuilder<T> builder = new();

        foreach (var nestedClassName in nestedClassNames)
        {
            builder.MustSatisfy(
                @class =>
                {
                    // System.Type 정보 가져오기
                    System.Type? type = AppDomain.CurrentDomain
                        .GetAssemblies()
                        .Select(a => a.GetType(@class.FullName, false))
                        .FirstOrDefault(t => t != null);

                    if (type == null)
                        return false;

                    // 중첩 클래스 (non-public 포함)
                    var nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic);

                    // 지정된 이름의 중첩 클래스 찾기
                    var nested = nestedTypes
                        .FirstOrDefault(n => n.Name.Equals(nestedClassName, StringComparison.Ordinal));

                    //foreach (var @interface in nested.GetInterfaces())
                    //{
                    //    bool isGenericType = @interface.IsGenericType == true;
                    //    var type1 = @interface.GetGenericTypeDefinition();      // Parenttype<GenericType> : Parenttype
                    //    var type2 = @interface.GenericTypeArguments[0];         // Parenttype<GenericType> : GenericType
                    //}

                    // 조건 검사: internal & sealed
                    return nested is not null &&
                           nested.IsNestedAssembly &&  // internal
                           nested.IsSealed;
                },
                $"missing a nested class named '{nestedClassName}' that is internal and sealed");
        }

        return new SyntaxLevelRule<T>(
            ruleName: nestedClassNames.Length == 1
                ? $"must have a nested class name '{nestedClassNames[0]}' that is internal and sealed"
                : $"must have nested class names {string.Join(", ", nestedClassNames.Select(n => $"'{n}'"))} that is internal and sealed",
            provider: provider,
            condition: builder.Build());
    }
}



