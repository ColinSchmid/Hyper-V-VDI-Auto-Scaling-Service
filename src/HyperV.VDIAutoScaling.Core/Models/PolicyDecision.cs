

namespace HyperV.VDIAutoScaling.Core.Models;

public enum ScalingAction
{
    None,
    ScaleOut,
    ScaleIn
}

public record PolicyDecision(
    ScalingAction Action,
    int Amount,
    string Reason
);
