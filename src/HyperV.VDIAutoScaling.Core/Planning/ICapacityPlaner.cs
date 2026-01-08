namespace HyperV.VDIAutoScaling.Core.Planning
{
    public interface ICapacityPlaner
    {
        int CalculateDesiredCapacity(
            int activeSessions,
            int currentVdiCount
            );
    }
}
