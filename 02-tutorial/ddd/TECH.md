```
dotnet new install .
dotnet new ddd-arch -o {솔루션 이름}
dotnet new uninstall {.template.config 폴더가 있는 전체 경로}
```

```json
{
  "sourceName": "CommandName",      // --name 값
                                    // "CommandName"을 "값"으로 변경합니다.
  "symbols": {
    "slnName": {                    // --slnName 값
      "type": "parameter",
      "replaces": "SolutionName",   // "SolutionName"을 "값"으로 변경합니다.
```

1. 순서 의미 有
   - 위    : 기술적(무한)으로 더 중요한 것
   - 아래  : 비즈니스적(유한)으로 더 중요한 것
2. 목표
   - 주 목표   : 유한(N개)
   - 부수 목표 : 무한 -> 유한(1개)
     특정 폴더(유한, 1개: Abstractions) 자식(무한)으로 만든다.
     Ex.
        Abstractions          // 부수 목표: 위, 구체적인 구현과 분리되어 여러 곳에서 사용할 수 있는(추상 클래스 같은 개념)
        AggregateRoots        // 주 목표: 아래
3. 용어
  부수: 주가 되는 것에 붙어 따르는 것
```