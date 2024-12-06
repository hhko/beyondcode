using System.Reflection;

namespace Crop.Hello.Api.Adapters.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
