namespace HyperV.VDIAutoScaling.Core.Engines
{
    public interface IScalingEngine
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
