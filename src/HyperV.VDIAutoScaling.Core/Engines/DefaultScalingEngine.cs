

using Microsoft.Extensions.Logging;

using HyperV.VDIAutoScaling.Core.Models;
using HyperV.VDIAutoScaling.Core.Planning;
using HyperV.VDIAutoScaling.Core.Policies;
using HyperV.VDIAutoScaling.Core.Metrics;

namespace HyperV.VDIAutoScaling.Core.Engines
{
    public class DefaultScalingEngine : IScalingEngine
    {
        private readonly IScalingPolicy _policy;
        private readonly ICapacityPlaner _capacityPlaner;
        private readonly IMetricsProvider _metricsProvider;
        private readonly ILogger<DefaultScalingEngine> _logger;

        public DefaultScalingEngine(ILogger<DefaultScalingEngine> logger, IScalingPolicy policy, ICapacityPlaner capacityPlaner, IMetricsProvider metricsProvider)
        {
            _logger = logger;
            _policy = policy;
            _capacityPlaner = capacityPlaner;
            _metricsProvider = metricsProvider;
        }

        public Task RunAsync(CancellationToken cancellationToken)
        {
            var activeSessions = _metricsProvider.GetActiveSessions();

            _logger.LogDebug("Active Sessions reported: {Sessions}", activeSessions);

            //Dummy value for testing
            var currentVdiCount = 7;

            _logger.LogDebug("Starting scaling evaluation: CurrentVDIs={Current}, ActiveSessions={Sessions}", currentVdiCount, activeSessions);

            var desiredVdiCount = _capacityPlaner.CalculateDesiredCapacity(activeSessions, currentVdiCount);

            _logger.LogDebug("Desired capacity calculated as {Desired}", desiredVdiCount);

            var context = new ScalingContext(
                currentVdiCount,
                desiredVdiCount
            );

            var decision = _policy.Evaluate(context);

            _logger.LogInformation("Scaling decision: {Action} {Amount} (Reason: {Reason})", decision.Action, decision.Amount, decision.Reason);

            Console.WriteLine($"Decision: {decision.Action}, Amount: {decision.Amount}, Reason: {decision.Reason}");

            return Task.CompletedTask;
        }
    }
}
