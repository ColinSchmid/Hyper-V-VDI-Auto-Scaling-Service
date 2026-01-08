
using HyperV.VDIAutoScaling.Core.Models;
using HyperV.VDIAutoScaling.Core.Planning;
using HyperV.VDIAutoScaling.Core.Policies;

namespace HyperV.VDIAutoScaling.Core.Engines
{
    public class DefaultScalingEngine : IScalingEngine
    {
        private readonly IScalingPolicy _policy;
        private readonly ICapacityPlaner _capacityPlaner;

        public DefaultScalingEngine(IScalingPolicy policy, ICapacityPlaner capacityPlaner)
        {
            _policy = policy;
            _capacityPlaner = capacityPlaner;
        }

        public Task RunAsync(CancellationToken cancellationToken)
        {
            //Dummy values for testing
            var activeSessions = 7;
            var currentVdiCount = 7;

            var desiredVdiCount = _capacityPlaner.CalculateDesiredCapacity(activeSessions, currentVdiCount);

            var context = new ScalingContext(
                currentVdiCount,
                desiredVdiCount
            );

            var decision = _policy.Evaluate(context);

            Console.WriteLine($"Decision: {decision.Action}, Amount: {decision.Amount}, Reason: {decision.Reason}");

            return Task.CompletedTask;
        }
    }
}
