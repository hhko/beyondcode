# 솔루션 .NET SDK 버전

```shell
dotnet new global.json --sdk-version 9.0.100 --roll-forward latestPatch
```

- 9.0.100 이전의 모든 SDK 버전을 허용하지 않으며 9.0.100 이상 `9.0.1xx` 버전(예: 9.0.103 또는 9.0.199)을 허용합니다.
  - **`x.y.znn`**
    - `x`는 주 버전입니다.
    - `y`는 부 버전입니다.
    - `z`는 기능 밴드입니다.
    - `nn`은 패치 버전입니다.

```json
{
  "sdk": {
    "version": "9.0.100",
    "rollForward": "latestPatch"
  }
}
```

<br/>

## 버전 확인
```shell
# 사용 가능한 모든 .NET SDK 버전 확인
dotnet --list-sdks

# global.json이 적용된 .NET SDK 버전 확인
dotnet --version

# global.json이 적용된 .NET SDK 버전 정보
dotnet --info
```
