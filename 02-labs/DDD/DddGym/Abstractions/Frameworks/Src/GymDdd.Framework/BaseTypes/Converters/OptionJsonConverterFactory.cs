using System.Text.Json;
using System.Text.Json.Serialization;

namespace GymDdd.Framework.BaseTypes.Converters;

// https://github.com/louthy/language-ext/discussions/1132

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class OptionAsNullableJsonConverterAttribute : JsonConverterAttribute
{
    public override JsonConverter? CreateConverter(Type typeToConvert) =>
        Activator.CreateInstance
        (
            typeof(OptionJsonConverterFactory.OptionAsNullableJsonConverter<>)
                .MakeGenericType
                (
                    typeToConvert.IsGenericType &&
                    typeToConvert.GetGenericTypeDefinition() == typeof(Option<>)
                        ? typeToConvert.GetGenericArguments().HeadAndTail().Head()
                        : typeToConvert
                )
        ) as JsonConverter;
}

public sealed class OptionJsonConverterFactory : JsonConverterFactory
{
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        // var innerType = typeToConvert.GetSingleGenericTypeArgument();
        var innerType = typeToConvert.GetGenericArguments().Match(
            () => throw new Exception("no generic type argument"),
            x => x,
            (x, xs) => throw new Exception("more than one generic type argument")
        );

        return Activator
            .CreateInstance(
                typeof(OptionJsonConverter<>).MakeGenericType(innerType),
                options
            ) as JsonConverter;
    }

    public override bool CanConvert(Type typeToConvert) =>
        typeToConvert.IsGenericType &&
        typeToConvert.GetGenericTypeDefinition() == typeof(Option<>);

    public class OptionJsonConverter<T> : JsonConverter<Option<T>>
    {
        private readonly JsonConverter<T> _innerConverter;

        public OptionJsonConverter(JsonSerializerOptions options) =>
            _innerConverter = (JsonConverter<T>)options.GetConverter(typeof(T));

        public override Option<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return Option<T>.None;
            }
            else
            {
                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new JsonException($"expected {JsonTokenType.StartArray} but found {reader.TokenType}");
                }
                if (!reader.Read())
                {
                    throw new JsonException($"could not read element in array or {JsonTokenType.EndArray}");
                }
                if (reader.TokenType != JsonTokenType.EndArray)
                {
                    // non-empty-array
                    var value = _innerConverter.Read(ref reader, typeof(T), options);
                    if (!reader.Read())
                    {
                        throw new JsonException("could not read next token");
                    }
                    if (reader.TokenType != JsonTokenType.EndArray)
                    {
                        throw new JsonException($"expected token {JsonTokenType.EndArray} but found {reader.TokenType}");
                    }
                    return Option<T>.Some(value!);
                }
                else
                {
                    return Option<T>.None;
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, Option<T> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            value.IfSome(some => _innerConverter.Write(writer, some, options));
            writer.WriteEndArray();
        }
    }

    internal class OptionAsNullableJsonConverter<T> : JsonConverter<Option<T>>
    {
        public override Option<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            reader.TokenType == JsonTokenType.Null
                ? Option<T>.None
                : Option<T>.Some((options.GetConverter(typeof(T?)) as JsonConverter<T>
                    ?? throw new JsonException($"Could not get converter for {typeof(T)}")).Read(ref reader, typeof(T), options)!);

        public override void Write(Utf8JsonWriter writer, Option<T> value, JsonSerializerOptions options)
        {
            if (value.Case is T some)
            {
                ((JsonConverter<T>)options.GetConverter(typeof(T))).Write(writer, some, options);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
