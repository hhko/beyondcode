---
outline: deep
---

# 공통 AssemblyReference

```cs
using System.Reflection;

// TODO: 네임스페이스만 조정합니다.
namespace GymManagement.Adapters.Persistence;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
```