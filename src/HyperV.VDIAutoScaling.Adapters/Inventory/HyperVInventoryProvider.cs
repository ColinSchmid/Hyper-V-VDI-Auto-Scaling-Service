using HyperV.VDIAutoScaling.Core.Inventory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace HyperV.VDIAutoScaling.Adapters.Inventory
{
    public class HyperVInventoryProvider : IInventoryProvider
    {
        private readonly ILogger<HyperVInventoryProvider> _logger;

        //Needs to be moved to config
        private const string VDINamePrefix = "VDI-";

        public HyperVInventoryProvider(ILogger<HyperVInventoryProvider> logger) 
        {
            _logger = logger; 
        }
        public int GetCurrentVdiCount()
        {
            try
            {
                int count = 0;

                using var searcher = new ManagementObjectSearcher(
                    @"root\virtualization\v2",
                    "SELECT ElementName FROM Msvm_ComputerSystem WHERE Description = 'Microsoft Virtual Machine'"
                );

                foreach (ManagementObject vm in searcher.Get())
                {
                    var name = vm["ElementName"]?.ToString();
                    if(name != null && name.StartsWith(VDINamePrefix)) {
                        count++;
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to enumerate Hyper-V VMs");
                return 0;
            }
        }
    }
}
