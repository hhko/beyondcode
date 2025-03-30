using FluentValidation;
using System.Reflection;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed partial class NamingConventionsTests : ArchitectureTestBase
{

    // 네이밍컨벤션 Options 규칙
    // 
    // - 옵션 유효성 검사 클래스
    //  - AbstractValidator<T> 을 상속받는 모든 클래스는 internal sealed 이어야 한다.
    //  - 클래스 이름은 반드시 Validator 접미사를 가져야 한다.
    //
    // - 옵션 클래스 규칙
    //   - AbstractValidator<T> 의 제네릭 타입 T는 Options 접미사를 가져야 한다.
    //   - T 클래스는 반드시 public const string SectionName 변수를 포함해야 한다.
    //   - SectionName 값은 클래스 이름에서 Options 접미사를 제외한 값과 같아야 한다.

    private const string SectionNameField = "SectionName";
    private const string OptionsSuffix = "Options";
    private const string ValidatorSuffix = "Validator";


    [Fact]
    public void All_AbstractValidator_Classes_Should_Be_Internal_Sealed_And_Have_Validator_Suffix()
    {
        Types()
        //var result = Types.InAssembly(Assembly.GetExecutingAssembly())
        //    .That()
        //    .Inherit(typeof(AbstractValidator<>))
        //    .Should()
        //    .BeSealed()
        //    .And()
        //    .BeInternal()
        //    .And()
        //    //.HaveNameEndingWith("Validator")
        //    .HaveNameEndingWith(ValidatorSuffix)
        //    .GetResult();

        //Assert.True(result.IsSuccessful, "All classes inheriting AbstractValidator<T> must be internal sealed and end with 'Validator'.");
    }

    [Fact]
    public void Generic_Type_Of_AbstractValidator_Should_Have_SectionName_Constant()
    {
        //var validatorTypes = Types.InAssembly(Assembly.GetExecutingAssembly())
        //    .That()
        //    .Inherit(typeof(AbstractValidator<>))
        //    .GetTypes();

        //foreach (var validatorType in validatorTypes)
        //{
        //    var baseType = validatorType.BaseType;
        //    if (baseType == null || !baseType.IsGenericType) continue;

        //    var genericArgument = baseType.GetGenericArguments().FirstOrDefault();
        //    Assert.NotNull(genericArgument);

        //    var sectionNameField = genericArgument.GetField(SectionNameField, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        //    Assert.NotNull(sectionNameField);
        //    Assert.True(sectionNameField.IsLiteral && !sectionNameField.IsInitOnly, $"{genericArgument.Name} must have a public const string SectionName.");
        //}
    }

    [Fact]
    public void Generic_Type_Of_AbstractValidator_Should_Have_SectionName_Constant_With_Correct_Value()
    {
        //var validatorTypes = Types.InAssembly(Assembly.GetExecutingAssembly())
        //    .That()
        //    .Inherit(typeof(AbstractValidator<>))
        //    .GetTypes();

        //foreach (var validatorType in validatorTypes)
        //{
        //    var baseType = validatorType.BaseType;
        //    Assert.NotNull(baseType);
        //    Assert.True(baseType.IsGenericType, $"{validatorType.Name} should inherit from AbstractValidator<T> with a generic type.");

        //    var genericArgument = baseType.GetGenericArguments().FirstOrDefault();
        //    Assert.NotNull(genericArgument);

        //    // 1. 제네릭 타입은 반드시 Options 접미사를 가져야 함
        //    Assert.EndsWith(OptionsSuffix, genericArgument.Name, $"{genericArgument.Name} must end with '{OptionsSuffix}'.");

        //    // 2. SectionName 필드가 존재해야 함
        //    var sectionNameField = genericArgument.GetField(SectionNameField, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        //    Assert.NotNull(sectionNameField);
        //    Assert.True(sectionNameField.IsLiteral && !sectionNameField.IsInitOnly, $"{genericArgument.Name} must have a public const string SectionName.");

        //    // 3. SectionName 값이 클래스 이름에서 Options 접미사를 제외한 값과 일치해야 함
        //    var expectedSectionName = genericArgument.Name.Replace(OptionsSuffix, "");
        //    var actualSectionName = sectionNameField.GetValue(null)?.ToString();

        //    Assert.Equal(expectedSectionName, actualSectionName,
        //        $"{genericArgument.Name}.SectionName must be '{expectedSectionName}' but was '{actualSectionName}'.");
        //}
    }

    // ----------------------

    [Fact]
    public void Options_Classes_Should_Have_SectionName_Constant_With_Correct_Value_And_Have_Corresponding_Validator()
    {
        //var optionsTypes = Types.InAssembly(Assembly.GetExecutingAssembly())
        //    .That()
        //    .HaveNameEndingWith(OptionsSuffix)
        //    .GetTypes();

        //var allTypes = Assembly.GetExecutingAssembly().GetTypes();

        //foreach (var optionsType in optionsTypes)
        //{
        //    // 1️⃣ SectionName 필드가 존재해야 함
        //    var sectionNameField = optionsType.GetField(SectionNameField, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        //    Assert.NotNull(sectionNameField);
        //    Assert.True(sectionNameField.IsLiteral && !sectionNameField.IsInitOnly, $"{optionsType.Name} must have a public const string SectionName.");

        //    // 2️⃣ SectionName 값이 올바른지 확인 (클래스 이름에서 "Options" 접미사 제거한 값과 같아야 함)
        //    var expectedSectionName = optionsType.Name.Replace(OptionsSuffix, "");
        //    var actualSectionName = sectionNameField.GetValue(null)?.ToString();
        //    Assert.Equal(expectedSectionName, actualSectionName,
        //        $"{optionsType.Name}.SectionName must be '{expectedSectionName}' but was '{actualSectionName}'.");

        //    // 3️⃣ 대응되는 Validator 클래스가 존재해야 함
        //    var expectedValidatorName = expectedSectionName + ValidatorSuffix;
        //    var validatorType = allTypes.FirstOrDefault(t =>
        //        t.Name == expectedValidatorName &&
        //        t.BaseType != null &&
        //        t.BaseType.IsGenericType &&
        //        t.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>) &&
        //        t.BaseType.GetGenericArguments().FirstOrDefault() == optionsType);

        //    Assert.NotNull(validatorType);
        //}
    }
}
