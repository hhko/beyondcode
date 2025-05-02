using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed partial class NamingConventionTests_Validator : ArchitectureTestBase
{

    // Validator 네이밍 컨벤션
    //
    //  internal sealed class {클래스}Validator : IValidator<T>
    //      - IValidator<T> 을 상속받는 모든 클래스는 internal sealed 이어야 한다.
    //      - 클래스 이름은 반드시 Validator 접미사를 가져야 한다.

    //[Fact]
    //public void ValidatorClasses_ShouldBe_InternalSealed_And_Have_ValidatorSuffix()
    //{
    //    ArchRuleDefinition
    //        .Classes()
    //        .That()
    //        .ImplementInterface(typeof(IValidator<>))
    //        .Should().BeInternal()
    //        .AndShould().BeSealed()
    //        .AndShould().HaveName(
    //            pattern: NamingConvention.ValidatorSuffix, 
    //            useRegularExpressions: true)
    //        .AndShould().BeNested()
    //        .WithoutRequiringPositiveResults()
    //        .Check(Architecture);
    //}
}
