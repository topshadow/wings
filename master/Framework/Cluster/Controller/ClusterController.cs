using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wings.Framework.Cluster.Model;
using Wings.Framework.RBAC.Controllers;

namespace Wings.Framework.Cluster.Controllers {
    /// <summary>
    /// 游戏自动化控制器
    /// </summary>
    [ApiController]
    [Route ("api/Cluster/[controller]")]
    public class ClusterController {

        private ClusterContext clusterContext { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clusterContext"></param>
        public ClusterController (ClusterContext _clusterContext) { this.clusterContext = _clusterContext; }
        /// <summary>
        /// 集群查询
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <returns></returns>
        [HttpGet]
        public object Get (DataSourceLoadOptions loadOptions) {
            var data = DataSourceLoader.Load (this.clusterContext.Workers, loadOptions);
            return data;
        }

        /// <summary>
        /// 创建Worker
        /// </summary>
        /// <param name="bodyData"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Post ([FromForm] BodyData bodyData) {
            var worker = new Worker ();
            JsonConvert.PopulateObject (bodyData.values, worker);

            this.clusterContext.Workers.Add (worker);
            this.clusterContext.SaveChanges ();
            return true;
        }

        /// <summary>
        /// 删除Worker
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool Delete (int key) {

            var menu = this.clusterContext.Workers.Find (key);
            this.clusterContext.Workers.Remove (menu);
            this.clusterContext.SaveChanges ();
            return true;
        }
        /// <summary>
        /// 更新Worker
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bodyData"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Put (int key, BodyData bodyData) {
            var menu = this.clusterContext.Workers.Find (key);
            JsonConvert.PopulateObject (bodyData.values, menu);
            this.clusterContext.SaveChanges ();
            return true;

        }
    }
}