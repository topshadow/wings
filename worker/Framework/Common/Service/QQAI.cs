using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace worker.Framework.Common.Service
{
    public class QQAI
    {
        public static int APP_ID { get; } = 1106550426;
        public static string APP_KEY { get; } = "00WdwrKA54aXkVG6";


        /// <summary>
        /// api 通用地址
        /// </summary>
        public static string API_COMMON_OCR_URL { get; } = "https://api.ai.qq.com/fcgi-bin/ocr/ocr_generalocr";

        /// <summary>
        /// 通用OCR请求内容
        /// </summary>
        public class CommonOCRRequestContent
        {
            /// <summary>
            /// 应用标识（AppId）
            /// 必填
            /// </summary>
            public int app_id { get; set; }
            /// <summary>
            /// 	请求时间戳（秒级）
            /// 	必填
            /// </summary>
            public long time_stamp { get; set; }
            /// <summary>
            /// 随机字符串
            /// </summary>
            public string nonce_str { get; set; }
            /// <summary>
            /// 非空且长度固定32字节     签名信息，详见接口鉴权
            /// 必填
            /// </summary>
            public string sign { get; set; }
            /// <summary>
            /// 原始图片的base64编码数据（原图大小上限1MB，支持JPG、PNG、BMP格式）	...	待识别图片
            /// </summary>
            public string image { get; set; }
        }


        /// <summary>
        /// 通用OCR
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        async public static Task<Object> commonOCR(string image)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    
                    long timeStamp  =(long)(DateTime.UtcNow).Subtract(new DateTime(1970,1,1,0,0,0,0,DateTimeKind.Utc)).TotalSeconds ; // 相差毫秒数
                    var dicationary = new Dictionary<string, string>();
                    
                    dicationary.Add("app_id", APP_ID.ToString());
                    dicationary.Add("image", image);
                    dicationary.Add("time_stamp", timeStamp.ToString());
                    dicationary.Add("nonce_str", timeStamp.ToString());
                    dicationary.Add("sign", "");

                    var sign = getSign(dicationary);
                    dicationary["sign"] = sign;

                    var keys = dicationary.Keys.ToList();
                    keys.Sort();
                    Console.WriteLine("kes:length" + keys.Count.ToString());

                    var str = "";
                    foreach (var key in keys)
                    {
                        str += key + "=" + dicationary[key].ToString() + "&";
                        Console.WriteLine(key + "=" + dicationary[key]);

                    }
                    var req = new HttpRequestMessage(HttpMethod.Post, API_COMMON_OCR_URL) { Content = new FormUrlEncodedContent(dicationary) };
                    //req.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    //var res = await client.SendAsync(req);
                    //var model = new CommonOCRRequestContent { app_id = APP_ID, image = image, sign = sign, nonce_str = timeStamp.ToString(), time_stamp = timeStamp, };
                    //var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, " 'Content-Type: application/x-www-form-urlencoded'");
                    var response = await client.SendAsync(req);
                    var body = await response.Content.ReadAsStringAsync();
                    return body; 
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e);
                    return "error";
                }
            }

        }

        /// <summary>
        /// 获取函数前面
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string getSign(Dictionary<string, string> param)
        {
            var keys = param.Keys.ToList();
            keys.Sort();
            Console.WriteLine("kes:length"+ keys.Count.ToString());
            var str = "";
            foreach (var key in keys)
            {
                    str += key + "=" + HttpUtility.UrlEncode(param[key].ToString()) + "&";
                    //Console.WriteLine(key+"="+ param[key]);
                
            }
            str += "app_key=" + APP_KEY;
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, str);
                return hash.ToUpper();
            }

        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
