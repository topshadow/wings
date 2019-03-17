using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tentcent.Ai.Sdk
{
    public class FaceUrl
    {
        /// <summary>
        /// 人脸检测与分析
        /// </summary>
        public static string detectFaceUrl = "https://api.ai.qq.com/fcgi-bin/face/face_detectface";
        /// <summary>
        /// 多人脸检测
        /// </summary>
        public static string detectMultiFaceUrl = "https://api.ai.qq.com/fcgi-bin/face/face_detectmultiface";
        /// <summary>
        /// 人脸对比
        /// </summary>
        public static string faceCompareUrl = "https://api.ai.qq.com/fcgi-bin/face/face_facecompare";
        /// <summary>
        /// 跨年龄人脸识别
        /// </summary>
        public static string detectCrossAgeUrl = "https://api.ai.qq.com/fcgi-bin/face/face_detectcrossageface";
        /// <summary>
        /// 五官定位
        /// </summary>
        public static string faceShapeUrl = "https://api.ai.qq.com/fcgi-bin/face/face_faceshape";
        /// <summary>
        /// 人脸识别
        /// </summary>
        public static string faceIdentifyUrl = "https://api.ai.qq.com/fcgi-bin/face/face_faceidentify";
        /// <summary>
        /// 人脸验证
        /// </summary>
        public static string faceVerifyUrl = "https://api.ai.qq.com/fcgi-bin/face/face_faceverify";
        /// <summary>
        /// 个体创建
        /// </summary>
        public static string newPersonFaceUrl = "https://api.ai.qq.com/fcgi-bin/face/face_newperson";
        /// <summary>
        /// 删除个体
        /// </summary>
        public static string delPersonUrl = "https://api.ai.qq.com/fcgi-bin/face/face_delperson";
        /// <summary>
        /// 增加人脸
        /// </summary>
        public static string addFaceUrl = "https://api.ai.qq.com/fcgi-bin/face/face_addface";
        /// <summary>
        /// 删除人脸
        /// </summary>
        public static string delFaceUrl = "https://api.ai.qq.com/fcgi-bin/face/face_delface";
        /// <summary>
        /// 设置信息
        /// </summary>
        public static string setInfoeUrl = "https://api.ai.qq.com/fcgi-bin/face/face_setinfo";
        /// <summary>
        /// 获取信息	
        /// </summary>
        public static string getInfoUrl = "https://api.ai.qq.com/fcgi-bin/face/face_getinfo";
        /// <summary>
        /// 获取组列表
        /// </summary>
        public static string getGroupidsUrl = "https://api.ai.qq.com/fcgi-bin/face/face_getgroupids";
        /// <summary>
        /// 获取个体列表
        /// </summary>
        public static string getPersonidsUrl = "https://api.ai.qq.com/fcgi-bin/face/face_getpersonids";
        /// <summary>
        /// 获取人脸列表	
        /// </summary>
        public static string getFaceidsUrl = "https://api.ai.qq.com/fcgi-bin/face/face_getfaceids";
        /// <summary>
        /// 获取人脸信息
        /// </summary>
        public static string getFaceInfoUrl = "https://api.ai.qq.com/fcgi-bin/face/face_getfaceinfo";
    }
}
