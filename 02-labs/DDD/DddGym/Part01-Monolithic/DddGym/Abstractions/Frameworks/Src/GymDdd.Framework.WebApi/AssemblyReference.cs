using System.Reflection;

namespace GymDdd.Framework.WebApi;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
