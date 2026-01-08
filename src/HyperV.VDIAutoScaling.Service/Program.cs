using HyperV.VDIAutoScaling.Service;
using HyperV.VDIAutoScaling.Core.Policies;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
