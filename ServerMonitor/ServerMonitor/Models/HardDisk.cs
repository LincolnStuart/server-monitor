namespace ServerMonitor.Models
{
    public class HardDisk
    {
        public string Name { get; set; }
        public double FreeSpace { get; set; }
        public double TotalSpace { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public string RootDirectory { get; set; }
    }
    
}
