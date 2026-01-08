using HyperV.VDIAutoScaling.Core.Engines;

namespace HyperV.VDIAutoScaling.Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IScalingEngine _scalingEngine;

    public Worker(ILogger<Worker> logger, IScalingEngine scalingEngine)
    {
        _logger = logger;
        _scalingEngine = scalingEngine;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Scaling cycle started");

            await _scalingEngine.RunAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
