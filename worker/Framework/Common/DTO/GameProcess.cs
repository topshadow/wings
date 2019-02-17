using System;

namespace Wings.worker.Framework.Common.DTO {

    /// <summary>
    /// 游戏进程数据结构
    /// </summary>
    public class GameProcess {
        public int gameId { get; set; }
        /// <summary>
        /// 窗口标题
        /// </summary>
        /// <value></value>
        public GameProcess (string windowTitle, IntPtr pid, string windowImageUrl, string status, string gameStatus) {
            this.windowTitle = windowTitle;
            this.pid = pid;
            this.windowImageUrl = windowImageUrl;
            this.status = status;
            this.gameStatus = gameStatus;

        }
        public string windowTitle { get; set; }

        /// <summary>
        /// 进程id
        /// </summary>
        /// <value></value>
        public IntPtr pid { get; set; }
        /// <summary>
        /// 窗口图像地址
        /// </summary>
        /// <value></value>
        public string windowImageUrl { get; set; }
        /// <summary>
        /// 进程状态 启动,停止
        /// </summary>
        /// <value></value>
        public string status { get; set; }
        /// <summary>
        /// 游戏状态
        /// </summary>
        /// <value></value>
        public string gameStatus { get; set; }

    }
}