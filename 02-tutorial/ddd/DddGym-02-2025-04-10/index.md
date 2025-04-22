
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

## TODO
- [ ] WebApi
- [ ] MediatR pipeline
- [ ] Validation
---
- [ ] OpenTelemetry Logs
- [ ] OpenTelemetry Serilog
- [ ] OpenTelemetry Metrics
- [ ] OpenTelemetry Traces
---
- [ ] 트랜잭션 파이프라인???(이벤트 전달)
- [ ] DTO???