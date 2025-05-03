using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace FunctionalDdd.SourceGenerator.Generators.EntityIdGenerator;

[Generator(LanguageNames.CSharp)]
public sealed class EntityIdGenerator() 
    : IncrementalGeneratorBase<EntityIdToGenerateEntry>(
        RegisterSourceProvider,
        Generate)
{
    private static IncrementalValuesProvider<EntityIdToGenerateEntry> RegisterSourceProvider(IncrementalGeneratorInitializationContext context)
    {
        throw new NotImplementedException();
        //context.RegisterPostInitializationOutput(ctx => ctx.AddSource
        //(
        //    GenerateEntityIdAttributeFileName,
        //    SourceText.From(GenerateEntityIdAttribute, Encoding.UTF8))
        //);

        //return context
        //    .SyntaxProvider
        //    .ForAttributeWithMetadataName(GenerateEntityIdAttributeMetadataName, Selectors.IsClass, MapToEntityIdToGenerate)
        //    .Where(x => x != EntityIdToGenerateEntry.None);
    }

    private static void Generate(SourceProductionContext context, ImmutableArray<EntityIdToGenerateEntry> entityIdToGenerateEntries)
    {
        throw new NotImplementedException();
        //foreach (var entityIdToGenerateEntry in entityIdToGenerateEntries)
        //{
        //    StringBuilder sb = new();
        //    var result = entityIdToGenerateEntry.Generate(sb);
        //    context.AddSource(entityIdToGenerateEntry.Name + ".g.cs", SourceText.From(result, Encoding.UTF8));
        //}
    }
}
