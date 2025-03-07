using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Crop.Hello.Framework.Utilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using Microsoft.VisualBasic;
using OpenTelemetry;
using System.Diagnostics;
using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.OpenTelemetryOption;
using Destructurama;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;

internal static class OpenTelemetryRegistration
{
    //private static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
    //{
    //    Log.Logger = new LoggerConfiguration()
    //        .ReadFrom.Configuration(configuration)
    //        .CreateLogger();

    //    services.AddSerilog();

    //    return services;
    //}

    internal static IServiceCollection RegisterOpenTelemetry(
        this IServiceCollection services, 
        //ILoggingBuilder logging, 
        IHostEnvironment environment,
        IConfiguration configuration)
    {
        var openTelemetryOptions = services.GetOptions<OpenTelemetryOptions>();
        bool useOnlyConsoleExporter = openTelemetryOptions.IsLocal();

        /*
         * Serilog
         * 
        // FromLogContext", "WithMachineName", "WithProcessId", "WithThreadId

        // "Serilog": {
        //     "WriteTo": [
        //         {
        //              "Name": "Console"
        //         }
        //     ]
        // },
        */
        //"Serilog": {
        //  "Using": [],
        //  "MinimumLevel": {
        //    "Default": "Information",
        //    "Override": {
        //      "Microsoft.Hosting": "Information",
        //      "NetClient.Elastic": "Information"
        //    }
        //  },
        //  "Enrich": [
        //    "FromLogContext",
        //    "WithMachineName",
        //    "WithEnvironmentUserName",
        //    "WithEnvironmentName",
        //    "WithProcessId",
        //    "WithThreadId"
        //  ],
        //  "Properties": {
        //    "Domain": "NetClient",
        //    "DomainContext": "NetClient.Elastic"
        //  },
        //  "WriteTo": [
        //    {
        //      "Name": "Console",
        //      "Args": {
        //        "outputTemplate": "-> [{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        //      }
        //    }
        //  ]
        //}

        
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Destructure.UsingAttributes()
            .CreateLogger();

        //logging.AddSerilog();     // Microsoft Logging -> Microsoft Logging 
        services.AddSerilog();      // Microsoft Logging -> Serilog

        //info: Crop.Hello.Api.Class1[0]
        //    Class1 is
        //LogRecord.Timestamp:               2024 - 12 - 08T22: 54:40.2979150Z
        //LogRecord.CategoryName:            Crop.Hello.Api.Class1
        //LogRecord.Severity:                Info
        //LogRecord.SeverityText:            Information
        //LogRecord.FormattedMessage:        Class1 is
        //LogRecord.Body:                    { Msg} is
        //LogRecord.Attributes(Key: Value):
        //    Msg: Class1
        //    OriginalFormat(a.k.a Body): { Msg} is
        //
        //Resource associated with LogRecord:
        //service.name: Crop.Hello.Api
        //service.version: 1.0.1
        //service.instance.id: 49a7e94d - 9005 - 4315 - a3d7 - 8ef7be3657ba
        //telemetry.sdk.name: opentelemetry
        //telemetry.sdk.language: dotnet
        //telemetry.sdk.version: 1.10.0

        /*
        logging.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;

            if (useOnlyConsoleExporter)
            {
                options.AddConsoleExporter();
            }
            else
            {
                //options.AddOtlpExporter(options => ConfigureOtlpCollectorExporter(options, openTelemetryOptions.OtlpCollectorHost));
            }

            // Resource associated with LogRecord:
            //  - service.name
            //  - service.version
            options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(
                serviceName: openTelemetryOptions.ApplicationName,
                serviceVersion: openTelemetryOptions.Version));

            // 분산 추적과 로그 데이터 간의 연관성 지정합니다.
            //  - 로그가 완료되면 LogRecord의 메시지를 추출하여 현재 Activity에 이벤트로 추가합니다.
            options.AddProcessor(new ActivityEventLogProcessor());
        });
        */

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

    // ActivityEventLogProcessor는 LogRecord와 OpenTelemetry의 Activity를 연결하는 데 사용됩니다.
    //  로그가 완료되면 LogRecord의 메시지를 추출하여 현재 Activity에 이벤트로 추가합니다.
    //  이를 통해, 분산 추적과 로그 데이터 간의 연관성을 강화하여 더 나은 관찰 가능성을 제공합니다.
    private sealed class ActivityEventLogProcessor : BaseProcessor<LogRecord>
    {
        public override void OnEnd(LogRecord log)
        {
            base.OnEnd(log);
            Activity.Current?.AddEvent(new ActivityEvent(log.FormattedMessage!));
        }
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
}