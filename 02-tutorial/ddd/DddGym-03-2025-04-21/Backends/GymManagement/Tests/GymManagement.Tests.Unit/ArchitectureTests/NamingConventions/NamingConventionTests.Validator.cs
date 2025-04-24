using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using FluentValidation;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed partial class NamingConventionTests_Validator : ArchitectureTestBase
{

    // Validator 네이밍컨벤션
    // 
    // - 옵션 유효성 검사 클래스
    //  - IValidator<T> 을 상속받는 모든 클래스는 internal sealed 이어야 한다.
    //  - 클래스 이름은 반드시 Validator 접미사를 가져야 한다.

    [Fact]
    public void ValidatorClasses_ShouldBe_InternalSealed_And_Have_ValidatorSuffix()
    {
        var sut = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValidator<>));

        if (!sut.GetObjects(Architecture).Any())
            return;

        sut.Should().BeInternal()
            .AndShould().BeSealed()
            .AndShould().HaveName(NamingConvention.ValidatorSuffix, true)
            .Check(Architecture);
    }
}
