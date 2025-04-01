---
outline: deep
---

# global.json

## 개요
>  .NET CLI 명령을 실행할 때 어떤 .NET SDK 버전을 사용할지 지정합니다.

## 명령
### 생성 명령
```shell
dotnet new global.json --sdk-version 8.0.102 --roll-forward latestPatch
```
- latestFeature
- latestPatch

```json
{
  "sdk": {
    "version": "8.0.102",
    "rollForward": "latestPatch"
  }
}
```
- 8.0.102 이전의 모든 SDK 버전을 허용하지 않으며 8.0.102 이상 `8.0.1xx` 버전(예: 8.0.103 또는 8.0.199)을 허용합니다.
- `x.y.znn`
  - `x`는 주 버전입니다.
  - `y`는 부 버전입니다.
  - `z`는 기능 밴드입니다.
  - `nn`은 패치 버전입니다.

### 확인 명령
```shell
# 사용 가능한 모든 .NET SDK 버전 확인
dotnet --list-sdks

# global.json이 적용된 .NET SDK 버전 확인
dotnet --version

# global.json이 적용된 .NET SDK 버전 정보
dotnet --info
```
