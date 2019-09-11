using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerMonitor.Models;
using ServerMonitor.Repositories;

namespace ServerMonitor.Controllers
{
    public class MonitorController : Controller
    {
        private ServerInfo info;
        private ServerRepository repo;
        private static int teste;

        public MonitorController(ServerRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            info = repo.GetInfo();
            return View(info);
        }

        [HttpPost]
        public Usage UpdateRam()
        {
           return repo.GetUsage();
        }
    }
}