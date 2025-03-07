using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services
    .RegisterPersistenceLayer(builder.Environment, builder.Logging);

using IHost host = builder.Build();

await host.RunAsync();