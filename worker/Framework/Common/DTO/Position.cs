using System;

namespace Wings.worker.Framework.Common.DTO {
    /// <summary>
    /// 地图上的点
    /// </summary>
    public struct Position {
        public int x { get; set; }
        public int y { get; set; }
        /// <summary>
        /// 位置初始化
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        public Position (int _x, int _y) {
            this.x = _x;
            this.y = _y;
        }
    }
}