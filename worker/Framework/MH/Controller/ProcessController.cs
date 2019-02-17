using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


using Wings.worker.Framework.Common.DTO;
using Wings.worker.Framework.Common.Service;
using Wings.worker.Framework.MH.Service;
using static PInvoke.User32;

namespace Wings.worker.Framework.MH.Controller
{
    [ApiController]
    [Route("api/Worker/MH/[controller]")]
    public class ProcessController
    {

        public static List<Process> processList = new List<Process>();
        [HttpGet("[action]")]
        public Object listGameProcess(DataSourceLoadOptions loadOptions)

        {
            string name = "梦幻西游";
            var gameProcesses = Finder.findGameProcesses(name);
            foreach (var gameProcess in gameProcesses)
            {
                var currentWindow = GetForegroundWindow();
                SetForegroundWindow(gameProcess.pid);
                System.IO.MemoryStream stream = new MemoryStream();
                var bitmap = Capture.CaptureScreen(gameProcess.pid);
                var key = gameProcess.pid.ToString() + "-" + DateTime.Now.Millisecond.ToString() + ".png";
                var putResult = OSSService.uploadBitmap("wingsworker", key, bitmap);
                gameProcess.windowImageUrl = OSSService.url + "/" + key;
                gameProcess.windowTitle = Finder.getWindowTitle(gameProcess.pid);
                gameProcess.status = "active";
                SetForegroundWindow(currentWindow);
            }
            return DataSourceLoader.Load(gameProcesses.ToArray(), loadOptions);
        }

        [HttpGet("[action]")]
        public Process startProcess()
        {
            var p = Util.CreateProcess();
            processList.Add(p);
            return p;
        }

        [HttpGet("[action]")]
        public List<Process> listProcess()
        {
            return processList;
        }

        [HttpGet("[action]")]
        public bool captureProcess()
        {
            Process p = processList[0];
            if (p != null)
            {
                Bitmap b = Util.CaptureWindowScreen(p.MainWindowHandle);
                b.Save("hello.jpg");
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}