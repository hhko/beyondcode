using GymDdd.Framework.BaseTypes.Converters;
using LanguageExt;
using System.Text.Json;
using System.Text.Json.Serialization;
using static GymDdd.Framework.Tests.Unit.Abstractions.Constants.Constants;

namespace GymDdd.Framework.Tests.Unit.BaseTypes.Converts;

[Trait(nameof(UnitTest), UnitTest.Framework)]
public class OptionJsonConverterFactoryTests
{
    public class SomeTypeWithOptions
    {
        public Option<int> MaybeIntPropArray { get; set; }

        [OptionAsNullableJsonConverter]
        public Option<int> MaybeIntPropNullable { get; set; }

        [OptionAsNullableJsonConverter]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]        // "기본값일 경우 무시" 하라는 의미
        public Option<int> MaybeIntPropNullableIgnore { get; set; }

    }

    [Fact]
    public void Json_To_T()
    {
        string json = """
        { 
            "MaybeIntPropArray": [4],
            "MaybeIntPropNullable": 5,
            "MaybeIntPropNullableIgnore": 6 
        }
        """;

        JsonSerializerOptions options = new(JsonSerializerDefaults.Web);
        options.Converters.Add(new OptionJsonConverterFactory());
        options.PropertyNameCaseInsensitive = true;

        // Act
        SomeTypeWithOptions actual = JsonSerializer.Deserialize<SomeTypeWithOptions>(json, options)!;

        // Assert
        actual.MaybeIntPropArray.ShouldBe(4);
        actual.MaybeIntPropNullable.ShouldBe(5);
        actual.MaybeIntPropNullableIgnore.ShouldBe(6);
    }

    [Fact]
    public void Json_To_T_When_Json_is_Empty()
    {
        // Arrange
        string json = """
            {
            }
            """;

        JsonSerializerOptions options = new(JsonSerializerDefaults.Web);
        options.Converters.Add(new OptionJsonConverterFactory());
        options.PropertyNameCaseInsensitive = true;

        // Act
        SomeTypeWithOptions actual = JsonSerializer.Deserialize<SomeTypeWithOptions>(json, options)!;

        // Assert
        actual.MaybeIntPropArray.ShouldBe(Option<int>.None);
        actual.MaybeIntPropNullable.ShouldBe(Option<int>.None);
        actual.MaybeIntPropNullableIgnore.ShouldBe(Option<int>.None);
    }


    [Fact]
    public void Json_From_T()
    {
        // Arrange
        SomeTypeWithOptions data = new()
        {
            MaybeIntPropArray = 1,
            MaybeIntPropNullable = 2,
            MaybeIntPropNullableIgnore = 3
        };

        JsonSerializerOptions options = new(JsonSerializerDefaults.General);
        options.Converters.Add(new OptionJsonConverterFactory());
        options.PropertyNameCaseInsensitive = true;

        // Act
        string actual = JsonSerializer.Serialize(data, options);

        // Assert
        actual.ShouldBe(@"{""MaybeIntPropArray"":[1],""MaybeIntPropNullable"":2,""MaybeIntPropNullableIgnore"":3}");
    }

    [Fact]
    public void Json_From_T_When_T_is_Empty()
    {
        // Arrange
        SomeTypeWithOptions data = new(); // all none
        JsonSerializerOptions options = new(JsonSerializerDefaults.General);
        options.Converters.Add(new OptionJsonConverterFactory());
        options.PropertyNameCaseInsensitive = true;

        // Act
        string actual = JsonSerializer.Serialize(data, options);

        // Assert
        actual.ShouldBe(@"{""MaybeIntPropArray"":[],""MaybeIntPropNullable"":0}");
    }
}
