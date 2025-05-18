using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using FluentValidation;
using System.Linq;
using System.Reflection;

namespace GymManagement.Tests.Unit.ArchitectureTests;



//public record StaticMethodRule(
//    string Name, 
//    Func<System.Type, bool> Predicate) 
//    : IRule<System.Type>
//{
//    public bool IsSatisfiedBy(System.Type type) => 
//        Predicate(type);
//}

//public static class StaticMethodPredicates
//{
//    public static Func<System.Type, bool> HasStaticMethod(string methodName, System.Type returnType, params System.Type[] parameters)
//    {
//        return type =>
//        {
//            return type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
//                       .Any(m =>
//                           m.Name == methodName &&
//                           m.ReturnType == returnType &&
//                           m.GetParameters().Select(p => p.ParameterType).SequenceEqual(parameters));
//        };
//    }

//    public static Func<System.Type, bool> HasStaticMethod(string methodName)
//    {
//        return type =>
//            type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
//                .Any(m => m.Name == methodName);
//    }
//}





public abstract class ArchitectureTestBase
{
    // 테스트 대상 어셈블리
    protected static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            Adapters.Infrastructure.AssemblyReference.Assembly,
            Adapters.Persistence.AssemblyReference.Assembly,
            Adapters.Presentation.AssemblyReference.Assembly,
            Application.AssemblyReference.Assembly,
            Domain.AssemblyReference.Assembly)
        .Build();

    protected static readonly IObjectProvider<IType> AdapterInfrastructureLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Adapters.Infrastructure.AssemblyReference.Assembly)
        .As("Adapters.Infrastructure");

    protected static readonly IObjectProvider<IType> AdapterPersistenceLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Adapters.Persistence.AssemblyReference.Assembly)
        .As("Adapters.Persistence");

    protected static readonly IObjectProvider<IType> AdapterPresentationLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Adapters.Presentation.AssemblyReference.Assembly)
        .As("Adapters.Presentation");

    protected static readonly IObjectProvider<IType> ApplicationLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Application.AssemblyReference.Assembly)
        .As("Application");

    protected static readonly IObjectProvider<IType> DomainLayer = ArchRuleDefinition
        .Types()
        .That()
        .ResideInAssembly(Domain.AssemblyReference.Assembly)
        .As("Domain");

    //private static System.Type? GetSystemTypeFromArchitectureClass(Class @class)
    //{
    //    // AssemblyQualifiedName으로 정확한 System.Type 찾기
    //    var type = AppDomain.CurrentDomain
    //        .GetAssemblies()
    //        .Select(a => a.GetType(@class.FullName, false))
    //        .FirstOrDefault(t => t != null);

    //    return type;
    //}

    //protected static Dictionary<string, List<string>> CheckNestedClassRules(
    //    string classNameEndingWith,
    //    IEnumerable<NestedClassRule> nestedClassRules)
    //{
    //    var classes = ArchRuleDefinition.Classes()
    //        .That()
    //        .HaveNameEndingWith(classNameEndingWith)
    //        .GetObjects(Architecture);

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

    //        foreach (var nestedClassRule in nestedClassRules)
    //        {
    //            // 중첩 클래스 검증하기
    //            var valid = nestedTypes.Any(nested =>
    //                nested.Name == nestedClassRule.Name &&
    //                nestedClassRule.IsSatisfiedBy((systemType, nested)));

    //            if (!valid)
    //            {
    //                missingRules.Add($"Missing valid nested class '{nestedClassRule.Name}'");
    //            }
    //        }

    //        if (missingRules.Any())
    //        {
    //            violations[systemType.FullName!] = missingRules;
    //        }
    //    }

    //    return violations;
    //}
}
