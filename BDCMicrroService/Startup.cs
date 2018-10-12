using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using BDCMicrroService.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Steeltoe.Discovery.Client;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace BDCMicrroService
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup构造函数
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("autofac.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// Startup构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 依赖注入DI
        /// </summary>
        public IContainer Container { get; private set; }
        /// <summary>
        /// 依赖注入配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Eureka
            services.AddDiscoveryClient(Configuration);

            //添加Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "BDCMicrroServiceAPI", Version = "v1" });
                //Determine base path for the application.  
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //Set the comments path for the swagger json and ui.  
                var xmlPath = Path.Combine(basePath, "BDCMicrroService.xml");
                
                c.IncludeXmlComments(xmlPath);
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);

            var module = new ConfigurationModule(Configuration);
            builder.RegisterModule(module);
            this.Container = builder.Build();
            ServiceFactory.Current = this.Container;
            return new AutofacServiceProvider(this.Container);

        }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BDCMicrroServiceAPI V1");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           //Eureka
            app.UseDiscoveryClient();
            app.UseMvc();
        }
    }
}
