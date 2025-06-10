using System.Reflection;

namespace GymDdd.Framework;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}