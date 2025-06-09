﻿namespace GymManagement.Adapters.Infrastructure.Abstractions.Options.Example;

public sealed class ExampleOptions
{
    public const string SectionName = "Example";

    public required int Retries { get; init; }
}
