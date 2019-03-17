using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tentcent.Ai.Sdk
{
    public class FaceServer
    {
        /// <summary>
        /// 共用参数
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
        /// 人脸检测与分析
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData DetectFace(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            //检测模式，0-正常，1-大脸模式（默认1）
            sendData.SetValue("mode", dic["mode"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.detectFaceUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 多人脸检测
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData DetectMultiFace(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.detectMultiFaceUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 人脸对比
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData FaceCompare(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image_a", dic["image_a"]);
            sendData.SetValue("image_b", dic["image_b"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.faceCompareUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 跨年龄人脸识别
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData DetectCrossAge(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("source_image", dic["source_image"]);
            sendData.SetValue("target_image", dic["target_image"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.detectCrossAgeUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 五官定位
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData FaceShape(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            //检测模式，0-正常，1-大脸模式（默认1）
            sendData.SetValue("mode", dic["mode"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.faceShapeUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 人脸识别
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData FaceIdentify(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            sendData.SetValue("group_id", dic["group_id"]);
            sendData.SetValue("topn", dic["topn"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.faceIdentifyUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 人脸验证
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData FaceVerify(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            sendData.SetValue("person_id", dic["person_id"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.faceVerifyUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }

        /// <summary>
        /// 个体创建
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData NewPerson(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            sendData.SetValue("person_id", dic["person_id"]);
            sendData.SetValue("group_ids", dic["group_ids"]);
            sendData.SetValue("person_name	", dic["person_name"]);
            sendData.SetValue("tag", dic["tag"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.newPersonFaceUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }

        /// <summary>
        /// 删除个体
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData DelPerson(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("person_id", dic["person_id"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.delPersonUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 增加人脸
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData AddFace(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("image", dic["image"]);
            sendData.SetValue("person_id", dic["person_id"]);
            sendData.SetValue("tag", dic["tag"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.addFaceUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }

        /// <summary>
        /// 删除人脸
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData DelFace(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("person_id", dic["person_id"]);
            sendData.SetValue("face_ids	", dic["face_ids"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.delFaceUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData SetInfo(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("person_id", dic["person_id"]);
            sendData.SetValue("person_name	", dic["person_name	"]);
            sendData.SetValue("tag", dic["tag"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.setInfoeUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData GetInfo(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("person_id", dic["person_id"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.getInfoUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }

        /// <summary>
        /// 获取组列表
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData GetGroupids(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.getGroupidsUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }

        /// <summary>
        /// 获取个体列表	
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData GetPersonids(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("group_id", dic["group_id"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.getPersonidsUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 获取人脸列表	
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData GetFaceids(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("person_id", dic["person_id"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.newPersonFaceUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }
        /// <summary>
        /// 获取人脸信息
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static ParaData GetFaceinfo(Dictionary<string, string> dic)
        {
            var sendData = Init(dic);
            sendData.SetValue("face_id", dic["face_id"]);
            string sign = sendData.MakeSign(dic["key"]);
            sendData.SetValue("sign", sign);
            string postData = string.Join("&", sendData.GetValues().Select(x => x.Key.Trim() + "=" + ParaData.UrlEncode(x.Value.ToString())).ToArray());
            string json = ParaData.HttpPost(FaceUrl.newPersonFaceUrl, postData, Encoding.UTF8);
            return new ParaData(json);
        }

    }
}
