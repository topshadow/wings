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
        public static readonly IntPtr ProcessId = new IntPtr (10752);

        /// <summary>
        ///  重置位置
        /// </summary>
        /// <returns></returns>
        [HttpGet ("[action]")]
        public bool resetPosition () {
            GameAuto.zhuagui (ProcessId);
            return true;
        }

        public List<GameProcess> listGameProcess () {
            return null;
        }
        public bool autoFlight (){
           
            return true;

        }
    }
}