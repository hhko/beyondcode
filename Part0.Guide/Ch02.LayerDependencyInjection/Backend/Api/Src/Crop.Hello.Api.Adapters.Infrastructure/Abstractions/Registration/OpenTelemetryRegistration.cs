using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.OpenTelemetryOptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Crop.Hello.Framework.Utilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;

internal static class OpenTelemetryRegistration
{
    internal static IServiceCollection RegisterOpenTelemetry(
        this IServiceCollection services, 
        ILoggingBuilder logging, 
        IHostEnvironment environment,
        IConfigurationManager configuration)
    {
        var openTelemetryOptions = services.GetOptions<OpenTelemetryOptions>();
        bool useOnlyConsoleExporter = openTelemetryOptions.IsLocal();

        // FromLogContext", "WithMachineName", "WithProcessId", "WithThreadId

        // "Serilog": {
        //     "WriteTo": [
        //         {
        //              "Name": "Console"
        //         }
        //     ]
        // },
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        //logging.AddSerilog();     // Microsoft Logging -> Microsoft Logging 
        services.AddSerilog();      // Microsoft Logging -> Serilog

        //logging.AddOpenTelemetry(options =>
        //{
        //    options.IncludeScopes = true;
        //    options.IncludeFormattedMessage = true;

        //    if (useOnlyConsoleExporter)
        //    {
        //        options.AddConsoleExporter();
        //    }
        //    else
        //    {
        //        //options.AddOtlpExporter(options => ConfigureOtlpCollectorExporter(options, openTelemetryOptions.OtlpCollectorHost));
        //    }

        //    options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(
        //        serviceName: openTelemetryOptions.ApplicationName,
        //        serviceVersion: openTelemetryOptions.Version));

        //    //options.AddProcessor(new ActivityEventLogProcessor());
        //});

        //        //services
        //        //    .AddOpenTelemetry()
        //        //    .AddResources(environment.EnvironmentName, openTelemetryOptions)
        //        //    .WithMetrics(metricBuilder =>
        //        //    {
        //        //        metricBuilder
        //        //            .AddRuntimeInstrumentation()
        //        //            .AddAspNetCoreInstrumentation()
        //        //            .AddFusionCacheInstrumentation()
        //        //            .AddMeter(openTelemetryOptions.Meters);

        //        //        if (useOnlyConsoleExporter)
        //        //        {
        //        //            metricBuilder.AddConsoleExporter();
        //        //        }
        //        //        else
        //        //        {
        //        //            metricBuilder.AddOtlpExporter(options => ConfigureOtlpCollectorExporter(options, openTelemetryOptions.OtlpCollectorHost));
        //        //        }
        //        //    })
        //        //    .WithTracing(traceBuilder =>
        //        //    {
        //        //        if (environment.IsDevelopment())
        //        //        {
        //        //            traceBuilder.SetSampler<AlwaysOnSampler>();
        //        //        }

        //        //        traceBuilder
        //        //            .AddAspNetCoreInstrumentation()
        //        //            .AddHttpClientInstrumentation()
        //        //            .AddFusionCacheInstrumentation()
        //        //            .AddEntityFrameworkCoreInstrumentation();

        //        //        if (useOnlyConsoleExporter)
        //        //        {
        //        //            traceBuilder.AddConsoleExporter();
        //        //        }
        //        //        else
        //        //        {
        //        //            traceBuilder.AddOtlpExporter(options => ConfigureOtlpCollectorExporter(options, openTelemetryOptions.OtlpCollectorHost));
        //        //        }
        //        //    });

        return services;
    }

//    //private static IOpenTelemetryBuilder AddResources(this IOpenTelemetryBuilder builder, string environment, OpenTelemetryOptions openTelemetryOptions)
//    //{
//    //    return builder.ConfigureResource(resourceBuilder => resourceBuilder
//    //        .AddService(serviceName: openTelemetryOptions.ApplicationName, serviceVersion: openTelemetryOptions.Version)
//    //        .AddAttributes(new Dictionary<string, object>
//    //        {
//    //            ["environment.name"] = environment,
//    //            ["team.name"] = openTelemetryOptions.TeamName
//    //        }));
//    //}

//    //private static void ConfigureOtlpCollectorExporter(this OtlpExporterOptions options, string otlpCollectorHost)
//    //{
//    //    const string _grpcCollectorPort = "4317";
//    //    options.Endpoint = new Uri($"http://{otlpCollectorHost}:{_grpcCollectorPort}");
//    //    options.Protocol = OtlpExportProtocol.Grpc;
//    //}

//    //private sealed class ActivityEventLogProcessor : BaseProcessor<LogRecord>
//    //{
//    //    public override void OnEnd(LogRecord log)
//    //    {
//    //        base.OnEnd(log);
//    //        Activity.Current?.AddEvent(new ActivityEvent(log.FormattedMessage!));
//    //    }
//    //}
}