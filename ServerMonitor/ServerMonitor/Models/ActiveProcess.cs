namespace ServerMonitor.Models
{
    public class ActiveProcess
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public long Memory { get; set; }
    }
}
