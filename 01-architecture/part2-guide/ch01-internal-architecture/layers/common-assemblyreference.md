# 공통 AssemblyReference

```cs
using System.Reflection;

namespace GymManagement.Adapters.Persistence;           // TODO: 네임스페이스만 조정합니다.

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
```