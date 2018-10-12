using BDCMicrroService.Platform.Ztgeo.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BDCMicrroService.Platform.Ztgeo.Aop;
using BDCMicrroService.Platform.Ztgeo.Models;
using Microsoft.AspNetCore.Http;

namespace BDCMicrroService.Platform.Ztgeo
{
    /// <summary>
    /// 平台客户端工具类
    /// </summary>
    public class ZtgeoClient
    {
        #region 协议级请求参数

        private string serverUrl;              //平台url
        private string apiID;                  //接口id
        private string userID;                 //用户id
        //private string timestamp;              //时间戳
        //private string token;                  //平台鉴权口令（SDK负责管理）
        //private string sign;                   //数字签名
        //private string data;                   //业务参数数据
        private string charset = "utf-8";      //字符集
        private string privateKeyPem;          //客户端RSA私钥
        private string signType = "RSA";       //签名类型
        private string platformPublicKey;      //平台RSA公钥（SDK负责管理）
        private string encyptKey;              //AES加密密钥
        private string encyptType = "AES";     //加密类型

        private WebUtils webUtils;

        #endregion

        #region 构造器

        public ZtgeoClient(string serverUrl, string apiID, string userID, string platformPublicKey,
            string privateKeyPem, string encyptKey)
        {
            this.serverUrl = serverUrl;
            this.apiID = apiID;
            this.userID = userID;
            this.platformPublicKey = platformPublicKey;
            this.privateKeyPem = privateKeyPem;
            this.encyptKey = encyptKey;
            this.webUtils = new WebUtils();
        }

        public void SetTimeout(int timeout)
        {
            webUtils.Timeout = timeout;
        }

        #endregion

        #region 客户端操作

        //执行平台请求，并获取平台响应
        public string RequestToServer(IDictionary<string, object> dictionary = null)
        {
            try
            {
                //去除空字典项，并检查字典合法性
                IDictionary<string, object> newDictionary = null;
                if (dictionary != null)
                {
                    newDictionary = AopUtils.CleanupDictionary(dictionary);
                }
                // 添加协议级请求参数
                HttpRequestJson httpRequestJson = new HttpRequestJson
                {
                    token = new PlatformToken
                    {
                        apiID = apiID,
                        userID = userID,
                        timestamp = AopUtils.GetTimestamp(DateTime.Now).ToString()
                    }
                };
                string httpJsonStr = JsonConvert.SerializeObject(httpRequestJson);
                if (newDictionary != null && newDictionary.Count > 0)
                {
                    //构造业务参数json
                    string dataJson = BuildBizJson(newDictionary);
                    //数据加签RSA
                    var tokenStr = JsonConvert.SerializeObject(httpRequestJson.token);
                    string sign = Signature.RSASignCharSetXML(dataJson + tokenStr, privateKeyPem, charset, false);
                    //数据加密AES
                    string dataEncrypt = Encrypt.AesEncrypt(encyptKey, dataJson, charset);
                    httpRequestJson.data = dataEncrypt;
                    httpRequestJson.sign = sign;
                    httpJsonStr = JsonConvert.SerializeObject(httpRequestJson);
                }
                string result = Excute(httpJsonStr);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string RequestToServer(object jsonObj = null)
        {
            try
            {
                // 添加协议级请求参数
                HttpRequestJson httpRequestJson = new HttpRequestJson
                {
                    token = new PlatformToken
                    {
                        apiID = apiID,
                        userID = userID,
                        timestamp = AopUtils.GetTimestamp(DateTime.Now).ToString()
                    }
                };
                string httpJsonStr = JsonConvert.SerializeObject(httpRequestJson);
                if (jsonObj != null)
                {
                    //构造业务参数json
                    string dataJson = JsonConvert.SerializeObject(jsonObj);
                    //数据加签RSA
                    var tokenStr = JsonConvert.SerializeObject(httpRequestJson.token);
                    string sign = Signature.RSASignCharSetXML(dataJson + tokenStr, privateKeyPem, charset, false);
                    //数据加密AES
                    string dataEncrypt = Encrypt.AesEncrypt(encyptKey, dataJson, charset);
                    httpRequestJson.data = dataEncrypt;
                    httpRequestJson.sign = sign;
                    httpJsonStr = JsonConvert.SerializeObject(httpRequestJson);
                }
                string result = Excute(httpJsonStr);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //接收平台请求
        public string ReceiveFromServer(HttpRequest httpRequest)
        {
            try
            {
                string receiveData = webUtils.GetRequestAsString(httpRequest, Encoding.GetEncoding(charset));
                ReceiveJson receiveJson = JsonConvert.DeserializeObject<ReceiveJson>(receiveData);
                //数据解密
                string dataDecrypt = Encrypt.AesDencrypt(encyptKey, receiveJson.data, charset);
                //数据验签
                bool checkSignResult = Signature.RSASignCheck(dataDecrypt, receiveJson.sign,
                    platformPublicKey, charset);
                if (checkSignResult)
                {
                    return dataDecrypt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //响应平台请求
        public void ResponseToServer(HttpResponse resp, IDictionary<string, object> dictionary = null)
        {
            try
            {
                //去除空字典项，并检查字典合法性
                IDictionary<string, object> newDictionary = null;
                if (dictionary != null)
                {
                    newDictionary = AopUtils.CleanupDictionary(dictionary);
                }
                ReturnToServer returnToServer = new ReturnToServer();
                string httpJsonStr = JsonConvert.SerializeObject(returnToServer);
                if (newDictionary != null && newDictionary.Count > 0)
                {
                    //构造业务参数json
                    string dataJson = BuildBizJson(newDictionary);
                    //数据加签RSA
                    string sign = Signature.RSASignCharSetXML(dataJson, privateKeyPem, charset, false);
                    //数据加密AES
                    string dataEncrypt = Encrypt.AesEncrypt(encyptKey, dataJson, charset);
                    returnToServer.data = dataEncrypt;
                    returnToServer.sign = sign;
                    httpJsonStr = JsonConvert.SerializeObject(returnToServer);
                }
                //写入响应流
                webUtils.WriteResponse(resp, httpJsonStr, Encoding.GetEncoding(charset));
            }
            catch (Exception ex)
            {
                throw new AopException("响应失败，请检查业务参数");
            }
        }

        public void ResponseToServer(HttpResponse resp, object jsonObj = null)
        {
            try
            {
                ReturnToServer returnToServer = new ReturnToServer();
                string httpJsonStr = JsonConvert.SerializeObject(returnToServer);
                if (jsonObj != null)
                {
                    //构造业务参数json
                    string dataJson = JsonConvert.SerializeObject(jsonObj);
                    //数据加签RSA
                    string sign = Signature.RSASignCharSetXML(dataJson, privateKeyPem, charset, false);
                    //数据加密AES
                    string dataEncrypt = Encrypt.AesEncrypt(encyptKey, dataJson, charset);
                    returnToServer.data = dataEncrypt;
                    returnToServer.sign = sign;
                    httpJsonStr = JsonConvert.SerializeObject(returnToServer);
                }
                //写入响应流
                webUtils.WriteResponse(resp, httpJsonStr, Encoding.GetEncoding(charset));
            }
            catch (Exception ex)
            {
                throw new AopException("响应失败，请检查业务参数");
            }
        }

        #endregion

        private string Excute(string httpJsonStr)
        {
            //发送平台请求
            string httpResponseJson = webUtils.DoPost(serverUrl, httpJsonStr, charset);
            //平台响应数据解密AES
            var respJsonObj = JsonConvert.DeserializeObject<HttpResponseJson>(httpResponseJson);
            if (respJsonObj.status == "200")   //请求成功
            {
                //平台响应数据解密
                string dataDecrypt = Encrypt.AesDencrypt(encyptKey, respJsonObj.data.data, charset);
                //平台响应数据验签
                bool checkSignResult = Signature.RSASignCheck(dataDecrypt, respJsonObj.data.sign,
                    platformPublicKey, charset);
                if (checkSignResult)  //验签成功
                {
                    return dataDecrypt;
                }
                else   //验签失败
                {
                    ReturnToClient re = new ReturnToClient
                    {
                        status = "500",
                        message = "数据不合法",
                    };
                    return JsonConvert.SerializeObject(re);
                }
            }
            else   //请求失败
            {
                ReturnToClient re = new ReturnToClient
                {
                    status = respJsonObj.status,
                    message = respJsonObj.message
                };
                return JsonConvert.SerializeObject(re);
            }
        }

        // 根据字典构造json对象
        private string BuildBizJson(IDictionary<string, object> dic)
        {
            JObject json = new JObject();
            //文本参数构建
            foreach (var item in dic)
            {
                if (item.Value is string)
                {
                    json.Add(item.Key, item.Value as string);
                }
                else if (item.Value is int || item.Value is float || item.Value is double
                    || item.Value is bool || item.Value is DateTime)
                {
                    json.Add(item.Key, item.Value.ToString());
                }
                else if (item.Value is FileItem)
                {
                    //文件转base64
                    var file = item.Value as FileItem;
                    var base64 = Convert.ToBase64String(file.GetContent());
                    json.Add(item.Key, base64);
                }
            }
            return json.ToString();
        }
    }
}
