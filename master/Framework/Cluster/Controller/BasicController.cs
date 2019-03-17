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

namespace Wings.Framework.Cluster.Controller
{
    /// <summary>
    /// 基础数据控制器
    /// </summary>
    [Route("api/Cluster/[controller]")]
    [ApiController]
    public class BasicController : ControllerBase
    {
        private ClusterContext clusterContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_clusterContext"></param>
       public BasicController(ClusterContext _clusterContext) { this.clusterContext = _clusterContext; }
        /// <summary>
        /// 查询地理位置
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <returns></returns>

            [HttpGet("Position")]
        public Object getPosition(DataSourceLoadOptions loadOptions)
        {
          return  DataSourceLoader.Load(this.clusterContext.Positions, loadOptions);
        }
    }
}