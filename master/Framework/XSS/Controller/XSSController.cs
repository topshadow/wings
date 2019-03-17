using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wings.Framework.XSS.Service;

namespace Wings.Framework.XSS.Controller
{

    /// <summary>
    /// 请求方法
    /// </summary>
    public enum Method
    {
        /// <summary>
        /// 
        /// </summary>
        Get,
        /// <summary>
        /// POST 
        /// FORM
        /// </summary>
        FORM_POST,
        /// <summary>
        /// POST
        /// </summary>
        POST


    }
    /// <summary>
    /// xss测试请求体
    /// </summary>
    public class XSSTestBodyInput
    {
        /// <summary>
        /// 渗透测试的URL地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 渗透测试的方法
        /// </summary>
        public Method method { get; set; }
        /// <summary>
        /// 表单数据
        /// </summary>
        public string formData { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class XSSController : ControllerBase
    {

        private IXSSService xssSerivce { get; set; }
        private HttpClient httpClient { get; set; } = new HttpClient();
        private XSSService xssService { get; set; }

        class TestBody
        {
            public string html { get; set; }
            public object queryParam { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_xssService"></param>
        public XSSController(IXSSService _xssService) { this.xssSerivce = _xssService; }
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        async public Task<Object> testXSS()
        {
            var uri = new Uri("https://www.baidu.com?a=a");
            var queryParam = uri.ParseQueryString();
            Console.WriteLine(queryParam);
            foreach (var key in queryParam.Keys)
            {
                Console.WriteLine(key + ":" + queryParam.Get(key.ToString()));
            }
            var res = await this.httpClient.GetAsync("https://www.baidu.com");
            return new TestBody { html = await res.Content.ReadAsStringAsync(), queryParam = queryParam };
        }
        /// <summary>
        /// xss测试
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        async public Task<Object> xssTest([FromBody]XSSTestBodyInput body)
        {
            switch (body.method)
            {
                case Method.Get:
                    var xssTestResult = await this.xssSerivce.getUrlXSS(body.url);
                    return xssTestResult;
                case Method.FORM_POST:
                    var xssTestResultFormPost = await this.xssSerivce.postFormData(body.url,body.formData);
                    return xssTestResultFormPost;
                default:
                    return null;
            }




        }

    }
}