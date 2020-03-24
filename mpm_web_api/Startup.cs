using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using Swashbuckle.AspNetCore.Swagger;

namespace mpm_web_api
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            common.GetEnv();
            migration.Create();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // 添加文档信息
                c.SwaggerDoc("Common", new Info { Title = "公共配置接口", Version = "Common" });   //分组显示
                c.SwaggerDoc("OEE", new Info { Title = "OEE配置接口", Version = "OEE" });   //分组显示
                c.SwaggerDoc("Andon", new Info { Title = "Andon配置接口", Version = "Andon" });   //分组显示
                c.SwaggerDoc("WorkOrder", new Info { Title = "工单配置接口", Version = "WorkOrder" });   //分组显示
                c.SwaggerDoc("Dashboard", new Info { Title = "Dashboard数据源", Version = "Dashboard" });   //分组显示

                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "mpm_web_api.xml");
                c.IncludeXmlComments(xmlPath);
                //启用数据注解
                c.EnableAnnotations();

            });
            services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, Job>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseSwagger();
            // 配置SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Common/swagger.json", "公共配置接口");
                c.SwaggerEndpoint("/swagger/OEE/swagger.json", "OEE相关接口");
                c.SwaggerEndpoint("/swagger/Andon/swagger.json", "Andon配置接口");
                c.SwaggerEndpoint("/swagger/WorkOrder/swagger.json", "工单配置接口");
                c.SwaggerEndpoint("/swagger/Dashboard/swagger.json", "Dashboard数据源");
            });

            //异常处理中间件
            app.UseMiddleware(typeof(MiddleWare));
            app.UseMvc();
        }
    }
}
