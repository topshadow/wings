using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wings.worker.Framework.Common.DTO;
using Wings.worker.Framework.MH.Service;
using static PInvoke.User32;
using static PInvoke.Gdi32;
using  PInvoke;
using Wings.worker.Framework.Common.Service;

namespace Wings.worker.Framework.MH.Controller {
    /// <summary>
    /// 游戏自动化控制器
    /// </summary>
    [ApiController]
    [Route ("api/Worker/MH/[controller]")]
    public class GameAutoController {
        /// <summary>
        /// 写死的静态游戏id
        /// </summary>
        public static readonly IntPtr ProcessId = new IntPtr (19604);

        public static readonly int hwnd = 329764;

        /// <summary>
        /// 自动化玩耍
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
       public bool autoPlay(int hwnd=0)
        {
            if (hwnd == 0)
            {
                var gameProcesses = Finder.findGameProcesses("梦幻西游");
                hwnd = (int)gameProcesses[0].hwnd;
            }
            System.Timers.Timer t25yi = new System.Timers.Timer(5000);//实例化Timer类，设置时间间隔为100毫秒
            t25yi.Elapsed += new System.Timers.ElapsedEventHandler(autoPlayByHwnd);//当到达时间的时候执行MethodTimer2事件 
            t25yi.AutoReset = true;//false是执行一次，true是一直执行
            t25yi.Enabled = true;//设置是否执行System.Timers.Timer.Elapsed事件 
            return true;
        }
       public static void autoPlayByHwnd(object source, System.Timers.ElapsedEventArgs e)
        {
            GameAuto.isInJuqing(hwnd);

            GameAuto.autoPlayTask(hwnd);
            GameAuto.autoUseDrug(hwnd);
            GameAuto.autoCloseSubWindow(hwnd);

        }

        

        /// <summary>
        ///  重置位置
        /// </summary>
        /// <returns></returns>
        [HttpGet ("[action]")]
        public bool resetPosition () {
            GameAuto.zhuagui (ProcessId);
            return true;
        }
        /// <summary>
        /// 自动执行任务
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public bool autoPlayTask(int hwnd)
        {
           return GameAuto.autoPlayTask(hwnd);

        }
        /// <summary>
        /// 自动用药
        /// </summary>
        [HttpGet("[action]")]
        public bool autoUseDrug(int hwnd) {
         return   GameAuto.autoUseDrug(hwnd);
       
        }
        [HttpGet("[action]")]
        public bool isInJuqing(int hwnd)
        {
           return GameAuto.isInJuqing(hwnd);
        }

        [HttpGet("[action]")]
        public bool autoFlight(int hwnd)
        {
            return GameAuto.autoFlight(hwnd);
        }


        public List<GameProcess> listGameProcess () {
            return null;
        }
        public bool autoFlight (){
           
            return true;

        }
    }
}