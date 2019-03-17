using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Wings.Framework.XSS.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class XSSTestResult
    {
        /// <summary>
        /// 原始URL
        /// </summary>
        public string originUrl { get; set; }

        /// <summary>
        /// 实际发送的URl
        /// </summary>
        public string sendUrl { get; set; }

        /// <summary>
        /// 原始参数
        /// </summary>
        public Dictionary<string, object> originParams { get; set; } = new Dictionary<string, object>();
        /// <summary>
        /// 发送参数
        /// </summary>
        public Dictionary<string, object> sendParams { get; set; } = new Dictionary<string, object>();
        /// <summary>
        /// 是否可以进行XSS攻击
        /// </summary>
        public bool isCanXSS { get; set; } = false;
        /// <summary>
        /// 返回的HTML
        /// </summary>
        public string html { get; set; }

    }
    /// <summary>
    /// XSS 注入测试
    /// </summary>
    public interface IXSSService
    {
        /// <summary>
        /// 根据URL注入
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<object> getUrlXSS(string url);
        /// <summary>
        /// POST FORM渗透测试
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        Task<object> postFormData(string url, string formData);
    }
    /// <summary>
    /// XSS注入测试类
    /// </summary>
    public class XSSService : IXSSService
    {
        private HttpClient httpClient = new HttpClient();

        /// <summary>
        /// 初始化用户代理
        /// </summary>
        public XSSService()
        {
            this.httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36");
        }
        /// <summary>
        /// PostForm请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        async public Task<object> postFormData(string url, string formData)
        {
            var keyValueStringArr = formData.Split("&");
            var originContent = new List<KeyValuePair<string, string>> { };
            foreach (var keyValueString in keyValueStringArr)
            {
                var keyValueArr = keyValueString.Split("=");
                originContent.Add(new KeyValuePair<string, string>(keyValueArr[0], keyValueArr[1]));
            }
            var content = new FormUrlEncodedContent(originContent);
            var res = await this.httpClient.PostAsync(url, content);
            var text = await res.Content.ReadAsStringAsync();
            return text;
        }
        /// <summary>
        /// GET URL注入
        /// </summary>
        /// <returns></returns>
        async public Task<object> getUrlXSS(string url)
        {
            var xssTestResult = new XSSTestResult();
            xssTestResult.originUrl = url;

            var uri = new Uri(url);

            var queryParam = uri.ParseQueryString();

            var queryDicttionary = new Dictionary<string, object>();
            var keys = queryParam.Keys;
            foreach (var key in keys)
            {
                var qb = new QueryBuilder();
                qb.Add(key.ToString(), queryParam.Get(key.ToString()) + "fd<xss>sa");

                foreach (var modifyKey in keys)
                {
                    if (modifyKey.ToString() != key.ToString())
                    {
                        qb.Add(modifyKey.ToString(), queryParam.Get(modifyKey.ToString()));
                    }
                }
                var sendUrl = uri.Scheme + "://" + String.Empty;
                sendUrl += uri.Host + uri.AbsolutePath;
                Console.WriteLine(uri.AbsolutePath);
                sendUrl += qb.ToQueryString();
                Console.WriteLine(sendUrl);
                var res = await this.httpClient.GetAsync(sendUrl);
                var text = await res.Content.ReadAsStringAsync();
                //Console.WriteLine(text);
                if (text.Contains("<xss>"))
                {
                    xssTestResult.isCanXSS = true;
                    Console.WriteLine("有漏洞啊");
                }
            }
            return xssTestResult;
        }
    }
}
