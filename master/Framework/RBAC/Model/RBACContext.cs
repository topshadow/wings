using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Wings.Framework.RBAC.Model {
    /// <summary>
    /// 应用用户
    /// </summary>
    public class ApplicationUser : IdentityUser {

    }
    /// <summary>
    /// 角色权限模块
    /// </summary>
    public class RBACContext : DbContext { /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public RBACContext (DbContextOptions<RBACContext> options) : base (options) { }
        /// <summary>
        /// 组织: 不同组织的角色不同
        /// </summary>
        public DbSet<Org> Orgs { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) { }
    }

    /// <summary>
    /// 组织表
    /// </summary>
    [Table ("Org")]
    public class Org {
        /// <summary>
        /// 组织Id
        /// </summary>
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int orgId { get; set; }
        /// <summary>
        /// 组织名字
        /// </summary>
        public string orgName { get; set; }
        /// <summary>
        /// 上级组织Id
        /// </summary>
        public int parentId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<DateTime> createTime { get; set; }
    }
    /// <summary>
    /// 用户
    /// </summary>
    [Table ("Users")]
    public class User {
        /// <summary>
        /// 用户id
        /// </summary>
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }
        public string username { get; set; }
        public int orgId { get; set; }
        /// <summary>
        /// 创建时间,默认为当前时间
        /// </summary>
        public Nullable<DateTime> createTime { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        public string roleIds { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }

    }
    /// <summary>
    /// 菜单表
    /// </summary>
    [Table ("Menu")]
    public class Menu {
        /// <summary>
        /// 菜单id
        /// </summary>
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int menuId { get; set; }
        public string text { get; set; }
        public int parentId { get; set; }
        public int menuCode { get; set; }
        public string link { get; set; }
        public Nullable<DateTime> createTime { get; set; }

    }
    /// <summary>
    /// 角色
    /// </summary>
    [Table ("Role")]
    public class Role {
        /// <summary>
        /// 角色id
        /// </summary>
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int roleId { get; set; }
        public string name { get; set; }
        public string menuIds { get; set; }

        [NotMapped]
        public List<Menu> menus { get; set; }
    }
}