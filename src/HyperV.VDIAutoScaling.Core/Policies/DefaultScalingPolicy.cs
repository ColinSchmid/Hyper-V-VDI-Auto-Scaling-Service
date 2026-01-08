using HyperV.VDIAutoScaling.Core.Models;
using System.Security.Cryptography.X509Certificates;

namespace HyperV.VDIAutoScaling.Core.Policies
{
    public class DefaultScalingPolicy : IScalingPolicy
    {
        public PolicyDecision Evaluate(ScalingContext context)
        {
            if (context.DesiredVdiCount > context.CurrentVdiCount)
            {
                return new PolicyDecision(
                    ScalingAction.ScaleOut,
                    context.DesiredVdiCount - context.CurrentVdiCount,
                    "Desired capacity is lower than current"
                );
            }

            if (context.DesiredVdiCount < context.CurrentVdiCount)
            {
                return new PolicyDecision(
                    ScalingAction.ScaleIn,
                    context.CurrentVdiCount - context.DesiredVdiCount,
                    "Desired capacity is higher than current"
                );
            }

            return new PolicyDecision(
                ScalingAction.None,
                0,
                "Capacity already matches desired state."
            );
        }
    }
}
