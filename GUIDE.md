- 레이어 솔루션 구성(폴더 구성)
  - 프로젝트 참조
  - 패키지
- 솔루션 빌드 설정
  - Directory.Build.props(전체, 테스트)
  - Directory.Packages.props(구분)
  - global.json
  - nuget.config
  - .editorconfig
  - .gitignore
  - README.md
- 어셈블리 AssemblyReference 구현
- 아키텍처 단위 테스트 구현
  - 폴더 구성
    - Abstractions
    - ArchitectureTests
  - 레이어 의존성 테스트
  - 어셈블리 AssemblyReference 테스트
- 레이어 의존성 주입
  ```
  Abstractions/
    Registration/
      {레이어}Registration.cs
  ```
- 관찰 가능션 옵션
  ```
  - appsettings.json
  -> {Featrue}Options
  -> {Feature}OptionsSetup : IConfigureOptions<{Feature}Options>
  -> {Feature}OptionsValidator : IValidateOptions<{Feature}Options>
  ```
  - 옵션 데이터: XyzOptions
  - 옵션 데이터 읽기: IConfigureOptions
  - 옵션 유효성 검사: IValidateOptions
