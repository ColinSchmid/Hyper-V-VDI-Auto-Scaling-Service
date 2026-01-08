namespace HyperV.VDIAutoScaling.Core.Models;

public record ScalingContext(
    int CurrentVdiCount,
    int DesiredVdiCount
);
