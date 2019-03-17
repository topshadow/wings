using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO; 
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Tentcent.Ai.Sdk
{
    [Serializable]
    public class ParaData
    {
        public ParaData()
        {

        }

        public ParaData(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return;
            }
            Newtonsoft.Json.JsonReader reader = new JsonTextReader(new StringReader(json));
            //JsonReader reader = new JsonTextReader(new StringReader(json));
            string tempKey = null;
            while (reader.Read())
            {
                if (reader.TokenType.ToString() == "PropertyName")
                {
                    tempKey = reader.Value.ToString();
                }
                if ((reader.TokenType.ToString() == "Boolean" || reader.TokenType.ToString() == "String" || reader.TokenType.ToString() == "Integer") && reader.TokenType.ToString() != "StartObject" && reader.TokenType.ToString() != "EndObject")
                {
                    m_values[tempKey] = reader.Value.ToString();
                }
            }
        }

        public ParaData(string[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                string[] str = data[i].Split('=');
                if (str.Length < 2)
                {
                    return;
                }
                str[1] = HttpUtility.UrlDecode(str[1]);
                m_values[str[0]] = str[1];
            }
        }
        //采用排序的Dictionary的好处是方便对数据包进行签名，不用再签名之前再做一次排序
        public SortedDictionary<string, string> m_values = new SortedDictionary<string, string>();

        /// <summary>
        /// 设置某个字段的值
        /// </summary>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        public void SetValue(string key, string value)
        {
            m_values[key] = value;
        }
        /// <summary>
        /// json字符串 转换 SortedDictionary 方便验签
        /// </summary>
        /// <param name="json">json 字符</param>
        public void SetValues(string json)
        {
            //Log.Debug(this.GetType().ToString() + "---satrt---", json);
            Newtonsoft.Json.JsonReader reader = new JsonTextReader(new StringReader(json));
            //JsonReader reader = new JsonTextReader(new StringReader(json));
            string tempKey = null;
            while (reader.Read())
            {
                if (reader.TokenType.ToString() == "PropertyName")
                {
                    tempKey = reader.Value.ToString();
                }
                if (reader.TokenType.ToString() == "String" && reader.TokenType.ToString() != "StartObject" && reader.TokenType.ToString() != "EndObject")
                {
                    m_values[tempKey] = reader.Value.ToString();
                }
            }
            //Log.Debug(this.GetType().ToString() + "---end---", this.ToJson());
        }
        /// <summary>
        /// 根据字段名获取某个字段的值
        /// </summary>
        /// <param name="key">字段名</param>
        /// <returns>key对应的字段值</returns>
        public string GetValue(string key)
        {
            string o = null;
            m_values.TryGetValue(key, out o);
            return o;
        }

        /// <summary>
        /// 判断某个字段是否已设置
        /// </summary>
        /// <param name="key">字段名</param>
        /// <returns>若字段key已被设置，则返回true，否则返回false</returns>
        public bool IsSet(string key)
        {
            string o = null;
            m_values.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
        }
        /// <summary>
        ///  将Dictionary转成xml
        /// </summary>
        /// <returns>经转换得到的xml串</returns>
        public string ToXml()
        {
            //数据为空时不能转化为xml格式
            if (0 == m_values.Count)
            {
                throw new Exception("Data数据为空!");
            }

            string xml = "<xml>";
            foreach (KeyValuePair<string, string> pair in m_values)
            {
                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                    throw new Exception("Data内部含有值为null的字段!");
                }

                if (pair.Value.GetType() == typeof(int))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    throw new Exception("Data字段数据类型错误!");
                }
            }
            xml += "</xml>";
            return xml;
        }

        /**
        * @将xml转为PayData对象并返回对象内部的数据
        * @param string 待转换的xml串
        * @return 经转换得到的Dictionary
        * @throws WxPayException
        */
        public SortedDictionary<string, string> FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new Exception("将空的xml串转换为Data不合法!");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }

            try
            {
                //2015-06-29 错误是没有签名
                if (m_values["return_code"] != "SUCCESS")
                {
                    return m_values;
                }
                //CheckSign();//验证签名,不通过会抛异常
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return m_values;
        }


        /// <summary>
        /// @Dictionary格式化成Json
        /// </summary>
        /// <returns>json串数据</returns>
        public string ToJson()
        {
            string jsonStr = JsonMapper.ToJson(m_values);
            return jsonStr;
        }

        /// <summary>
        /// @values格式化成能在Web页面上显示的结果（因为web页面上不能直接输出xml格式的字符串）
        /// </summary>
        /// <returns></returns>
        public string ToPrintStr()
        {
            string str = "";
            foreach (KeyValuePair<string, string> pair in m_values)
            {
                if (pair.Value == null)
                {
                    throw new Exception("Data内部含有值为null的字段!");
                }

                str += string.Format("{0}={1}<br>", pair.Key, pair.Value.ToString());
            }
            return str;
        }




        public SortedDictionary<string, string> GetValues()
        {
            return m_values;
        }




        /// <summary>
        /// 根据当前系统时间加随机序列来生成订单号       
        /// </summary>
        /// <returns>订单号</returns>
        public static string GenerateOutTradeNo()
        {
            var ran = new Random();
            return string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(1000, 9999));
        }

        /// <summary>
        /// 获取签名字符串
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetSignContent(IDictionary<string, string> parameters)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder("");
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append("=").Append(value).Append("&");
                }
            }
            string content = query.ToString().Substring(0, query.Length - 1);

            return content;
        }

        /// <summary>
        /// JavaUrlEncode
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(string strCode)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(strCode); //默认是System.Text.Encoding.Default.GetBytes(str)
            System.Text.RegularExpressions.Regex regKey = new System.Text.RegularExpressions.Regex("^[A-Za-z0-9]+$");
            for (int i = 0; i < byStr.Length; i++)
            {
                string strBy = Convert.ToChar(byStr[i]).ToString();
                if (regKey.IsMatch(strBy))
                {
                    //是字母或者数字则不进行转换  
                    sb.Append(strBy);
                }
                else
                {
                    sb.Append(@"%" + Convert.ToString(byStr[i], 16).ToUpper());//javaEncode（urlencode大写）
                }
            }
            return (sb.ToString());
        }

        /// <summary>
        /// base 64 编码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64Encode(string data)
        {
            byte[] encodeBase64Byte = new byte[data.Length];
            encodeBase64Byte = System.Text.Encoding.UTF8.GetBytes(data);
            string encodedData = Convert.ToBase64String(encodeBase64Byte);
            return encodedData;
        }
        /// <summary>
        /// 获取文件的Base64编码
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Base64File(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }
            System.IO.FileStream fs = System.IO.File.OpenRead(path);
            byte[] dt = new byte[fs.Length];
            fs.Read(dt, 0, (int)fs.Length);
            fs.Close();
            return Convert.ToBase64String(dt);
        }
        /// <summary>
        /// Post请求，可定义Headers
        /// </summary>
        /// <param name="Url">请求地址</param>
        /// <param name="PostData">Post参数:page=1&rows=2</param>
        /// <param name="Encode">编码格式,utf-8  gb2312</param>
        /// <returns></returns>
        public static string HttpPost(string Url, string PostData, Encoding Encode, string contentType = "application/x-www-form-urlencoded", System.Collections.Generic.Dictionary<string, string> headerList = null)
        {
            int status = 0;
            string result = "";
            byte[] data = Encode.GetBytes(PostData);

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(Url);

            httpRequest.ServicePoint.ConnectionLimit = 512;
            httpRequest.Method = "POST";
            httpRequest.ContentType = contentType;
            httpRequest.ContentLength = data.Length;

            if (headerList != null)
            {
                foreach (string key in headerList.Keys)
                {
                    httpRequest.Headers.Add(key, headerList[key]);
                }
            }

            using (Stream newStream = httpRequest.GetRequestStream())
            {
                newStream.Write(data, 0, data.Length);
                newStream.Close();
            }
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (StreamReader reqStream = new StreamReader(httpResponse.GetResponseStream(), Encode))
                {
                    result = reqStream.ReadToEnd();

                    status = (int)httpResponse.StatusCode;

                    reqStream.Close();
                }

                httpRequest.Abort();
                httpResponse.Close();
            }

            return result;
        }

        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        /// <summary>
        /// 下单时间
        /// </summary>
        /// <returns></returns>
        public string GetOrderTime()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");

        }

        /// <summary>
        /// Dictionary格式转化成url参数格式
        /// </summary>
        /// <returns>url格式串, 该串不包含sign字段值</returns>
        public string MakeToUrl()
        {
            string buff = "";
            foreach (KeyValuePair<string, string> pair in m_values)
            {
                if (pair.Value == null)
                {
                    throw new Exception("Data内部含有值为null的字段!");
                }

                if (pair.Key != "Sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key.Trim() + "=" + UrlEncode(pair.Value.ToString().Trim()) + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <returns></returns>
        public string MakeSign(string key)
        {
            //转url格式
            string str = MakeToUrl();
            //在string后加入API KEY
            str += "&app_key=" + key;
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2").ToUpper());
            }
            return sb.ToString();
        }

        /// <summary>
        ///检测签名是否正确
        ///正确返回true，错误抛异常
        /// </summary>
        /// <returns></returns>
        public bool CheckSign(string key)
        {
            //如果设置了签名但是签名为空，则抛异常
            if (!IsSet("Sign") || string.IsNullOrEmpty(GetValue("Sign")))
            {
                //throw new Exception("Data签名存在但不合法!");
                return false;
            }
            //获取接收到的签名
            string return_sign = GetValue("Sign").ToString();
            //在本地计算新的签名
            string cal_sign = MakeSign(key);
            if (cal_sign == return_sign)
            {
                return true;
            }
            return false;
        }

        public string ToUrl()
        {
            string buff = "";
            foreach (var pair in m_values)
            {
                if (pair.Value == null)
                {
                    //Log.Error(this.GetType().ToString(), "Data内部含有值为null的字段!");
                    throw new Exception("Data内部含有值为null的字段!");
                }
                if (pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(System.DateTime time, int length = 13)
        {
            long ts = ConvertDateTimeToInt(time);
            return ts.ToString().Substring(0, length);
        }

        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }
    }
}
