using System.Collections.Generic;

namespace ServerMonitor.Models
{
    public class Hardware
    {
        public Processor Processor { get; set; }
        public Ram Ram { get; set; }
        public Graphics Graphics { get; set; }
        public List<HardDisk> HardDisks { get; set; }
    }
}
