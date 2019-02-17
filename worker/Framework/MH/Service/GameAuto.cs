using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PInvoke.User32;
using static PInvoke.Gdi32;
using System.Diagnostics;
using Wings.worker.Framework.Common.DTO;

namespace Wings.worker.Framework.MH.Service {
    /// <summary>
    /// 游戏自动化脚本
    /// </summary>
    public class GameAuto {

        /// <summary>
        /// 梦幻西游标题高度
        /// </summary>
        public static int titleHeight = 62;
        /// <summary>
        /// 地点列表
        /// </summary>
        public static Dictionary<string, Position> positions = new Dictionary<string, Position> {
            ["总地图"] = new Position (32, 24),
            ["小地图"] = new Position (124, 38),
            ["总地图长安"] = new Position (486 - 32, 500 - 24),
            ["小地图钟馗"] = new Position (319, 441),
            //["抓鬼对话框"]=new Position(847,461)
        };

        /// <summary>
        /// 抓鬼任务
        /// </summary>
        /// <returns></returns>
        public static bool zhuagui (IntPtr ProcessId) {
            var process = Process.GetProcessById ((int) ProcessId);
            SetWindowPos (process.MainWindowHandle, new IntPtr (0), 0, 0, 1024, 768, SetWindowPosFlags.SWP_SHOWWINDOW);
            ClickPosition (process.MainWindowHandle, positions["总地图"]);
            ClickPosition (process.MainWindowHandle, positions["总地图长安"]);
            System.Threading.Thread.Sleep (1000);
            ClickPosition (process.MainWindowHandle, positions["小地图"]);
            ClickPosition (process.MainWindowHandle, positions["小地图钟馗"]);
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
        public static void Click (IntPtr wind, int x, int y, bool reset = false) {
            SetForegroundWindow (wind);
            SendMessage (wind, WindowMessage.WM_LBUTTONDOWN, IntPtr.Zero, MakeLParam (x, y));
            SendMessage (wind, WindowMessage.WM_LBUTTONUP, IntPtr.Zero, MakeLParam (x, y));
        }
        /// <summary>
        /// 点击位置
        /// </summary>
        /// <param name="wind"></param>
        /// <param name="position"></param>
        public static void ClickPosition (IntPtr wind, Position position) {
            Click (wind, position.x, position.y);
        }

        /// <summary>
        /// 长类型指针
        /// </summary>
        /// <param name="wLow"></param>
        /// <param name="wHigh"></param>
        /// <returns></returns>
        public static IntPtr MakeLParam (int wLow, int wHigh) {
            return (IntPtr) (((short) wHigh << 16) | (wLow & 0xffff));
        }
    }
}