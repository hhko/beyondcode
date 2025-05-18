namespace GymDdd.Tests.Architecture.ArchitectureTests;

public class TestArch : ArchitectureTestBase
{
    //[Fact]
    //public void AllRules_ShouldBeSatisfied()
    //{
    //    var valueObjectProvider = ArchRuleDefinition
    //        .Classes()
    //        .That()
    //        .ImplementInterface(typeof(IValueObject));

    //    List<IArchitectureRule> rules = [
    //        Must.HaveStaticMethod(valueObjectProvider, IValueObject.CreateMethodName),
    //        Must.HaveStaticMethod(valueObjectProvider, IValueObject.ValidateMethodName)
    //    ];

    //    foreach (var rule in rules)
    //    {
    //        var result = rule.Evaluate(Architecture);

    //        if (result.Any())
    //        {
    //            var messages = result
    //                .SelectMany(kvp => kvp.Value.Select(v => $"[{kvp.Key}] {v}"))
    //                .ToList();

    //            Assert.True(false, $"{rule.RuleName} failed:\n{string.Join("\n", messages)}");
    //        }
    //    }
    //var rules = new List<IArchitectureRule>
    //{
    //    //new SyntaxLevelRule<IType>(
    //    //    "Static method naming",
    //    //    ApplicationLayer,
    //    //    new HaveStaticMethodCondition<IType>("Create")),

    //    //new SyntaxLevelRule<IType>(
    //    //    "Private constructor check",
    //    //    DomainLayer,
    //    //    new HavePrivateParameterlessConstructorCondition<IType>()),

    //    //new SyntaxLevelRule<IType>(
    //    //    "Static method naming",
    //    //    ApplicationLayer,
    //    //    ArchitectureUtilities.MustHaveStaticMethodStartingWith("Create")),

    //    new SyntaxLevelRule<IType>(
    //        "ValueObject must have static Create() method",
    //        ArchRuleDefinition
    //            .Classes()
    //            .That()
    //            .ImplementInterface(typeof(IValueObject)),
    //        ArchitectureUtilities.MustHaveStaticMethodNamed(IValueObject.CreateMethodName)
    //    ),

    //    //new SyntaxLevelRule<IType>(
    //    //    "Private constructor check",
    //    //    DomainLayer,
    //    //    ArchitectureUtilities.MustHavePrivateParameterlessConstructor()),

    //    //new ReflectionLevelRule(
    //    //    "Nested Settings Validation",
    //    //    "Settings",
    //    //    new List<NestedClassRule>
    //    //    {
    //    //        new NestedClassRuleBuilder("Validator")
    //    //            .MustBeInternal()
    //    //            .MustImplementsGenericInterfaceWithOuterTypeArguments(typeof(IValidator<>))
    //    //            .Build()
    //    //    })

    //    //new ReflectionLevelRule(
    //    //    "Nested Settings Validation",
    //    //    "Settings",
    //    //    new []
    //    //    {
    //    //        new RuleBuilder<(System.Type Outer, System.Type Nested)>("Validator")
    //    //            .Where(t => !t.Nested.IsNestedPublic, "Validator must be internal")
    //    //            .Where(t => t
    //    //                .Nested
    //    //                .GetInterfaces()
    //    //                .Any(i =>
    //    //                    i.IsGenericType &&
    //    //                    i.GetGenericTypeDefinition() == typeof(IValidator<>) &&
    //    //                    i.GenericTypeArguments[0] == t.Outer),
    //    //                "Validator must implement IValidator<Outer>")
    //    //            .Build()
    //    //    })

    //};


    //}
}

//// ArchitectureUtilities
//public static class ArchitectureUtilities
//{
//    public static ICondition<IType> MustHaveStaticMethodStartingWith(string prefix)
//    {
//        return new SyntaxConditionBuilder<IType>()
//            .MustSatisfy(
//                @class =>
//                {
//                    if (@class is Class classObject)
//                    {
//                        return classObject
//                            .GetMethodMembers()
//                            .Any(m => m.IsStatic == true && m.Name.StartsWith(prefix));
//                    }

//                    return false;
//                },
//                $"must have at least one static method starting with '{prefix}'")
//            .Build();
//    }

//    public static ICondition<IType> MustHavePrivateParameterlessConstructor()
//    {
//        return new SyntaxConditionBuilder<IType>()
//            .MustSatisfy(
//                @class =>
//                {
//                    if (@class is Class classObject)
//                    {
//                        return classObject
//                            .GetConstructors()
//                            .Any(c => c.Visibility == Visibility.Private && c.Parameters.Count() == 0);
//                    }

//                    return false;
//                },
//                "must have a private parameterless constructor")
//            .Build();
//    }
//}