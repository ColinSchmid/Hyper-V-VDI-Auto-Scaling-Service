namespace HyperV.VDIAutoScaling.Core.Planning
{
    public class DefaultCapacityPlaner : ICapacityPlaner
    {
        private const int SessionsPerVdi = 1;
        private const int Buffer = 1;

        int ICapacityPlaner.CalculateDesiredCapacity(int activeSessions, int currentVdiCount)
        {
            var requiered = (int)Math.Ceiling(activeSessions / (double)SessionsPerVdi);

            return Math.Max(requiered + Buffer, 1);
        }
    }
}
