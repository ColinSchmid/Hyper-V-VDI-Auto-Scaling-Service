using HyperV.VDIAutoScaling.Core.Metrics;
using Microsoft.Extensions.Logging;
using System.Management;
using System.Runtime.Versioning;

namespace HyperV.VDIAutoScaling.Adapters.Metrics
{
    public class RdsMetricsProvider : IMetricsProvider
    {
        private readonly ILogger<RdsMetricsProvider> _logger;
        public RdsMetricsProvider(ILogger<RdsMetricsProvider> logger)
        {
            _logger = logger;
        }

        int IMetricsProvider.GetActiveSessions()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher(
                    @"root\cimv2",
                    "SELECT ActiveSessions FROM Win32_TerminalService"
                );

                foreach (ManagementObject obj in searcher.Get())
                {
                    if (obj["ActiveSessions"] != null)
                    {
                        return Convert.ToInt32(obj["ActiveSessions"]);
                    }
                }

                //Logs warning if RdsMetricsProvider is unable to determine the active session count.
                _logger.LogWarning("RDS metrics returned no active sessions count");
                
                return 0;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to read RDS active sessions");
                return 0;
            }
        }
    }
}
