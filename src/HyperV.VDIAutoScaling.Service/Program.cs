using HyperV.VDIAutoScaling.Service;
using HyperV.VDIAutoScaling.Core.Policies;
using HyperV.VDIAutoScaling.Core.Engines;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IScalingPolicy, DefaultScalingPolicy>();
builder.Services.AddSingleton<IScalingEngine, DefaultScalingEngine>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
