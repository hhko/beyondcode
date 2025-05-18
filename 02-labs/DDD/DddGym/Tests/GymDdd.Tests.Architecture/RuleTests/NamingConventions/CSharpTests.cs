using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using System.Text.RegularExpressions;
using static GymDdd.Tests.Architecture.Abstractions.Constants.Constants;

namespace GymDdd.Tests.Architecture.RuleTests.NamingConventions;

// C# 규칙
//
// - 인터페이스: I로 시작한다.
// - 메서드: 대문자로 시작한다.

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed partial class CSharpTests : ArchitectureTestBase
{
    [Fact]
    public void Interfaces_ShouldStartWith_I()
    {
        ArchRuleDefinition
            .Interfaces()
            .GetObjects(AllArchitecture)
            .ShouldAllBe(i => Regex.IsMatch(i.Name, "^I[A-Z].*"));
    }

    [Fact]
    public void AllMethods_ShouldStartWith_CapitalLetter()
    {
        ArchRuleDefinition
            .MethodMembers()

            // 제외
            .That().DoNotHaveNameStartingWith(".ctor")      // 클래스 생성자
            .And().DoNotHaveNameStartingWith(".cctor")      // 정적 클래스 생성자
            .And().DoNotHaveNameStartingWith("get_")        // property
            .And().DoNotHaveNameStartingWith("set_")        // property
            .And().DoNotHaveNameStartingWith("op_")         // 연산자 오버로딩: 예. + op_Addition, == op_Equality, ...

            // 규칙
            .Should().HaveName(@"^[A-Z]", true)
            .WithoutRequiringPositiveResults()
            .Check(AllArchitecture);
    }
}
