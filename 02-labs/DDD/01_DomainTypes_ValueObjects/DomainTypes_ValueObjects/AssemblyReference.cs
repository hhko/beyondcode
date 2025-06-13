using System.Reflection;

namespace DomainTypes_ValueObjects;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
