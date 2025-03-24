# 프로젝트 설정 appsettings.json

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
