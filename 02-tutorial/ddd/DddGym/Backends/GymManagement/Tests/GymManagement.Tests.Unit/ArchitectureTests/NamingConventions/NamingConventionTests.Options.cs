using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using ArchUnitNET.Fluent;
using FluentValidation;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed partial class NamingConventionTests_Options : ArchitectureTestBase
{
    // Options 네이밍컨벤션
    // 
    // - 옵션 클래스 규칙
    //   - AbstractValidator<T> 의 제네릭 타입 T는 Options 접미사를 가져야 한다.
    //   - T 클래스는 반드시 public const string SectionName 변수를 포함해야 한다.
    //   - SectionName 값은 클래스 이름에서 Options 접미사를 제외한 값과 같아야 한다.

    [Fact]
    public void OptionsClasses_ShouldBe_Sealed_And_Have_SectionName()
    {
        // Arrange
        var optionsValidatorClasses = ArchRuleDefinition
            .Classes()
            .That().ImplementInterface(typeof(IValidator<>))
            .And().HaveNameEndingWith("OptionsValidator")
            .GetObjects(Architecture);

        foreach (var optionsValidatorClass in optionsValidatorClasses)
        {
            // Act
            Class? optionsClass = GetCorrespondingOptionsClass(optionsValidatorClass);

            // Assert
            //  - public sealed 클래스
            //  - public const string SectionName
            //    - TODO: 값 예. XyzOption -> Xyz
            optionsClass
                .ShouldNotBeNull($"Corresponding Options class for '{optionsValidatorClass.FullName}' not found.");
            optionsClass
                .Visibility.ShouldBe(Visibility.Public, $"Options class '{optionsClass.FullName}' should be public.");
            optionsClass
                .IsSealed!.Value.ShouldBeTrue($"Options class '{optionsClass.FullName}' should be sealed.");
            optionsClass
                .GetFieldMembers()
                .ShouldContain(field =>
                    field.Name == "SectionName" &&
                        field.Visibility == Visibility.Public &&
                        field.IsStatic.Value &&
                        field.Type.FullName == "System.String",
                    $"Options class '{optionsClass.FullName}' should have a public const string SectionName."
            );
        }
    }

    private static Class? GetCorrespondingOptionsClass(Class optionsValidatorClass)
    {
        var optionClassName = optionsValidatorClass.Name[..optionsValidatorClass.Name.LastIndexOf("Validator")];
        return ArchRuleDefinition
            .Classes()
            .That().HaveName(optionClassName)
            .GetObjects(Architecture)
            .FirstOrDefault();
    }

    /*
    [Fact]
    public void Generic_Type_Of_AbstractValidator_Should_Have_SectionName_Constant()
    {
        // Arrange
        IEnumerable<Class> optionsClasses = ArchRuleDefinition
            .Classes()
            .That()
            .HaveNameEndingWith("Options")
            .GetObjects(Architecture);

        IEnumerable<Class> optionsValidatorClasses = ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IValidator<>))
            .And()
            .HaveNameEndingWith("OptionsValidator")
            .GetObjects(Architecture);

        // Act & Assert
        foreach (Class optionsValidatorClass in optionsValidatorClasses)
        {
            // Act: 대응되는 Options 클래스 찾기
            string className = optionsValidatorClass.Name[..optionsValidatorClass.Name.LastIndexOf("Validator")];
            Class? optionsClass = optionsClasses.FirstOrDefault(c => c.Name == className);

            // Assert: optionsClass가 sealed인지 확인
            if (optionsClass == null || !optionsClass.IsSealed.HasValue)
            { 
              throw new Exception($"Class '{className}' not found in architecture.");
            }

            // Assert: optionsClass에 public const string SectionName이 있는지 확인
            bool hasSectionName = optionsClass.GetFieldMembers().Any(f =>
                f.Name == "SectionName" &&
                f.Visibility == Visibility.Public &&
                f.Type.FullName == "System.String"
            );

            if (!hasSectionName)
            {
                throw new Exception($"Class '{optionsClass.FullName}' must have a public const string SectionName.");
            }
        }


        //var abstractValidatorDerivedClasses = ArchRuleDefinition.Types()
        //        .That().AreAssignableTo(typeof(AbstractValidator<>))
        //        .GetObjects(Architecture);

        //foreach (var x in abstractValidatorDerivedClasses)
        //{

        //}


        //type.Generic
        ////var sut = ArchRuleDefinition
        ////    .Classes()
        ////    .That()
        ////    .ImplementInterface(typeof(IValidator<>));

        //if (!sut.GetObjects(Architecture).Any())
        //    return;

        //var validatorDerivedClasses  = sut.GetObjects(Architecture);
        //foreach (var validatorDerivedClass in validatorDerivedClasses)
        //{
        //    var x = validatorDerivedClass.BaseClass;

        //    foreach (GenericParameter genericParameter in x.GenericParameters)
        //    {
        //        foreach (IType type in genericParameter.TypeConstraints)
        //        {
        //            //type.
        //            //type.GetField(SectionNameField, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        //        }
        //    }
        //}

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
    */

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
