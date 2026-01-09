using HyperV.VDIAutoScaling.Core.Metrics;

namespace HyperV.VDIAutoScaling.Adapters.Metrics
{
    public class DummyMetricsProvider : IMetricsProvider
    {
        public int GetActiveSessions()
        {
            //Dummy value
            //Will be replaced with Hyper-V Adapte etc.
            return 42;
        }
    }
}
