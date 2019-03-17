using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tentcent.Ai.Sdk
{
    public class OcrServer
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData Init(Dictionary<string, string> dic)
        {
            ParaData sendData = new ParaData();
            sendData.SetValue("app_id", dic["app_id"]);
            sendData.SetValue("time_stamp", ParaData.GetTimeStamp(DateTime.Now, 10));
            sendData.SetValue("nonce_str", ParaData.GenerateOutTradeNo());
            return sendData;
        }
        /// <summary>
        /// 身份证OCR
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData IdCard(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            //身份证图片类型，0-正面，1-反面
            sendData.SetValue("card_type", dic["card_type"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(OcrUrl.idCardUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 名片OCR
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData NameCard(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(OcrUrl.nameCardUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 行驶证驾驶证OCR
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData Drvie(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            //识别类型，0-行驶证识别，1-驾驶证识别
            sendData.SetValue("card_type", dic["card_type"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(OcrUrl.driveUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 车牌OCR
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData Car(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            if (dic.ContainsKey("image"))
            {
                sendData.SetValue("image", dic["image"]);
            }
            if (dic.ContainsKey("image_url"))
            {
                sendData.SetValue("image_url", dic["image_url"]);
            }
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(OcrUrl.carUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 营业执照OCR
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData Biz(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(OcrUrl.bizUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 银行卡OCR
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData Bank(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(OcrUrl.bankUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 通用OCR
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData Gen(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(OcrUrl.genUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }

        /// <summary>
        /// 手写体OCR
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData Hand(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(OcrUrl.handUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
    }
}
