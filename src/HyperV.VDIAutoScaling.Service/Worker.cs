namespace HyperV.VDIAutoScaling.Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Scaling cycle triggered");
            }
            //_scalingEngine.RunAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}
