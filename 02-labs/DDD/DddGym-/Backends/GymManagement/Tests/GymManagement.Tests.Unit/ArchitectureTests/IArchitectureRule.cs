using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Conditions;
using FluentValidation;
using GymManagement.Tests.Unit.ArchitectureTests.Conditions;
using System.Reflection;

namespace GymManagement.Tests.Unit.ArchitectureTests;

public interface IArchitectureRule
{
    string RuleName { get; }
    IReadOnlyDictionary<string, List<string>> Evaluate(Architecture architecture);
}

// Syntax 기반 규칙 어댑터: ArchUnitNET
public class SyntaxLevelRule<T> : IArchitectureRule where T : ICanBeAnalyzed
{
    private readonly string _ruleName;
    private readonly ICondition<T> _condition;
    private readonly IObjectProvider<T> _provider;

    public SyntaxLevelRule(string ruleName, IObjectProvider<T> provider, ICondition<T> condition)
    {
        _ruleName = ruleName;
        _provider = provider;
        _condition = condition;
    }

    public string RuleName => _ruleName;

    public IReadOnlyDictionary<string, List<string>> Evaluate(Architecture architecture)
    {
        var objects = _provider.GetObjects(architecture);
        var results = _condition.Check(objects, architecture);

        return results
            .Where(r => !r.Pass)
            .GroupBy(r => r.AnalyzedObject.FullName ?? "Unknown")
            .ToDictionary(
                g => g.Key,
                g => g.Select(r => r.FailDescription ?? "Unknown failure")
                      .ToList());
    }
}

// Reflection 기반 규칙 어댑터: System.Type
public class ReflectionLevelRule : IArchitectureRule
{
    private readonly string _ruleName;
    private readonly string _classNameEnding;
    //private readonly IEnumerable<NestedClassRule> _rules;
    //private readonly IEnumerable<PredicateRule> _rules;
    private readonly IEnumerable<IRule<(Type Outer, Type Nested)>> _rules;

    //public ReflectionLevelRule(string ruleName, string classNameEnding, IEnumerable<NestedClassRule> rules)
    public ReflectionLevelRule(string ruleName, string classNameEnding, IEnumerable<IRule<(Type Outer, Type Nested)>> rules)
    {
        _ruleName = ruleName;
        _classNameEnding = classNameEnding;
        _rules = rules;
    }

    public string RuleName => _ruleName;

    //public IReadOnlyDictionary<string, List<string>> Evaluate(Architecture architecture)
    //{
    //    var classes = ArchRuleDefinition.Classes()
    //        .That()
    //        .HaveNameEndingWith(_classNameEnding)
    //        .GetObjects(architecture);

    //    Dictionary<string, List<string>> violations = [];

    //    foreach (var @class in classes)
    //    {
    //        // ArchUnitNET 'Class'에서 'System.Type' 얻기
    //        var systemType = GetSystemTypeFromArchitectureClass(@class);
    //        if (systemType == null)
    //        {
    //            violations[@class.FullName!] = ["System.Type not found"];
    //            continue;
    //        }

    //        List<string> missingRules = [];

    //        // Type에서 중첩 클래스 얻기
    //        var nestedTypes = systemType.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic);

    //        //foreach (var nestedClassRule in _rules)
    //        //{
    //        //    // 중첩 클래스 검증하기
    //        //    var valid = nestedTypes.Any(nested =>
    //        //        nested.Name == nestedClassRule.Name &&
    //        //        nestedClassRule.IsSatisfiedBy((systemType, nested)));

    //        //    if (!valid)
    //        //    {
    //        //        missingRules.Add($"Missing valid nested class '{nestedClassRule.Name}'");
    //        //    }
    //        //}

    //        foreach (var rule in _rules)
    //        {
    //            var valid = nestedTypes.Any(nested =>
    //                rule.IsSatisfiedBy((systemType, nested)));

    //            if (!valid)
    //            {
    //                violations[systemType.FullName!] ??= [];
    //                violations[systemType.FullName!].Add($"Missing valid nested class '{rule.Name}'");
    //            }
    //        }


    //        if (missingRules.Any())
    //        {
    //            violations[systemType.FullName!] = missingRules;
    //        }
    //    }

    //    return violations;
    //}

    public IReadOnlyDictionary<string, List<string>> Evaluate(Architecture architecture)
    {
        var classes = ArchRuleDefinition.Classes()
            .That()
            .HaveNameEndingWith(_classNameEnding)
            .GetObjects(architecture);

        Dictionary<string, List<string>> violations = [];

        foreach (var @class in classes)
        {
            var systemType = GetSystemTypeFromArchitectureClass(@class);
            if (systemType == null)
            {
                violations[@class.FullName!] = ["System.Type not found"];
                continue;
            }

            var nestedTypes = systemType.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var rule in _rules)
            {
                bool hasValid = nestedTypes.Any(nested =>
                    rule.IsSatisfiedBy((systemType, nested)));

                if (!hasValid)
                {
                    if (!violations.TryGetValue(systemType.FullName!, out var messages))
                    {
                        messages = [];
                        violations[systemType.FullName!] = messages;
                    }

                    messages.Add($"Missing valid nested class satisfying '{rule.Name}'");
                }
            }
        }

        return violations;
    }

    private static Type? GetSystemTypeFromArchitectureClass(Class @class)
    {
        var type = AppDomain.CurrentDomain
            .GetAssemblies()
            .Select(a => a.GetType(@class.FullName, false))
            .FirstOrDefault(t => t != null);

        return type;
    }
}

public interface IRule<T>
{
    bool IsSatisfiedBy(T item);
    string Name { get; }
}

// 중첩 클래스 규칙
public record NestedClassRule(
    string Name,
    Func<System.Type, System.Type, bool> Predicate)
    : IRule<(System.Type Outer, System.Type Nested)>
{
    public bool IsSatisfiedBy((System.Type Outer, System.Type Nested) item) =>
        Predicate(item.Outer, item.Nested);
}

// 중첩 클래스 규칙 집합
public class NestedClassRuleBuilder
{
    private readonly string _nestedClassName;
    private readonly List<Func<System.Type, System.Type, bool>> _predicates = [];

    public NestedClassRuleBuilder(string nestedClassName)
    {
        _nestedClassName = nestedClassName;
    }

    public NestedClassRuleBuilder MustBePublic()
    {
        _predicates.Add((_, nested) => nested.IsNestedPublic);
        return this;
    }

    public NestedClassRuleBuilder MustBeInternal()
    {
        _predicates.Add((_, nested) => !nested.IsNestedPublic);
        return this;
    }

    public NestedClassRuleBuilder MustBeSealed()
    {
        _predicates.Add((_, nested) => nested.IsSealed);
        return this;
    }

    public NestedClassRuleBuilder MustImplementsGenericInterface(System.Type type)
    {
        _predicates.Add((outer, nested) =>
            nested.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == type));     // nested : type<?>
        return this;
    }

    public NestedClassRuleBuilder MustImplementsGenericInterfaceWithOuterTypeArguments(System.Type type)
    {
        _predicates.Add((outer, nested) =>
            nested.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == type &&
                i.GenericTypeArguments[0] == outer));       // nested : type<outer>
        return this;
    }

    public NestedClassRuleBuilder MustImplementsInterface(System.Type type)
    {
        _predicates.Add((outer, nested) =>
            nested.GetInterfaces().Any(i =>
                !i.IsGenericType &&                         // nested : type

                // i.FullName은 어셈블리 세부 정보까지 포함되어 이써 i.Name으로 비교합니다.
                i.Name == type.Name));
        return this;
    }

    public NestedClassRule Build()
    {
        return new NestedClassRule(
            Name: _nestedClassName,
            Predicate: (outer, nested) => _predicates.All(predicate => predicate(outer, nested)));
    }
}

public class TestArch : ArchitectureTestBase
{
    [Fact]
    public void AllRules_ShouldBeSatisfied()
    {
        var rules = new List<IArchitectureRule>
        {
            //new SyntaxLevelRule<IType>(
            //    "Static method naming",
            //    ApplicationLayer,
            //    new HaveStaticMethodCondition<IType>("Create")),

            //new SyntaxLevelRule<IType>(
            //    "Private constructor check",
            //    DomainLayer,
            //    new HavePrivateParameterlessConstructorCondition<IType>()),

            new SyntaxLevelRule<IType>(
                "Static method naming",
                ApplicationLayer,
                ArchitectureUtilities.MustHaveStaticMethodStartingWith("Create")),

            new SyntaxLevelRule<IType>(
                "Private constructor check",
                DomainLayer,
                ArchitectureUtilities.MustHavePrivateParameterlessConstructor()),

            //new ReflectionLevelRule(
            //    "Nested Settings Validation",
            //    "Settings",
            //    new List<NestedClassRule>
            //    {
            //        new NestedClassRuleBuilder("Validator")
            //            .MustBeInternal()
            //            .MustImplementsGenericInterfaceWithOuterTypeArguments(typeof(IValidator<>))
            //            .Build()
            //    })

            new ReflectionLevelRule(
                "Nested Settings Validation",
                "Settings",
                new []
                {
                    new RuleBuilder<(System.Type Outer, System.Type Nested)>("Validator")
                        .Where(t => !t.Nested.IsNestedPublic, "Validator must be internal")
                        .Where(t => t
                            .Nested
                            .GetInterfaces()
                            .Any(i =>
                                i.IsGenericType &&
                                i.GetGenericTypeDefinition() == typeof(IValidator<>) &&
                                i.GenericTypeArguments[0] == t.Outer),
                            "Validator must implement IValidator<Outer>")
                        .Build()
                })

        };

        foreach (var rule in rules)
        {
            var result = rule.Evaluate(Architecture);

            if (result.Any())
            {
                var messages = result
                    .SelectMany(kvp => kvp.Value.Select(v => $"[{kvp.Key}] {v}"))
                    .ToList();

                Assert.True(false, $"{rule.RuleName} failed:\n{string.Join("\n", messages)}");
            }
        }
    }
}

// 공통 PredicateCondition<T>로 일반화
public sealed class PredicateCondition<T> : ICondition<T> where T : ICanBeAnalyzed
{
    private readonly Func<T, bool> _predicate;
    private readonly string _description;

    public PredicateCondition(Func<T, bool> predicate, string description)
    {
        _predicate = predicate;
        _description = description;
    }

    public string Description => _description;

    public IEnumerable<ConditionResult> Check(IEnumerable<T> objects, Architecture architecture)
    {
        foreach (var obj in objects)
        {
            bool result = _predicate(obj);

            yield return new ConditionResult(
                analyzedObject: obj,
                pass: result,
                failDescription: result ? null : _description);
        }
    }

    public bool CheckEmpty() => true;
}

// SyntaxConditionBuilder<T> 도입
public class SyntaxConditionBuilder<T> where T : ICanBeAnalyzed
{
    private readonly List<ICondition<T>> _conditions = [];

    public SyntaxConditionBuilder<T> MustSatisfy(Func<T, bool> predicate, string description)
    {
        _conditions.Add(new PredicateCondition<T>(predicate, description));
        return this;
    }

    public ICondition<T> Build()
    {
        return new CompositeCondition<T>(_conditions);
    }
}

public sealed class CompositeCondition<T> : ICondition<T> where T : ICanBeAnalyzed
{
    private readonly IEnumerable<ICondition<T>> _conditions;

    public CompositeCondition(IEnumerable<ICondition<T>> conditions)
    {
        _conditions = conditions;
    }

    public string Description => "One or more conditions failed";

    public IEnumerable<ConditionResult> Check(IEnumerable<T> objects, Architecture architecture)
    {
        foreach (var obj in objects)
        {
            foreach (var condition in _conditions)
            {
                foreach (var result in condition.Check([obj], architecture))
                {
                    if (!result.Pass)
                        yield return result;
                }
            }
        }
    }

    public bool CheckEmpty() => true;
}

// ArchitectureUtilities
public static class ArchitectureUtilities
{
    public static ICondition<IType> MustHaveStaticMethodStartingWith(string prefix)
    {
        return new SyntaxConditionBuilder<IType>()
            .MustSatisfy(
                @class =>
                {
                    if (@class is Class classObject)
                    {
                        return classObject
                            .GetMethodMembers()
                            .Any(m => m.IsStatic == true && m.Name.StartsWith(prefix));
                    }

                    return false;
                },
                $"must have at least one static method starting with '{prefix}'")
            .Build();
    }

    public static ICondition<IType> MustHavePrivateParameterlessConstructor()
    {
        return new SyntaxConditionBuilder<IType>()
            .MustSatisfy(
                @class =>
                {
                    if (@class is Class classObject)
                    {
                        return classObject
                            .GetConstructors()
                            .Any(c => c.Visibility == Visibility.Private && c.Parameters.Count() == 0);
                    }

                    return false;
                },
                "must have a private parameterless constructor")
            .Build();
    }
}


public sealed class PredicateRule<T> : IRule<T>
{
    private readonly Func<T, bool> _predicate;
    public string Name { get; }

    public PredicateRule(string name, Func<T, bool> predicate)
    {
        Name = name;
        _predicate = predicate;
    }

    public bool IsSatisfiedBy(T item) => _predicate(item);
}

public class RuleBuilder<T>
{
    private readonly string _name;
    private readonly List<(Func<T, bool> Predicate, string Description)> _conditions = [];

    public RuleBuilder(string name)
    {
        _name = name;
    }

    public RuleBuilder<T> Where(Func<T, bool> predicate, string description)
    {
        _conditions.Add((predicate, description));
        return this;
    }

    public IRule<T> Build()
    {
        return new PredicateRule<T>(_name, item =>
            _conditions.All(cond => cond.Predicate(item)));
    }

    public IEnumerable<string> GetViolationDescriptions(T item)
    {
        return _conditions
            .Where(cond => !cond.Predicate(item))
            .Select(cond => cond.Description);
    }
}
