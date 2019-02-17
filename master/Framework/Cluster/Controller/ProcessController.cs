// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Drawing;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Wings.Framework.Cluster.Service;

// namespace Wings.worker.MH.Controller
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class ProcessController 
//     {

//         public static List<Process> processList = new List<Process>();

//         [HttpGet("[action]")]
//         public Process startProcess()
//         {

//             var p = Util.CreateProcess();
//             processList.Add(p);
//             return p;

//         }

//         [HttpGet("[action]")]
//         public List<Process> listProcess()
//         {
//             return processList;
//         }
//         [HttpGet("[action]")]
//         public bool captureProcess()
//         {
//             Process p = processList[0];
//             if (p != null)
//             {
//                 Bitmap b = Util.CaptureScreen(p.MainWindowHandle);
//                 b.Save("hello.jpg");

//                 return true;
//             }
//             else
//             {
//                 return false;
//             }

//         }

//     }
// }