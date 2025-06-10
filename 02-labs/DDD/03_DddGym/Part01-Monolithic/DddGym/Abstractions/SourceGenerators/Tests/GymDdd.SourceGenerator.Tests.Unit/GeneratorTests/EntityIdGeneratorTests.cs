using GymDdd.SourceGenerator.Generators.EntityIdGenerator;
using GymDdd.SourceGenerator.Tests.Unit.Abstractions;
using static GymDdd.SourceGenerator.Tests.Unit.Abstractions.Constants.Constants;

namespace GymDdd.SourceGenerator.Tests.Unit.GeneratorTests;

[Trait(nameof(UnitTest), UnitTest.SourceGenerator)]
public sealed class EntityIdGeneratorTests
{
    private readonly EntityIdGenerator _sut;

    public EntityIdGeneratorTests()
    {
        _sut = new EntityIdGenerator();
    }

    [Fact]
    public Task EntityIdGenerator_ShouldGenerate_EntityIdAttribute()
    {
        // Assert
        string input = string.Empty;

        // Act
        string? actual = _sut.Generate(input);

        // Assert
        return Verify(actual);
    }

    [Fact]
    public Task EntityIdGenerator_ShouldGenerateEntityId()
    {
        // Arrange
        string input = """
        using System;

        namespace MyNamespace;

        [GenerateEntityId]
        public sealed class Product;
        """;

        // Act
        string? actual = _sut.Generate(input);

        // Assert
        return Verify(actual);
        //actual.ShouldBe(output);
    }
}
