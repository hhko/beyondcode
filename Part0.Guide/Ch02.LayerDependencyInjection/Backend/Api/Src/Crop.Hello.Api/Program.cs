using Crop.Hello.Api;
using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// TODO: 호스트 로그
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services
    .RegisterPersistenceLayer(builder.Environment, builder.Logging, builder.Configuration);

builder.Services.AddTransient<Class1>();

using IHost host = builder.Build();

Class1 c1 = host.Services.GetRequiredService<Class1>();

await host.RunAsync();


//public static Serilog.ILogger CreateSerilogLogger()
//{
//    return new LoggerConfiguration()
//        .MinimumLevel.Override(Microsoft, Information)
//        .Enrich.FromLogContext()                          // ?
//        .WriteTo.Console()
//        .CreateBootstrapLogger();                         // ?
//}

//Log.Logger = CreateSerilogLogger();
//
//try
//{
//    Log.Information("Staring the host");
//}
//catch (Exception exception)
//{
//    Log.Fatal(exception, "Host terminated unexpectedly");
//    return 1;
//}
//finally
//{
//    Log.Information("Stopping the host");
//    Log.CloseAndFlush();
//}
//
//return 0;

//--------------------------------------------------------------------------------
//
//public static void ConfigureSerilog(this WebApplicationBuilder builder)
//{
//    builder.Host.UseSerilog((context, services, configuration) => configuration
//        .ReadFrom.Configuration(context.Configuration)
//        .ReadFrom.Services(services)
//        .Enrich.FromLogContext());
//}

// https://mohsen.es/configuring-serilog-through-appsettings-json-file-33b26594bb46