# 솔루션 .NET SDK 버전

## 개요
- `global.json` 파일을 활용하여 .NET SDK 버전을 고정하고, SDK 버전 확인 및 관리 방법을 확인합니다.
- 여러 개발 환경에서 **일관된 SDK 버전** 사용을 보장하기 위해 필수적인 설정입니다.

<br/>

## 지침
- 솔루션 루트에 `global.json` 파일을 생성하여, 프로젝트 빌드 시 사용할 .NET SDK 버전을 명확히 지정합니다.
- `sdk.version`에는 기본으로 사용할 SDK 버전을 지정하고, `rollForward` 옵션을 통해 허용할 범위를 설정합니다.

<br/>

## 생성 명령어
- `global.json`을 사용하면 프로젝트에서 사용할 정확한 .NET SDK 버전을 지정할 수 있습니다.

```shell
dotnet new global.json --sdk-version 9.0.100 --roll-forward latestPatch
```

```json
{
  "sdk": {
    "version": "9.0.100",
    "rollForward": "latestPatch"
  }
}
```

- 9.0.100 이전의 모든 SDK 버전을 허용하지 않으며 9.0.100 이상 `9.0.1xx` 버전(예: 9.0.103 또는 9.0.199)을 허용합니다.
  - **`x.y.znn`**
    - `x`는 주 버전입니다.
    - `y`는 부 버전입니다.
    - `z`는 기능 밴드입니다.
    - `nn`은 패치 버전입니다.

<br/>

## 관리 명령어어
```shell
# 사용 가능한 모든 .NET SDK 버전 확인
dotnet --list-sdks

# global.json이 적용된 .NET SDK 버전 확인
dotnet --version

# global.json이 적용된 .NET SDK 버전 정보
dotnet --info
```
