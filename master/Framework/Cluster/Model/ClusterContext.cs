using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Wings.Framework.Cluster.Model
{
    /// <summary>
    /// 集群数据库
    /// 存放不同电脑的配置,运行信息,图片,处理的脚本,待执行的任务,点击
    /// </summary>
    public class ClusterContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public ClusterContext(DbContextOptions<ClusterContext> options) : base(options) { }
        /// <summary>
        /// 组织: 不同组织的角色不同
        /// </summary>
        public DbSet<Worker> Workers { get; set; }
        /// <summary>
        /// 地理位置
        /// </summary>
        public DbSet<Position> Positions { get; set; }
        /// <summary>
        /// 函数
        /// </summary>
        public DbSet<Func> Func { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
    /// <summary>
    /// 地理位置坐标
    /// </summary>
    [Table("Position")]
    public class Position
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int positionId { get; set; }
        /// <summary>
        /// 地理位置分组
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 分组时间
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// x
        /// </summary>
        public int x { get; set; }
        /// <summary>
        /// y
        /// </summary>
        public int y { get; set; }
    }
    /// <summary>
    /// 函数,用于调用具体方法
    /// </summary>
    [Table("Func")]
    public class Func
    {
        /// <summary>
        /// 函数id
        /// </summary>
        public int funcId { get; set; }
        /// <summary>
        /// 别称
        /// </summary>
        public string alias { get; set; }
        /// <summary>
        /// 调用函数的名字
        /// </summary>
        public string funcName { get; set; }
        public string domain { get; set; }
        public string className { get; set; }
        /// <summary>
        /// 参数长度
        /// </summary>
        public int paramsLength { get; set; }

    }
    /// <summary>
    /// 操作
    /// 例如一次点击,拖放,窗口位移等等
    /// </summary>
    [Table("Action")]
    public class Action
    {
        /// <summary>
        /// 
        /// </summary>
        public int actionId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        //public string acti { get; set; }
    }


    /// <summary>
    /// 一台电脑为一个Worker
    /// </summary>
    [Table("Worker")]
    public class Worker
    {
        /// <summary>
        /// workerId
        /// </summary>
        /// <value></value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int workerId { get; set; }
        /// <summary>
        /// 服务器ip
        /// </summary>
        /// <value></value>
        public string ip { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <value></value>
        public string remark { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        /// <value></value>
        public string alias { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <value></value>
        public Nullable<DateTime> createTime { get; set; }
        /// <summary>
        /// 上次登录游戏的时间
        /// </summary>
        /// <value></value>
        public Nullable<DateTime> lastLoginTime { get; set; }

    }
}