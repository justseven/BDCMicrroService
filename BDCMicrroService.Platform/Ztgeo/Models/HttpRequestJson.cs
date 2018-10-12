using System;
using System.Collections.Generic;
using System.Text;

namespace BDCMicrroService.Platform.Ztgeo.Models
{
    #region 发送平台HTTP请求模型
    internal class HttpRequestJson
    {
        public string data { get; set; }
        public PlatformToken token { get; set; }
        public string sign { get; set; }
    }

    internal class PlatformToken
    {
        public string userID { get; set; }
        public string apiID { get; set; }
        public string timestamp { get; set; }
    }
    #endregion

    #region 平台响应模型

    internal class HttpResponseJson
    {
        public string status { get; set; }
        public string message { get; set; }
        public PlatformData data { get; set; }
    }

    internal class PlatformData
    {
        public string data { get; set; }
        public string sign { get; set; }
    }

    internal class ReturnToClient
    {
        public string status { get; set; }
        public string message { get; set; }
        public string data { get; set; }
    }
    #endregion

    #region 平台发送请求模型

    internal class ReceiveJson
    {
        public string data { get; set; }
        public string sign { get; set; }
    }

    #endregion

    #region 响应平台请求模型

    internal class ReturnToServer
    {
        public string data { get; set; }
        public string sign { get; set; }
    }

    #endregion
}
