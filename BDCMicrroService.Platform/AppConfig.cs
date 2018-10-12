using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDCMicrroService.Platform
{
    public class AppConfig
    {
        #region 字段
       
        #endregion

        internal static IConfigurationRoot Configuration { get; set; }

       

        public static IConfigurationSection GetSection(string name)
        {
            return Configuration?.GetSection(name);
        }

      
    }
}
