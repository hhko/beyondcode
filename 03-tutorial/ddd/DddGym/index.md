
## WebApi Controller 분리

- Host 프로젝트

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
```

- Adapters.Presentation 프로젝트

```xml
<ItemGroup>
  <FrameworkReference Include="Microsoft.AspNetCore.App" />
</ItemGroup>
```

```cs
services
  .AddControllers()
  .AddApplicationPart(AssemblyReference.Assembly);
```