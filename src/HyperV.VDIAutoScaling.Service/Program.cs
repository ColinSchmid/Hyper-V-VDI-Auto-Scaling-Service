using HyperV.VDIAutoScaling.Service;
using HyperV.VDIAutoScaling.Core.Policies;
using HyperV.VDIAutoScaling.Core.Engines;
using HyperV.VDIAutoScaling.Core.Planning;
using HyperV.VDIAutoScaling.Core.Configuration;
using HyperV.VDIAutoScaling.Service.Configuration;
using HyperV.VDIAutoScaling.Core.Metrics;
using HyperV.VDIAutoScaling.Adapters.Metrics;
using System.Runtime.Versioning;

[assembly: SupportedOSPlatform("windows")]

var builder = Host.CreateApplicationBuilder(args);

var configDirectory = builder.Configuration["ScalingConfig:Directory"]
    ?? throw new InvalidOperationException(
        "ScalingConfig:Directory not configured"
    );

var configPath = Path.Combine(configDirectory, "scaling.config");

builder.Services.AddSingleton<IScalingPolicy, DefaultScalingPolicy>();
builder.Services.AddSingleton<IScalingEngine, DefaultScalingEngine>();
builder.Services.AddSingleton<ICapacityPlaner, DefaultCapacityPlaner>();
builder.Services.AddSingleton<IMetricsProvider, RdsMetricsProvider>();
builder.Services.AddSingleton<IScalingConfigProvider>(_ => new FileScalingConfigProvider(configPath));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
