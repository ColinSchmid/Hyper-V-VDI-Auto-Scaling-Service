using HyperV.VDIAutoScaling.Service;
using HyperV.VDIAutoScaling.Core.Policies;
using HyperV.VDIAutoScaling.Core.Engines;
using HyperV.VDIAutoScaling.Core.Planning;
using HyperV.VDIAutoScaling.Core.Configuration;
using HyperV.VDIAutoScaling.Service.Configuration;

var builder = Host.CreateApplicationBuilder(args);

var configPath = @"C:\ProgramData\HyperVVDIAutoScaling\scaling.json";

builder.Services.AddSingleton<IScalingPolicy, DefaultScalingPolicy>();
builder.Services.AddSingleton<IScalingEngine, DefaultScalingEngine>();
builder.Services.AddSingleton<ICapacityPlaner, DefaultCapacityPlaner>();
builder.Services.AddSingleton<IScalingConfigProvider>(_ => new FileScalingConfigProvider(configPath));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
