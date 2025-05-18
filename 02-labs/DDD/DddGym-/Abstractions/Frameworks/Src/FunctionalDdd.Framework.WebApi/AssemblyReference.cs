using System.Reflection;

namespace FunctionalDdd.Framework.WebApi;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
