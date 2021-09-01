using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagment
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //多个中间件使用app.Use
            app.Use(async(context, next) => 
            {
                context.Response.ContentType = "text/plain;charset=utf-8";  //转中文
                logger.LogInformation("M1:传入请求");
                await context.Response.WriteAsync("第一个中间件");
                await next();   //进入下一个中间件，先入后出原则
                logger.LogInformation("M1:传出请求");
            });

            //终端中间件
            app.Run(async (context) =>
            {
                logger.LogInformation("M2:传入请求");
                //进程名
                //var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

                //var configVal = _configuration["MyKey"];

                await context.Response.WriteAsync("第二个中间件");
                logger.LogInformation("M2:传出请求");
            });
        }
    }
}
