# 공통 접근 제어자

```cs
internal sealed class 클래스
{

}
```
- `internal`: 외부 어셈블리에서 접근을 차단합니다(클래스 참조 불가).
- `sealed`: 자식 클래스에서 접근을 차단합니다(클래스 상속 불가).

| Caller's location | `public` | `protected internal` | `protected` | `internal` | `private protected` | `private` | `file` |
|--|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
| Within the file | ✔️️ | ✔️ | ✔️ | ✔️ | ✔️ | ✔️ | ✔️ |
| Within the class | ✔️️ | ✔️ | ✔️ | ✔️ | ✔️ | ✔️ | ❌ |
| Derived class (same assembly) | ✔️ | ✔️ | ✔️ | ✔️ | ✔️ | ❌ | ❌ |
| Non-derived class (same assembly) | ✔️ | ✔️ | ❌ | ✔️ | ❌ | ❌ | ❌ |
| Derived class (different assembly) | ✔️ | ✔️ | ✔️ | ❌ | ❌ | ❌ | ❌ |
| Non-derived class (different assembly) | ✔️ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |