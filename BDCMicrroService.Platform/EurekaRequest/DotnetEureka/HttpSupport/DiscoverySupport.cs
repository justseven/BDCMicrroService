#region Copyright 2018 D-Haven.org

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using BDCMicrroService.Platform.Utilitys.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using System;
using System.Text.RegularExpressions;


namespace DotnetEureka.HttpSupport
{
    /// <summary>
    /// Manage the HttpClient for hte application.  NOTE: HttpClient is designed to be a singleton even though it implements IDisposable.
    /// See https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/ for more details
    /// </summary>
    public static class DiscoverySupport
    {
       
        static DiscoverySupport()
        {

            Configuration = Configuration==null?ConfigFactory.GetConfiguration(): Configuration;

            var logFactory = new LoggerFactory();
           
            LogFactory = logFactory;

            string url = Configuration["eureka:client:serviceUrl"];

            discoveryClient = new DiscoveryClient(new EurekaClientConfig
            {
                EurekaServerServiceUrls = url,
                ProxyHost = url,
                ProxyPort = GetPort(url),
            });

            var factory = new DiscoveryClientFactory(new DiscoveryOptions(Configuration));
            var handler = new DiscoveryHttpClientHandler(factory.CreateClient() as IDiscoveryClient, logFactory.CreateLogger<DiscoveryHttpClientHandler>());
            Client = new HttpClientWrapper(handler);
        }

        private static int GetPort(string str)
        {
            Regex reg = new Regex(@":[0-9]\d*");
            MatchCollection result = reg.Matches(str);
            if (null != result && result.Count > 0)
            {
                string port = result[0].ToString().Replace(":", "");
                return Convert.ToInt32(port);
            }
            return -1;
        }

        internal static IConfiguration Configuration { get; }

        internal static ILoggerFactory LogFactory { get; }

        public static IHttpClient Client { get; set; }

        public static DiscoveryClient discoveryClient { get; set; }
    }
}
