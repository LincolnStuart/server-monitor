using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerMonitor.Models
{
    public class Usage
    {
        public UseOfProcessor Cpu { get; set; }
        public UseOfMemory Ram { get; set; }
    }
}
