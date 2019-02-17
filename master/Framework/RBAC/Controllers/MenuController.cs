using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wings.Framework.Controllers;
using Wings.Framework.RBAC.Model;

namespace Wings.Framework.RBAC.Controllers {
    ///
    [Route ("api/rbac/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase {
        RBACContext rbacContext;
        ///
        public MenuController (RBACContext _rbacContext) { this.rbacContext = _rbacContext; }
        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="loadOptions"></param>    
        [HttpGet]
        public object menuList (DataSourceLoadOptions loadOptions) {
            var data = DataSourceLoader.Load (this.rbacContext.Menus, loadOptions);
            return data;
        }

        /// <summary>
        /// 创建组织
        /// </summary>
        /// <param name="bodyData"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Post ([FromForm] BodyData bodyData) {
            var menu = new Menu ();
            JsonConvert.PopulateObject (bodyData.values, menu);
            //Validate(order);
            if (!ModelState.IsValid)
                return false;
            this.rbacContext.Menus.Add (menu);
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

            var menu = this.rbacContext.Menus.Find (key);
            this.rbacContext.Menus.Remove (menu);
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
            var menu = this.rbacContext.Menus.Find (key);
            JsonConvert.PopulateObject (bodyData.values, menu);
            this.rbacContext.SaveChanges ();
            return true;

        }
    }
}