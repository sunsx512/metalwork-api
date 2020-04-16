using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
using mpm_web_api.db;
using mpm_web_api.DB;
using Swashbuckle.AspNetCore.Swagger;
using Wise_Paas.models;
using Wise_Pass;

namespace mpm_web_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            EnvironmentInfo environmentInfo = EnvironmentVariable.Get();
            string pg = "Server={0};Port={1};Database={2};User Id={3};Password={4};";
            pg = string.Format(pg, environmentInfo.postgres_host, environmentInfo.postgres_port, environmentInfo.postgres_database, environmentInfo.postgres_username, environmentInfo.postgres_password);
            PostgreBase.connString = pg;

            //EnSaaS 4.0 环境
            if (environmentInfo.cluster != null)
            {
                string mg = "mongodb://{0}:{1}@{2}:{3}/{4}";
                mg = string.Format(mg, environmentInfo.mongo_username, environmentInfo.mongo_password, environmentInfo.mongo_host, environmentInfo.mongo_port, environmentInfo.mongo_database);
                MongoHelper.connectionstring = mg;
                MongoHelper.databaseName = environmentInfo.mongo_database;
                GlobalVar.IsCloud = true;
                migration.Create(false);
                //开启4.0 Licence认证
                EnsaasLicenceService els = new EnsaasLicenceService();
                CancellationToken token = new CancellationToken();
                els.StartAsync(token);
            }
            //docker 环境
            else
            {
                string mg = "mongodb://{0}:{1}@{2}:{3}/{4}";
                mg = string.Format(mg, environmentInfo.mongo_username, environmentInfo.mongo_password, environmentInfo.mongo_host, environmentInfo.mongo_port, environmentInfo.mongo_database);
                MongoHelper.connectionstring = mg + "?authSource=admin";
                MongoHelper.databaseName = environmentInfo.mongo_database;
                GlobalVar.IsCloud = false;
                migration.Create(false);
            }
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

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "请输入带有Bearer的Token",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", Enumerable.Empty<string>() }
                });

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
            if (GlobalVar.IsCloud)
                //如果是云端的话 需要启动权限处理中间件
                app.UseMiddleware(typeof(AuthMiddleWare));
            //异常处理中间件
            app.UseMiddleware(typeof(MiddleWare));
            app.UseMvc();
        }
    }
}
