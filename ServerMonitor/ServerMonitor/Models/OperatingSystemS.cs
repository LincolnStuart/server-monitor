using System.Collections.Generic;

namespace ServerMonitor.Models
{
    public class OperatingSystem
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Architecture { get; set; }
        public string Key { get; set; }
        public List<ActiveThread> ActiveThreads { get; set; }
        public List<InstalledWindowsService> InstalledWindowsServices { get; set; }
    }
}
