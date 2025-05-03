using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;

namespace FunctionalDdd.SourceGenerator;

public abstract class IncrementalGeneratorBase<TValue>
(
    Func<IncrementalGeneratorInitializationContext, IncrementalValuesProvider<TValue>> registerSourceProvider,
    Action<SourceProductionContext, ImmutableArray<TValue>> generate,
    Action<IncrementalGeneratorPostInitializationContext>? registerPostInitializationSourceOutput = null,
    bool AttachDebugger = false
) : IIncrementalGenerator
{
    protected const string ClassEntityName = "class";
    protected const string MethodEntityName = "method";
    protected const string PropertyEntityName = "property";
    protected const string FieldEntityName = "field";

    private readonly bool _attachDebugger = AttachDebugger;
    private readonly Func<IncrementalGeneratorInitializationContext, IncrementalValuesProvider<TValue>> _registerSourceProvider = registerSourceProvider;
    private readonly Action<SourceProductionContext, ImmutableArray<TValue>> _generate = generate;
    private readonly Action<IncrementalGeneratorPostInitializationContext>? _registerPostInitializationSourceOutput = registerPostInitializationSourceOutput;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        if (_attachDebugger && Debugger.IsAttached is false)
        {
            Debugger.Launch();
        }
    }
}
