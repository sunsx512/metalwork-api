using Microsoft.Extensions.Hosting;
using mpm_web_api.Common;
using mpm_web_api.model.m_common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class DockerLicenceService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await new TaskFactory().StartNew(() =>
                {
                    try
                    {
                        //Licence_Original lo = LicenceHelper.ReadLicence();
                        //if (lo != null)
                        //{
                        //    GlobalVar.authorized_number = lo.machineNum;
                        //}
                        if(DateTime.Now >= Convert.ToDateTime("2021-07-01 00:00:00"))
                        {
                            GlobalVar.authorized_number = 50;
                        }
                        else
                        {
                            GlobalVar.authorized_number = 1000;
                        }
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp);
                        //错误处理
                    }

                    //定时任务休眠
                    Thread.Sleep(10 * 1000);
                });
            }
        }
    }
}
