
using HyperV.VDIAutoScaling.Core.Models;
using HyperV.VDIAutoScaling.Core.Policies;

namespace HyperV.VDIAutoScaling.Core.Engines
{
    public class DefaultScalingEngine : IScalingEngine
    {
        private readonly IScalingPolicy _policy;

        public DefaultScalingEngine(IScalingPolicy policy)
        {
            _policy = policy;
        }

        public Task RunAsync(CancellationToken cancellationToken)
        {
            //Dummy values for testing
            var currentVdiCount = 7;
            var desiredVdiCount = 100;

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
