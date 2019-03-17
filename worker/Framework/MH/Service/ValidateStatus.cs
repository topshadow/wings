using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Wings.worker.Framework.Common.Service;

namespace worker.Framework.MH.Service
{

    /// <summary>
    /// 校验游戏状态工具类
    /// </summary>
    public class ValidateStatus
    {
        /// <summary>
        /// 判断是否登录页面
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static bool isLoginPage(IntPtr hwnd)
        {
            var rectangle = new Rectangle(470, 220, 100, 50);
           var  image= Capture.CaptureWindowRectangle(hwnd, rectangle);
            image.Save("a.png");
           var color= image.GetPixel(0, 0);
            if (color.R < 50 && color.B < 50 && color.G < 50)
            {
                Console.WriteLine($"R{color.R},G{color.G},B{color.B}");
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
