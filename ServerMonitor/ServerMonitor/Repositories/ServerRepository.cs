using ServerMonitor.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Threading;

namespace ServerMonitor.Repositories
{
    public class ServerRepository
    {
        public ServerInfo GetInfo()
        {
            return new ServerInfo()
            {
                Bios = GetBios(),
                OperatingSystem = GetOperationSystem(),
                Hardware = GetHardware()
            };
        }

        private IList<InstalledWindowsService> GetServices()
        {
            var result = new List<InstalledWindowsService>();
            ServiceController.GetServices().ToList().ForEach(s =>
                {
                    var installedWindowsService = new InstalledWindowsService()
                    {
                        Name = s.DisplayName,
                        Status = s.Status.ToString()
                    };
                    result.Add(installedWindowsService);
                });
            return result;
        }

        private IList<ActiveProcess> GetProcesses()
        {
            var result = new List<ActiveProcess>();
            Process.GetProcesses().ToList().ForEach(p =>
                {
                    var activeProccess = new ActiveProcess()
                    {
                        Id = p.Id,
                        Name = p.ProcessName,
                        Priority = p.BasePriority,
                        Memory = p.PrivateMemorySize64
                    };
                    result.Add(activeProccess);
                });
            return result;
        }

        private IList<ActiveThread> GetThreads()
        {
            var result = new List<ActiveThread>();
            var threads = Process.GetCurrentProcess().Threads;
            foreach (ProcessThread t in threads)
            {
                var activeThread = new ActiveThread()
                {
                    Id = t.Id,
                    Priority = t.CurrentPriority.ToString(),
                    Status = t.ThreadState.ToString()
                };
                result.Add(activeThread);
            }
            return result;
        }

        private UseOfProcessor GetUseOfProcessor()
        {
            var counter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            return new UseOfProcessor() { Percentage = counter.NextValue() };
        }

        private UseOfMemory GetUseOfMemory()
        {
            var counter = new PerformanceCounter("Memory", "Available MBytes"); ;
            return new UseOfMemory() { Mb = counter.NextValue() };
        }

        private OperatingSystem GetOperationSystem()
        {
            var result = new OperatingSystem();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject mo in searcher.Get())
            {
                result.Name = mo.GetPropertyValue("Caption").ToString();
                result.Version = mo.GetPropertyValue("Version").ToString();
                result.Architecture = mo.GetPropertyValue("OSArchitecture").ToString();
                //result.Key = mo.GetPropertyValue("SerialNumber").ToString();
                result.Key = "##### - ##### - ##### - #####";
            }
            return result;
        }

        private Bios GetBios()
        {
            var result = new Bios();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            foreach (ManagementObject mo in searcher.Get())
            {
                result.Name = mo.GetPropertyValue("Name").ToString();
                result.Version = mo.GetPropertyValue("Version").ToString();
                result.Manufacturer = mo.GetPropertyValue("Manufacturer").ToString();
                result.SerialNumber = mo.GetPropertyValue("SerialNumber").ToString();
            }
            return result;
        }

        private Hardware GetHardware()
        {
            return new Hardware()
            {
                Processor = GetProcessor(),
                Graphics = GetGraphics(),
                Ram = GetRam()
            };
        }

        private Graphics GetGraphics()
        {
            var result = new Graphics();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");
            foreach (ManagementObject mo in searcher.Get())
            {
                result.Description = mo.GetPropertyValue("Description").ToString();
            }
            return result;
        }

        private Processor GetProcessor()
        {
            var result = new Processor();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject mo in searcher.Get())
            {
                result.Name = mo.GetPropertyValue("Name").ToString();
            }
            return result;
        }

        private UseOfMemory GetRamStatistics()
        {
            var counter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            counter.NextValue();
            Thread.Sleep(1000);
            return new UseOfMemory()
            {
                Mb = counter.NextValue()
            };
        }

        private UseOfProcessor GetCpuStatistics()
        {
            var counter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            counter.NextValue();
            Thread.Sleep(1000);
            return new UseOfProcessor()
            {
                Percentage = counter.NextValue()
            };
        }

        public Usage GetUsage()
        {
            return new Usage
            {
                Cpu = GetCpuStatistics(),
                Ram = GetRamStatistics()
            };
        }

        private Ram GetRam()
        {
            var result = new Ram();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            long ramSize = 0;
            foreach (ManagementObject mo in searcher.Get())
            {
                ramSize += System.Convert.ToInt64(mo.GetPropertyValue("Capacity"));
            }
            ramSize = (ramSize / 1024) / 1024 / 1024;
            result.Max = $"{ramSize} GB";
            return result;
        }

    }
}
