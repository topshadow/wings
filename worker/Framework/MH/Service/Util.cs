using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PInvoke;
using static PInvoke.User32;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using static PInvoke.Gdi32;

namespace Wings.worker.Framework.MH.Service {
    interface IUtil {

    }
    public class Util {
        /// <summary>
        /// 桌面截图
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static Bitmap CaptureWindowScreen (IntPtr handle) {

            handle = GetDesktopWindow ();
            var windowRect = new RECT ();
            GetWindowRect (handle, out windowRect);
            Rectangle tScreenRect = new Rectangle (0, 0, windowRect.right - windowRect.left, windowRect.bottom - windowRect.top);
            Bitmap tSrcBmp = new Bitmap (windowRect.right - windowRect.left, windowRect.bottom - windowRect.top); // 用于屏幕原始图片保存
            Graphics gp = Graphics.FromImage (tSrcBmp);
            gp.CopyFromScreen (0, 0, 0, 0, tScreenRect.Size);
            gp.DrawImage (tSrcBmp, 0, 0, tScreenRect, GraphicsUnit.Pixel);
            return tSrcBmp;
        }

        //private Bitmap GetBitmap(IntPtr hbitmap)
        //{
        //    Bitmap bmp = new Bitmap(FromHbitmap(new IntPtr(hbitmap)), FromHbitmap(new IntPtr(hbitmap)).Width, FromHbitmap(new IntPtr(hbitmap)).Height);
        //    return bmp;
        //}

        public static void RunExe (string fielPath) {
            throw new NotImplementedException ();

        }

        public static Process CreateProcess () {
            //WinApi.createP
            return RunExe (ShanShanKeExePath, "");

        }

        public static string ShanShanKeExePath = "C:\\Users\\Administrator\\Desktop\\SSKMH.exe";
        /// <summary>
        /// 启动exe
        /// </summary>
        /// <param name="filePath">程序路径</param>
        /// <param name="argument">参数</param>
        /// <param name="waitTime">等待时间，毫秒计</param>
        public static Process RunExe (string filePath, string argument, int waitTime = 0) {
            if (string.IsNullOrEmpty (filePath)) {
                throw new Exception ("filePath is empty");
            }
            if (!System.IO.File.Exists (filePath)) {
                throw new Exception (filePath + " is not exist");
            }
            string directory = Path.GetDirectoryName (filePath);

            try {
                Process p = new Process ();

                p.StartInfo.FileName = filePath;
                p.StartInfo.WorkingDirectory = directory;
                p.StartInfo.Arguments = argument;
                p.StartInfo.ErrorDialog = false;

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; //与CreateNoWindow联合使用可以隐藏进程运行的窗体
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardError = true;
                p.EnableRaisingEvents = true; // 启用Exited事件
                //p.Exited += p_Exited;

                p.Start ();
                if (waitTime > 0) {
                    p.WaitForExit (waitTime);
                }
                return p;

                //if (p.ExitCode == 0)//正常退出
                //{
                //    //TODO记录日志
                //    System.Console.WriteLine("执行完毕！");
                //}

            } catch (Exception ex) {
                throw new Exception ("系统错误：", ex);
            }
        }
    }

}