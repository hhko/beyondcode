# NuGet 패키지 버전 중앙 관리

## 개요
- 기존 .NET 프로젝트에서는 각 프로젝트 파일(.csproj)마다 패키지 참조(PackageReference)와 버전(Version)을 개별적으로 관리했습니다.
- 이 방식은 프로젝트 수가 적을 때는 큰 문제가 되지 않았지만, 솔루션 내에 프로젝트가 많아질수록 다음과 같은 문제가 발생했습니다:
  - 버전 관리의 복잡성 증가
    - 여러 프로젝트에서 동일한 패키지를 참조하면서 서로 다른 버전을 사용하는 경우가 발생하여, 패키지 충돌이나 런타임 오류가 생길 수 있었습니다.
  - 일관성 유지의 어려움
    - 모든 프로젝트에서 패키지 버전을 일관되게 유지하려면, 각 프로젝트 파일을 수작업으로 수정해야 했습니다.
  - 유지보수 비용 증가
    - 패키지 버전을 업데이트할 때 모든 프로젝트 파일을 수정해야 하므로, 유지보수 작업이 비효율적이었습니다.
- `upgrade-assistant` 도구를 이용하여 `Directory.Package.props` 기반 패키지 관리로 업그레이드합니다.

<br/>

## upgrade-assistant 도구
```shell
# 도구 설치
dotnet tool install -g upgrade-assistant

# 도구 확인
dotnet tool list -g
  패키지 ID             버전            명령
  ------------------------------------------------------
  upgrade-assistant     0.5.820        upgrade-assistant

# 패키지 버전 중앙 관리: Directory.Package.props
upgrade-assistant upgrade
```
![](./upgrade-assistant.png)

## Directory.Package.props 적용 전/후
![](./upgrade-assistant-result.png)

- `upgrade-assistant` 도구를 이용하여 모든 프로젝트 파일의 `PackageReference`에서 `Version` 속성이 제거되고, 솔루션 루트에 위치한 `Directory.Package.props` 파일에 패키지 버전 정보가 명시됩니다.
- 이후 `Directory.Package.props` 파일에서 패키지 버전을 수정하면, 해당 패키지를 참조하는 모든 프로젝트에 일괄 적용되어 버전 관리를 보다 쉽게 할 수 있습니다.