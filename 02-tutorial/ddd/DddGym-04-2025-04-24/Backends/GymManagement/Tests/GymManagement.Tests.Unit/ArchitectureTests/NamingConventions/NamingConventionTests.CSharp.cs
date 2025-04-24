using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using System.Text.RegularExpressions;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed partial class NamingConventionTests_CSharp : ArchitectureTestBase
{
    // C# 네이밍컨벤션
    //
    // - 인터페이스
    //   - I로 시작한다.
    //
    // - 메서드
    //   - 대문자로 시작한다.

    [Fact]
    public void Interfaces_ShouldStartWith_I()
    {
        IEnumerable<Interface> sut = ArchRuleDefinition
            .Interfaces()
            .GetObjects(Architecture);

        if (!sut.Any())
            return;

        sut.ShouldAllBe(i => Regex.IsMatch(i.Name, "^I[A-Z].*"));
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
            .Check(Architecture);
    }
}
