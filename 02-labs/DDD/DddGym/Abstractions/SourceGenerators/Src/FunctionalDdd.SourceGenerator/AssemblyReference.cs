using System.Reflection;

namespace FunctionalDdd.SourceGenerator;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}