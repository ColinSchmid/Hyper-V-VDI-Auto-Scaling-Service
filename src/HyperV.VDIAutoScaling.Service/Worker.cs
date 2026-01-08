using HyperV.VDIAutoScaling.Core.Configuration;
using HyperV.VDIAutoScaling.Core.Engines;

namespace HyperV.VDIAutoScaling.Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IScalingEngine _scalingEngine;
    private readonly IScalingConfigProvider _configProvider;

    public Worker(ILogger<Worker> logger, IScalingEngine scalingEngine, IScalingConfigProvider configProvider)
    {
        _logger = logger;
        _scalingEngine = scalingEngine;
        _configProvider = configProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Scaling cycle started");

            await _scalingEngine.RunAsync(stoppingToken);

            var interval = _configProvider.GetConfig().ServiceIntervalSeconds;

            await Task.Delay(TimeSpan.FromSeconds(interval), stoppingToken);
        }
    }
}
