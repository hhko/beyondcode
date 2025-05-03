using FunctionalDdd.SourceGenerator.Generators.EntityIdGenerator;
using FunctionalDdd.SourceGenerator.Tests.Unit.Abstractions;
using VerifyTests;
using VerifyXunit;

namespace FunctionalDdd.SourceGenerator.Tests.Unit.GeneratorTests;

public class EntityIdTests
{
    private readonly EntityIdGenerator _sut;

    public EntityIdTests()
    {
        _sut = new EntityIdGenerator();
    }

    [Fact]
    public Task GeneratesEnumExtensionsCorrectly()
    {
        var actual = _sut.Generate(string.Empty);

        return Verify(actual);
    }
}
