using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Tentcent.Ai.Sdk;

namespace Wings.worker.Framework.Common.Service
{
    public class AIService
    {
        public static int APP_ID { get; } = 1106550426;
        public static string APP_KEY { get; } = "00WdwrKA54aXkVG6";

        public static ParaData commonOcr(string base64)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("image", base64);
            dict.Add("app_id", APP_ID.ToString());
            dict.Add("key", APP_KEY);
            return OcrServer.Gen(dict);
        }
    }
}
