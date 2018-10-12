using Microsoft.Extensions.Configuration;
using System.IO;

namespace BDCMicrroService.Platform.Utilitys.Configuration
{
    public class ConfigFactory
    {
        private static IConfiguration Configuration;
        public static IConfiguration GetConfiguration()
        {
            if (null == Configuration)
            {
                CreateConfig();
            }
            return Configuration;
        }

        private static void CreateConfig()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();

        }
    }
}
