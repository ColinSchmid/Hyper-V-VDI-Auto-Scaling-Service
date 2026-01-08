namespace HyperV.VDIAutoScaling.Core.Models
{
    public class ScalingConfig
    { 
        public int MinVdis { get; set; } 
        public int MaxVdis { get; set; }
        public int SessionsPerVDI { get; set; }
        public int Buffer {  get; set; }
        public int ServiceIntervalSeconds { get; set; }
    }

}
