using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using System.Reflection;

namespace GymManagement.Tests.Unit.ArchitectureTests;

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

    protected record NestedClassRule(string Name, Func<System.Type, System.Type, bool> Predicate);

    private static System.Type? GetSystemTypeFromArchitectureClass(Class @class)
    {
        // AssemblyQualifiedName으로 정확한 System.Type 찾기
        var type = AppDomain.CurrentDomain
            .GetAssemblies()
            .Select(a => a.GetType(@class.FullName, false))
            .FirstOrDefault(t => t != null);

        return type;
    }

    protected static Dictionary<string, List<string>> CheckNestedClassRules(
        string classNameEndingWith,
        IEnumerable<NestedClassRule> nestedClassRules)
    {
        var classes = ArchRuleDefinition.Classes()
            .That()
            .HaveNameEndingWith(classNameEndingWith);

        Dictionary<string, List<string>> violations = [];

        foreach (Class @class in classes.GetObjects(Architecture))
        {
            // Class에서 System.Type 얻기
            var systemType = GetSystemTypeFromArchitectureClass(@class);
            if (systemType == null)
            {
                violations[@class.FullName!] = ["System.Type not found"];
                continue;
            }

            List<string> missing = [];
            foreach (var nestedClassRule in nestedClassRules)
            {
                // Type에서 중첩 클래스 규칙 검증하기
                var nestedType = systemType
                    .GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public)
                    .FirstOrDefault(n =>
                        n.Name == nestedClassRule.Name &&
                        nestedClassRule.Predicate(systemType, n));

                // 중첩 클래스 규칙을 준수하지 않는다면
                if (nestedType is null)
                {
                    missing.Add($"Missing valid nested class '{nestedClassRule.Name}'");
                }
            }

            if (missing.Any())
            {
                violations[systemType.FullName!] = missing;
            }
        }

        return violations;
    }
}
