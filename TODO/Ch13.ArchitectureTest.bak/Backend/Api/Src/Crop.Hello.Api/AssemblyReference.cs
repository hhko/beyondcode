using System.Reflection;

namespace Crop.Hello.Api;

// TODO: 어셈블리에 AssemblyReference이 존재 테스트
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
