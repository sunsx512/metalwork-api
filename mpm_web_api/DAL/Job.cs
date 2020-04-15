using Microsoft.Extensions.Hosting;
using mpm_web_api.model.m_oee;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class Job: BackgroundService
    {
        OnsiteMachineStatusService onsite = new OnsiteMachineStatusService();
        public static List<tricolor_tag_status> tricolor_Tag_Statuses = new List<tricolor_tag_status>();
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    await new TaskFactory().StartNew(() =>
            //    {
            //        try
            //        {
            //            tricolor_Tag_Statuses = onsite.QueryableDetailToList();
            //        }
            //        catch (Exception exp)
            //        {
            //            Console.WriteLine(exp);
            //            //错误处理
            //        }

            //        //定时任务休眠
            //        Thread.Sleep(1 * 1000);
            //    });
            //}

        }
    }
}
