using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wings.worker.Framework.Common.DTO;
using Wings.worker.Framework.Common.Service;
using Wings.worker.Framework.MH.Service;
using worker.Framework.Common.Service;
using static PInvoke.User32;


namespace worker.Framework.Common.Controller
{
    [ApiController]
    [Route("api/Worker/Common/[controller]")]
    public class AIController
    {
        /// <summary>
        /// 通用OCR
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
         public object commonOCR()
        {

           var gameProcesses= Finder.findGameProcesses("梦幻西游");
            var hwnd = gameProcesses[0]?.hwnd;
            var rectangle = new Rectangle(810, 184, 168, 86);
            var bitmap = Capture.CaptureWindowRectangle((IntPtr)hwnd, rectangle);
            System.IO.MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            bitmap.Save("test.png");
            byte[] byteImage = ms.ToArray();
            var base64 = Convert.ToBase64String(byteImage);
            var text = AIService.commonOcr(base64);
            Console.WriteLine(text);
            return text;
        }
    }
}
