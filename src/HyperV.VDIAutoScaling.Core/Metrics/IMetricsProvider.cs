using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperV.VDIAutoScaling.Core.Metrics
{
    public interface IMetricsProvider
    {
        int GetActiveSessions();
    }
}
