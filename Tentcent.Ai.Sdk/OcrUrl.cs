using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tentcent.Ai.Sdk
{
    public class OcrUrl
    {
        /// <summary>
        /// 身份证OCR
        /// </summary>
        public static string idCardUrl = "https://api.ai.qq.com/fcgi-bin/ocr/ocr_idcardocr";

        /// <summary>
        /// 名片OCR
        /// </summary>
        public static string nameCardUrl = "https://api.ai.qq.com/fcgi-bin/ocr/ocr_bcocr";

        /// <summary>
        /// 行驶证驾驶证OCR
        /// </summary>
        public static string driveUrl = "https://api.ai.qq.com/fcgi-bin/ocr/ocr_driverlicenseocr";


        /// <summary>
        /// 车牌OCR
        /// </summary>
        public static string carUrl = "https://api.ai.qq.com/fcgi-bin/ocr/ocr_plateocr";


        /// <summary>
        /// 营业执照OCR
        /// </summary>
        public static string bizUrl = "https://api.ai.qq.com/fcgi-bin/ocr/ocr_bizlicenseocr";


        /// <summary>
        /// 银行卡OCR
        /// </summary>
        public static string bankUrl = "https://api.ai.qq.com/fcgi-bin/ocr/ocr_creditcardocr";


        /// <summary>
        /// 通用OCR
        /// </summary>
        public static string genUrl = "https://api.ai.qq.com/fcgi-bin/ocr/ocr_generalocr";

        /// <summary>
        /// 手写体OCR
        /// </summary>
        public static string handUrl = "https://api.ai.qq.com/fcgi-bin/ocr/ocr_handwritingocr";
    }
}
