using HyperV.VDIAutoScaling.Core.Models;

namespace HyperV.VDIAutoScaling.Core.Policies
{
    public interface IScalingPolicy
    {
        PolicyDecision Evaluate(ScalingContext context);
    }
}
