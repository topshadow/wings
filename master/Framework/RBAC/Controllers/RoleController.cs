using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Wings.Framework.RBAC.Model;
namespace Wings.Framework.RBAC.Controllers {
    /// <summary>
    /// 角色控制器
    /// </summary>
    [Route ("api/rbac/[controller]")]
    // [ApiController]
    public class RoleController : ControllerBase {

        [HttpGet("[action]")]
        public bool migrate()
        {
            this.rbacContext.Database.Migrate();
            return true;
        }
        ///
        public RBACContext rbacContext { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_rbacContext"></param>
        public RoleController (RBACContext _rbacContext) { this.rbacContext = _rbacContext; }
        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="loadOptions"></param>    
        [HttpGet]
        public object roleList ([FromForm] DataSourceLoadOptions loadOptions) {
            var roles = from role in this.rbacContext.Roles

            select new {
                roleId = role.roleId,
                name = role.name,
                menus = from menu in this.rbacContext.Menus where role.menuIds.Contains (menu.menuId.ToString ()) select menu
            };

            return DataSourceLoader.Load (roles, loadOptions);
        }

        /// <summary>
        /// 创建组织
        /// </summary>
        /// <param name="bodyData"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Post (BodyData bodyData) {
            var role = new Role ();
            JsonConvert.PopulateObject (bodyData.values, role);
            //Validate(order);
            if (!ModelState.IsValid)
                return false;
            this.rbacContext.Roles.Add (role);
            this.rbacContext.SaveChanges ();
            return true;
        }

        /// <summary>
        /// 删除组织
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool Delete (int key) {

            var order = this.rbacContext.Roles.Find (key);
            this.rbacContext.Roles.Remove (order);
            this.rbacContext.SaveChanges ();
            return true;
        }
        /// <summary>
        /// 更新组织
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bodyData"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Put (int key, BodyData bodyData) {

            var order = this.rbacContext.Roles.Find (key);
            JsonConvert.PopulateObject (bodyData.values, order);
            order.menuIds = string.Join (",", from menu in order.menus select menu.menuId);
            this.rbacContext.SaveChanges ();
            return true;

        }
    }
}