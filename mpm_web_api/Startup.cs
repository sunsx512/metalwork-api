
using System;
using System.IO;
using System.Threading;
using Advantech.Ensaas;
using Advantech.Ensaas.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.db;
using mpm_web_api.DB;

namespace mpm_web_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            EnvironmentInfo environmentInfo = EnsaasEnvironment.Get();

            GlobalVar.mqtthost = Environment.GetEnvironmentVariable("MQTT_HOST");
            GlobalVar.mqttport = Convert.ToInt32(Environment.GetEnvironmentVariable("MQTT_PORT"));
            GlobalVar.mqttuser = Environment.GetEnvironmentVariable("MQTT_USER");
            GlobalVar.mqttpwd = Environment.GetEnvironmentVariable("MQTT_PWD");
            GlobalVar.mqtttopic = Environment.GetEnvironmentVariable("MQTT_TOPIC");

            PostgreBase.connString = environmentInfo.postgres_connection;
            GlobalVar.module = Environment.GetEnvironmentVariable("module");
            //EnSaaS 4.0 环境
            if (environmentInfo.iscloud)
            {
                GlobalVar.time_zone = Convert.ToDouble(Environment.GetEnvironmentVariable("db_time_zone"));
                MongoHelper.connectionstring = environmentInfo.mongo_connection;
                MongoHelper.databaseName = environmentInfo.mongo_database;
                GlobalVar.IsCloud = true;
                //创建数据表
                migration.Create(PostgreBase.connString);
                //开启4.0 Licence认证
                EnsaasLicenceService els = new EnsaasLicenceService();
                CancellationToken token = new CancellationToken();
                els.StartAsync(token);
            }
            //docker 环境
            else
            {
                GlobalVar.time_zone = Convert.ToDouble(Environment.GetEnvironmentVariable("time_zone"));
                MongoHelper.connectionstring = environmentInfo.mongo_connection + "?authSource=admin";
                MongoHelper.databaseName = environmentInfo.mongo_database;
                GlobalVar.IsCloud = false;
                //migration.Create();
                //开启docker Licence认证
                DockerLicenceService dls = new DockerLicenceService();
                CancellationToken token = new CancellationToken();
                dls.StartAsync(token);
            }
            //800所专用
            //PostgreBase.connString = Configuration.GetValue<string>("pgconnectionString");
            //GlobalVar.time_zone = Configuration.GetValue<double>("time_zone");
            //MongoHelper.connectionstring = Configuration.GetValue<string>("mgconnectionString");
            //MongoHelper.databaseName = Configuration.GetValue<string>("mgdatabaseName");
            //GlobalVar.IsCloud = false;
            ////migration.Create();
            ////开启docker Licence认证
            //DockerLicenceService dls = new DockerLicenceService();
            //CancellationToken token = new CancellationToken();
            //dls.StartAsync(token);

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // 添加文档信息
                c.SwaggerDoc("Common", new OpenApiInfo { Title = "公共配置接口", Version = "Common" });   //分组显示
                c.SwaggerDoc("OEE", new OpenApiInfo { Title = "OEE配置接口", Version = "OEE" });   //分组显示
                c.SwaggerDoc("Andon", new OpenApiInfo { Title = "Andon配置接口", Version = "Andon" });   //分组显示
                c.SwaggerDoc("WorkOrder", new OpenApiInfo { Title = "工单配置接口", Version = "WorkOrder" });   //分组显示
                c.SwaggerDoc("Notice", new OpenApiInfo { Title = "微信/邮件通知", Version = "Notice" });   //分组显示
                //c.SwaggerDoc("EHS", new OpenApiInfo { Title = "环境健康管理", Version = "EHS" });   //分组显示
                //c.SwaggerDoc("LPM", new OpenApiInfo { Title = "人员绩效管理", Version = "LPM" });   //分组显示
                //c.SwaggerDoc("Notice", new OpenApiInfo { Title = "通知管理", Version = "Notice" });   //分组显示
                //c.SwaggerDoc("Dashboard", new OpenApiInfo { Title = "Dashboard数据源", Version = "Dashboard" });   //分组显示

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入带有Bearer的Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                    //In = "header",
                    //Type = "apiKey"
                }) ;
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {  new OpenApiSecurityScheme
                   {
                    Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                   },
                    new string[] { }
                }
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "mpm_web_api.xml");              
                c.IncludeXmlComments(xmlPath);
                //启用数据注解
                c.EnableAnnotations();
                

            });
            //services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, Job>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseSwagger();
            // 配置SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Common/swagger.json", "公共配置接口");
                if (GlobalVar.module == "ALL")
                {
                    c.SwaggerEndpoint("/swagger/OEE/swagger.json", "OEE相关接口");
                    c.SwaggerEndpoint("/swagger/Andon/swagger.json", "Andon配置接口");
                    c.SwaggerEndpoint("/swagger/WorkOrder/swagger.json", "工单配置接口"); 
                    c.SwaggerEndpoint("/swagger/Notice/swagger.json", "工单配置接口"); 
                    //c.SwaggerEndpoint("/swagger/EHS/swagger.json", "环境健康管理");
                    //c.SwaggerEndpoint("/swagger/LPM/swagger.json", "人员绩效管理");
                    //c.SwaggerEndpoint("/swagger/Notice/swagger.json", "通知管理");
                    //c.SwaggerEndpoint("/swagger/Dashboard/swagger.json", "Dashboard数据源");
                }
                else if(GlobalVar.module == "OEE")
                {
                    c.SwaggerEndpoint("/swagger/OEE/swagger.json", "OEE相关接口");
                }
                else if (GlobalVar.module == "Andon")
                {
                    c.SwaggerEndpoint("/swagger/Andon/swagger.json", "Andon配置接口");
                }
                else if (GlobalVar.module == "WorkOrder")
                {
                    c.SwaggerEndpoint("/swagger/WorkOrder/swagger.json", "工单配置接口");
                }
                else
                {
                    c.SwaggerEndpoint("/swagger/OEE/swagger.json", "OEE相关接口");
                    c.SwaggerEndpoint("/swagger/Andon/swagger.json", "Andon配置接口");
                    c.SwaggerEndpoint("/swagger/WorkOrder/swagger.json", "工单配置接口");
                    //c.SwaggerEndpoint("/swagger/EHS/swagger.json", "环境健康管理");
                    //c.SwaggerEndpoint("/swagger/LPM/swagger.json", "人员绩效管理");
                    //c.SwaggerEndpoint("/swagger/Dashboard/swagger.json", "Dashboard数据源");
                }
            });
            //if (GlobalVar.IsCloud)
                //如果是云端的话 需要启动权限处理中间件
                //app.UseMiddleware(typeof(AuthMiddleWare));
            //异常处理中间件
            app.UseMiddleware(typeof(MiddleWare));
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
