# 공통 접근 제어자

## 개요
> 클래스는 `internal sealed`를 사용하여 외부 노출을 최소화합니다.
```cs
internal sealed class 클래스
{

}
```

- `internal`: 외부 어셈블리에서 접근할 수 없습니다(클래스 참조 불가).
- `sealed`: 상속할 수 없습니다(자식 클래스에서 확장 불가).


| Caller's location | `public` | `protected internal` | `protected` | `internal` | `private protected` | `private` | `file` |
|--|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
| Within the file | ✔️️ | ✔️ | ✔️ | ✔️ | ✔️ | ✔️ | ✔️ |
| Within the class | ✔️️ | ✔️ | ✔️ | ✔️ | ✔️ | ✔️ | ❌ |
| Derived class (same assembly) | ✔️ | ✔️ | ✔️ | ✔️ | ✔️ | ❌ | ❌ |
| Non-derived class (same assembly) | ✔️ | ✔️ | ❌ | ✔️ | ❌ | ❌ | ❌ |
| Derived class (different assembly) | ✔️ | ✔️ | ✔️ | ❌ | ❌ | ❌ | ❌ |
| Non-derived class (different assembly) | ✔️ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |