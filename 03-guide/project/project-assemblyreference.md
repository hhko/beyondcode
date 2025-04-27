# 레이어별 AssemblyReference 정의

## 개요
- 모든 레이어별 프로젝트에 AssemblyReference 정적 클래스를 생성하여, 해당 레이어의 어셈블리를 명시적으로 참조할 수 있도록 합니다.
- 이를 통해 아키텍처 테스트 시 어셈블리를 레이어 이름 단위로 명확하게 지정할 수 있습니다.

1[](./project-assemblyreference..png)

## AssemblyReference 코드
```cs
using System.Reflection;

namespace GymManagement.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
```

## AssemblyReference 활용 예제
- AssemblyReference 정적 클래스를 어셈블리 단위로 구현함으로써, 아래와 같이 레이어별 어셈블리를 명시적으로 참조할 수 있습니다

```cs
public abstract class ArchitectureTestBase
{
    // 테스트 대상 어셈블리
    protected static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            Adapters.Infrastructure.AssemblyReference.Assembly,
            Adapters.Persistence.AssemblyReference.Assembly,
            Adapters.Presentation.AssemblyReference.Assembly,
            Application.AssemblyReference.Assembly,
            Domain.AssemblyReference.Assembly)
        .Build();
```
