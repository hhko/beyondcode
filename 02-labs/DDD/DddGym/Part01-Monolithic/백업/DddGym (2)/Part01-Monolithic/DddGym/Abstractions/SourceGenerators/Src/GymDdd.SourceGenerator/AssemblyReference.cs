using System.Reflection;

namespace GymDdd.SourceGenerator;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}