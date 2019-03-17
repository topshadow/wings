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
    public class UserController : ControllerBase {

        ///
        public RBACContext rbacContext { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_rbacContext"></param>
        public UserController (RBACContext _rbacContext) { this.rbacContext = _rbacContext; }
        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="loadOptions"></param>    
        [HttpGet]
        public object userList ([FromForm] DataSourceLoadOptions loadOptions) {
            var query = from user in this.rbacContext.Users select
                        new User {username=user.username,orgId=user.orgId,
                            roleIds =user.roleIds,roles=(from role in this.rbacContext.Roles where user.roleIds.Contains(","+role.roleId+",") select role).ToList() };
            return DataSourceLoader.Load (query, loadOptions);
        }

        /// <summary>
        /// 创建组织
        /// </summary>
        /// <param name="bodyData"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Post (BodyData bodyData) {
            var user = new User ();
            JsonConvert.PopulateObject (bodyData.values, user);
            //Validate(order);
            if (!ModelState.IsValid)
                return false;
            this.rbacContext.Users.Add (user);
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

            var user = this.rbacContext.Users.Find (key);
            this.rbacContext.Users.Remove (user);
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
            var user = this.rbacContext.Users.Find (key);
            JsonConvert.PopulateObject (bodyData.values, user);
            this.rbacContext.SaveChanges ();
            return true;
        }
    }
}