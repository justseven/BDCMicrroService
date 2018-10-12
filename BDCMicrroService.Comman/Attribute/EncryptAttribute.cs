using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BDCMicrroService.Comman.Attribute
{
    public class EncryptAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IList<ParameterDescriptor> pds = filterContext.ActionDescriptor.Parameters;//.GetParameters();
            IDictionary<string, object> paramBase = filterContext.ActionArguments;

            foreach (var pd in pds)
            {
                if(paramBase.Keys.Contains(pd.Name))
                {
                    string key = pd.Name;
                    object value = GetMD5Hash(paramBase[pd.Name].ToString());
                    paramBase.Remove(pd.Name);
                    paramBase.Add(key, value);
                }
                
            }

            base.OnActionExecuting(filterContext);

    }
        private static string GetMD5Hash(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                //string→byte[]
                MD5 md5Hash = new MD5CryptoServiceProvider();
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in data)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
                return stringBuilder.ToString();
            }
            return string.Empty;
        }
    }
}
