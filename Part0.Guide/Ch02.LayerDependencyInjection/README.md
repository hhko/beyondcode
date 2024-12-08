# 레이어 의존성 주입(옵션 패턴)

## 목표
- [x] 옵션 패턴
- [x] 레이어 의존성 주입
- [ ] 관찰 가능성 콘솔 로그
- [ ] 통합 테스트(로그)

## TODO
- [ ] .

## 폴더 구성
![](./.images/DI.Structure.png)

- `Abstractions` 폴더
  - 레이어의 주요 목표와 직접 관련이 없는 모든 코드는 Abstractions 폴더에서 관리합니다.
    - 의존성 등록: Registrations
    - 옵션: Options

## 옵션 패턴

```
appsettings.json
  -> {Featrue}Options
  -> {Feature}OptionsSetup : IConfigureOptions<{Feature}Options>
  -> {Feature}OptionsValidator : IValidateOptions<{Feature}Options>
```

```cs
// {Feature}: OpenTelemetry

// 옵션
public sealed class {Featrue}Options
{

}

// 옵션 자료구조
internal sealed class {Feature}OptionsSetup(IConfiguration configuration) : IConfigureOptions<{Feature}Options>
{
  private const string _configurationSectionName = nameof({Feature}Options);
  private readonly IConfiguration _configuration = configuration;

  public void Configure({Feature}Options options)
  {
    _configuration
      .GetSection(_configurationSectionName)
      .Bind(options);
  }
}

// 옵션 유효성
internal sealed class {Feature}OptionsValidator : IValidateOptions<{Feature}Options>
{
  public ValidateOptionsResult Validate(string? name, {Feature}Options options)
  {
    string validationResult = string.Empty;

    if (options.{실패 조건})
    {
      validationResult += "{실패 이유}";
    }

    // 옵션 실패 조건 ...

    if (!validationResult.IsNullOrEmptyOrWhiteSpace())
    {
      return ValidateOptionsResult.Fail(validationResult);
    }

    return ValidateOptionsResult.Success;
  }
}
```

## 레이어 의존성 주입

OpenTelemetry.Extensions.Hosting
OpenTelemetry.Exporter.Console
OpenTelemetry.Exporter.OpenTelemetryProtocol

<!--
OpenTelemetry.Instrumentation.AspNetCore
OpenTelemetry.Instrumentation.Http
OpenTelemetry.Instrumentation.Runtime
OpenTelemetry.Instrumentation.EntityFrameworkCore -->