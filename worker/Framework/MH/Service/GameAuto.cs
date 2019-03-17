using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PInvoke.User32;
using static PInvoke.Gdi32;
using System.Diagnostics;
using Wings.worker.Framework.Common.DTO;
using System.Timers;
using Wings.worker.Framework.Common.Service;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
//using Wings.worker.Framework.Common.Service;

namespace Wings.worker.Framework.MH.Service
{
    /// <summary>
    /// 游戏自动化脚本
    /// </summary>
    public class GameAuto
    {

        /// <summary>
        /// 梦幻西游标题高度
        /// </summary>
        public static int titleHeight = 62;
        /// <summary>
        /// 地点列表
        /// </summary>
        public static Dictionary<string, Position> positions = new Dictionary<string, Position>
        {
            ["总地图"] = new Position(32, 24),
            ["小地图"] = new Position(124, 38),
            ["总地图长安"] = new Position(486 - 32, 500 - 24),
            ["小地图钟馗"] = new Position(319, 441),
            //["抓鬼对话框"]=new Position(847,461)
        };
        public static bool autoPlayTask(int processId)
        {
            playTask(processId);
            return true;
        }
        /// <summary>
        /// 自动使用推荐物品
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static bool autoUseDrug(int hwnd)
        {

            var rectange = new Rectangle(840, 500, 200, 40);
            var bitmap = Capture.CaptureWindowRectangle((IntPtr)hwnd, rectange);
            var color = bitmap.GetPixel(5, 5);
            Console.WriteLine($"R{color.R},B{color.B},G{color.G}");
            bitmap.Save("autouse.png");
            if (color.G == 210 && color.B == 173 && color.R == 234)
            {
                Click((IntPtr)hwnd, 860, 650);
                return true;
            }
            else
            {
                return false;
            }



        }

        /// <summary>
        /// 自动关闭小窗口
        /// position : 841,169
        /// RGB:211,0,0
        /// </summary>
        /// <returns></returns>
        public static bool autoCloseSubWindow(int hwnd)
        {
            var rectange = new Rectangle(839, 167, 20, 40);
            var bitmap = Capture.CaptureWindowRectangle((IntPtr)hwnd, rectange);
            var color = bitmap.GetPixel(2, 2);
            Console.WriteLine($"R{color.R},B{color.B},G{color.G}");
            bitmap.Save("autoCloseSubWindow.png");
            if (color.R == 211 && color.B == 0 && color.G == 0)
            {
                Click((IntPtr)hwnd, 841, 169);
                return true;
            }
            else
            {
                return false;
            }


        }

        /// <summary>
        /// 是否在剧情里
        /// </summary>
        public static bool isInJuqing(int hwnd)
        {

            var rectange = new Rectangle(768, 768, 200, 40);
            var bitmap = Capture.CaptureWindowRectangle((IntPtr)hwnd, rectange);
            var color = bitmap.GetPixel(2, 2);
            Console.WriteLine($"c R{color.R},B{color.B},G{color.G}");
            bitmap.Save("isInJuqing.png");
            var r2 = new Rectangle(737, 556, 20, 20);
            var b2 = Capture.CaptureWindowRectangle((IntPtr)hwnd, r2);
            var c2 = b2.GetPixel(2, 2);
            Console.WriteLine($"c2 R{c2.R},B{c2.B},G{c2.G}");
            // position : 788,508
            // color(RGB): 240,219,189
            var r3 = new Rectangle(787, 507, 20, 20);
            var b3 = Capture.CaptureWindowRectangle((IntPtr)hwnd, r3);
            var c3 = b3.GetPixel(1, 1);
            Console.WriteLine($"c3: R{c3.R},B{c3.B},G{c3.G}");
            //position 785,396
            //color
            var r4 = new Rectangle(784, 395, 20, 20);
            var b4 = Capture.CaptureWindowRectangle((IntPtr)hwnd, r4);
            var c4 = b4.GetPixel(1, 1);
            Console.WriteLine($"c4: R{c4.R},B{c4.B},G{c4.G}");
            //762,451
            var r5 = new Rectangle(761, 450, 20, 20);
            var b5 = Capture.CaptureWindowRectangle((IntPtr)hwnd, r5);
            var c5 = b5.GetPixel(1, 1);
            Console.WriteLine($"c5: R{c5.R},B{c5.B},G{c5.G}");

            // 781,511
            var r6 = new Rectangle(780, 510, 20, 20);
            var b6 = Capture.CaptureWindowRectangle((IntPtr)hwnd, r6);
            var c6 = b6.GetPixel(1, 1);
            Console.WriteLine($"c6: R{c6.R},B{c6.B},G{c6.G}");


            //c2:R244,B202,G228
            if ((color.R == 37 && color.B == 38 && color.G == 37))
            {

                Click((IntPtr)hwnd, 768, 768);
                return true;
            }
            else if (c6.R == 237 && c6.B == 186)
            {
                Click((IntPtr)hwnd, 781, 511);
                Console.WriteLine("傲来国偷偷怪");
                return true;
            }
            //238,B187,G217
            else if (c5.R == 238 && c5.B == 187)
            {
                Click((IntPtr)hwnd, 762, 451);
                Console.WriteLine("选择宝象国奇遇");
                return true;
            }
           
            else if (c2.R == 244 && c2.B == 202 && c2.G == 228)
            {
                Click((IntPtr)hwnd, 737, 556);
                return true;

            }
            // 宝象国奇遇,选择相信还是不相信,选择相信
            else if (c3.R == 240 && c3.G == 219 && c3.B == 189)
            {
                Click((IntPtr)hwnd, 788, 508);
                Console.WriteLine("选择相信");
                return true;
            }
            //
            else if (c4.R == 55 && c4.B == 36)
            {
                Click((IntPtr)hwnd, 784, 395);
                Console.WriteLine("选择宝象国奇遇");
                return true;
            }
          
            else
            {
                return false;
            }


        }

        /// <summary>
        /// 自动战斗
        /// </summary>
        /// <returns></returns>
        public static bool autoFlight(int hwnd)
        {
            var rectange = new Rectangle(973, 780, 20, 20);
            var bitmap = Capture.CaptureWindowRectangle((IntPtr)hwnd, rectange);
            var color = bitmap.GetPixel(2, 2);
            Console.WriteLine($"R{color.R},B{color.B},G{color.G}");
            bitmap.Save("autoFlight.png");

            return true;
        }


        private static void playTask(int hwnd)
        {

            var rectangle = new Rectangle(810, 184, 168, 86);
            var bitmap = Capture.CaptureWindowRectangle((IntPtr)hwnd, rectangle);
            System.IO.MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            bitmap.Save("test.png");
            byte[] byteImage = ms.ToArray();
            var base64 = Convert.ToBase64String(byteImage);
            var text = AIService.commonOcr(base64);
            var taskText = text.GetValues()["itemstring"];
            Console.WriteLine("task-title:" + taskText);
            if (taskText.Contains("20级"))
            {
                Click((IntPtr)hwnd, 856, 324);
                Console.WriteLine("已经满了16级,进行第二个任务");
            }
            else
            {
                Click((IntPtr)hwnd, 1000, 240);
                Console.WriteLine("OK, test event is fired at: " + DateTime.Now.ToString());

            }
            foreach (var key in text.GetValues().Keys)
            {
                Console.WriteLine("key:" + key);
            }

        }

        /// <summary>
        /// 重置窗口位置
        /// </summary>
        /// <param name="ProcessId"></param>
        public static void resetPosition(IntPtr ProcessId)
        {
            var process = Process.GetProcessById((int)ProcessId);
            SetWindowPos(process.MainWindowHandle, new IntPtr(0), 0, 0, 1024, 768, SetWindowPosFlags.SWP_SHOWWINDOW);
        }

        /// <summary>
        /// 抓鬼任务
        /// </summary>
        /// <returns></returns>
        public static bool zhuagui(IntPtr ProcessId)
        {
            var process = Process.GetProcessById((int)ProcessId);
            SetWindowPos(process.MainWindowHandle, new IntPtr(0), 0, 0, 1024, 768, SetWindowPosFlags.SWP_SHOWWINDOW);
            ClickPosition(process.MainWindowHandle, positions["总地图"]);
            ClickPosition(process.MainWindowHandle, positions["总地图长安"]);
            System.Threading.Thread.Sleep(1000);
            ClickPosition(process.MainWindowHandle, positions["小地图"]);
            ClickPosition(process.MainWindowHandle, positions["小地图钟馗"]);
            //System.Threading.Thread.Sleep(1000);
            //ClickPosition(process.MainWindowHandle, positions["抓鬼对话框"]);

            return true;
        }
        /// <summary>
        /// 鼠标位移事件
        /// </summary>
        /// <param name="wind"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="reset"></param>
        public static void Click(IntPtr wind, int x, int y, bool reset = false)
        {
            SetForegroundWindow(wind);
            SendMessage(wind, WindowMessage.WM_LBUTTONDOWN, IntPtr.Zero, MakeLParam(x, y));
            SendMessage(wind, WindowMessage.WM_LBUTTONUP, IntPtr.Zero, MakeLParam(x, y));
        }
        /// <summary>
        /// 点击位置
        /// </summary>
        /// <param name="wind"></param>
        /// <param name="position"></param>
        public static void ClickPosition(IntPtr wind, Position position)
        {
            Click(wind, position.x, position.y);
        }

        /// <summary>
        /// 长类型指针
        /// </summary>
        /// <param name="wLow"></param>
        /// <param name="wHigh"></param>
        /// <returns></returns>
        public static IntPtr MakeLParam(int wLow, int wHigh)
        {
            return (IntPtr)(((short)wHigh << 16) | (wLow & 0xffff));
        }
    }
}