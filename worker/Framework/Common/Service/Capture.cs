using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using static PInvoke.User32;
using static PInvoke.Gdi32;
using PInvoke;
namespace Wings.worker.Framework.Common.Service {

    public class Capture {
        public static Bitmap CaptureWindow(IntPtr handle)
        {

            var windowRect = new RECT();
            GetWindowRect(handle, out windowRect);
            Rectangle tScreenRect = new Rectangle(windowRect.left, windowRect.top, windowRect.right-windowRect.left, windowRect.bottom-windowRect.top );
            Bitmap tSrcBmp = new Bitmap(windowRect.right - windowRect.left, windowRect.bottom - windowRect.top); // 用于屏幕原始图片保存
            Graphics gp = Graphics.FromImage(tSrcBmp);
            gp.CopyFromScreen(tScreenRect.Left+5, tScreenRect.Top+5, 0, 0, tScreenRect.Size);
            gp.DrawImage(tSrcBmp, tScreenRect.Left+5, tScreenRect.Top+5, tScreenRect, GraphicsUnit.Pixel);

            return tSrcBmp;

        }
        /// <summary>
        /// 截屏,全部区域
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static Bitmap CaptureScreen (IntPtr handle) {
            handle = GetDesktopWindow ();
            var windowRect = new RECT ();
            GetWindowRect (handle, out windowRect);
            Rectangle tScreenRect = new Rectangle (windowRect.left, windowRect.top, windowRect.right - windowRect.left, windowRect.bottom - windowRect.top);
            Bitmap tSrcBmp = new Bitmap (windowRect.right - windowRect.left, windowRect.bottom - windowRect.top); // 用于屏幕原始图片保存
            Graphics gp = Graphics.FromImage (tSrcBmp);
            gp.CopyFromScreen (0, 0, 0, 0, tScreenRect.Size);
            gp.DrawImage (tSrcBmp, 0, 0, tScreenRect, GraphicsUnit.Pixel);

            return tSrcBmp;
        }

        /**
         *TODO:
         *- 截取部分屏幕
         *- [x]123
         */
        public static Bitmap CaptureScreenRect (IntPtr handle, RECT rect) {
            return null;
        }

    }
}