using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace BDCMicrroService
{
    /// <summary>
    /// 程序启动类
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 主函数
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 生成IWebHostBuilder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
             .ConfigureLogging((hostingContext, logging) =>
             {
                 logging.AddFilter("System", LogLevel.Warning);
                 logging.AddFilter("Microsoft", LogLevel.Warning);
                 logging.AddLog4Net();
             })
                .UseStartup<Startup>()
             .UseUrls("http://*:5000")
             .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory());
    }
}
