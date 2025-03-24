# 프로젝트 설정 appsettings.json

## appsettings.json 그룹화
```shell
# 적용 전
├─appsettings.json
├─appsettings.Development.json

# 적용 후: <Project Sdk="Microsoft.NET.Sdk.Web">은 자동 적용됨
├─appsettings.json
│  └─appsettings.Development
```

```xml
<ItemGroup>
  <Content Include="appsettings.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </Content>

  <Content Include="appsettings.*.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    <DependentUpon>appsettings.json</DependentUpon>
  </Content>
</ItemGroup>
```

## appsettings.*.json
```shell
# ASP.NET Core 프로젝트트
appsettings.{ASPNETCORE_ENVIRONMENT}.json

# ASP.NET Core 외 프로젝트
appsettings.{DOTNET_ENVIRONMENT}.json
```

```json
# launchSettings.json
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "{이름}": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "http://localhost:5081",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```