using System.Reflection;

namespace FunctionalDdd.Framework;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}