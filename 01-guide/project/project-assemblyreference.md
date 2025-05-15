# 어셈블리 정의

## 개요
- 모든 레이어별 프로젝트에 AssemblyReference 정적 클래스를 생성하여, 해당 레이어의 어셈블리를 명시적으로 참조할 수 있도록 합니다.
- 어셈블리를 레이어별로 체계적으로 관리할 수 있어, 아키텍처 테스트와 같이 어셈블리를 레이어 이름 단위로 명확하게 지정해야 하는 경우에도 효과적으로 활용할 수 있습니다.

![](./project-assemblyreference..png)

<br/>

## 지침
- 모든 레이어별 프로젝트는 루트 경로에 `AssemblyReference` 정적 클래스를 생성합니다.
- 클래스명은 반드시 `AssemblyReference`로 고정하며, 정적 속성 `Assembly`를 통해 해당 프로젝트의 어셈블리를 반환합니다.
- 네임스페이스는 각 프로젝트의 기본 네임스페이스를 따르되, 클래스 정의는 동일한 형식을 유지합니다.
- 이 클래스를 통해 내/외부에서 해당 프로젝트의 어셈블리를 명시적으로 참조합니다.

<br/>

## AssemblyReference 코드 예제
- 아래 코드는 Application 레이어에 적용한 사례입니다.
- 다른 레이어에서도 네임스페이스만 다를 뿐, 동일한 방식으로 구현합니다.

```cs
using System.Reflection;

// Application 레이어 프로젝트
namespace GymManagement.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
```

<br/>

## AssemblyReference 활용 예제
- AssemblyReference 정적 클래스를 어셈블리 단위로 구현함으로써, 아래와 같이 레이어별 어셈블리를 명시적으로 참조할 수 있습니다

```cs
public abstract class ArchitectureTestBase
{
    // 테스트 대상 어셈블리
    protected static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            // Adapter 레이어
            Adapters.Infrastructure.AssemblyReference.Assembly,
            Adapters.Persistence.AssemblyReference.Assembly,
            Adapters.Presentation.AssemblyReference.Assembly,

            // Application 레이어
            Application.AssemblyReference.Assembly,

            // Domain 레이어
            Domain.AssemblyReference.Assembly)
        .Build();
```
