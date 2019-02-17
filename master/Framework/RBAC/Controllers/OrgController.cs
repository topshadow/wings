using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Wings.Framework.RBAC;
using Wings.Framework.RBAC.Model;

namespace Wings.Framework.Controllers {
    /// <summary>
    /// 
    /// </summary>
    public class BodyData {
        /// <summary>
        /// 
        /// </summary>
        public string values { get; set; }
    }

    /// <summary>
    /// 组织的增删改查
    /// </summary>
    [Route ("api/rbac/[controller]")]
    public class OrgController : Controller

    {
        /// <summary>
        /// 列出所有组织
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpGet ("list")]
        //[SwaggerResponse("201",typeof(Org))]
        public bool list ([FromBody] Org org) {
            return false;
        }

        private RBACContext rbacContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_rbacContext"></param>
        public OrgController (RBACContext _rbacContext) { this.rbacContext = _rbacContext; }
        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="loadOptions"></param>    
        [HttpGet]
        public object orgList (DataSourceLoadOptions loadOptions) {
            return DataSourceLoader.Load (this.rbacContext.Orgs, loadOptions);
        }

        /// <summary>
        /// 创建组织
        /// </summary>
        /// <param name="bodyData"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Post (BodyData bodyData) {
            var org = new Org ();
            JsonConvert.PopulateObject (bodyData.values, org);
            //Validate(order);
            if (!ModelState.IsValid)
                return false;
            this.rbacContext.Orgs.Add (org);
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

            var order = this.rbacContext.Orgs.Find (key);
            this.rbacContext.Orgs.Remove (order);
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
            var order = this.rbacContext.Orgs.Find (key);
            JsonConvert.PopulateObject (bodyData.values, order);
            this.rbacContext.SaveChanges ();
            return true;

        }
    }

}