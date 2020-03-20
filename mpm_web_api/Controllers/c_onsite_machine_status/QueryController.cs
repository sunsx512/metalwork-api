using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.model.m_oee;
using Newtonsoft.Json;
using OnsiteStatusWorker.Models;

namespace OnsiteStatusWorker.Controllers
{
    [ApiExplorerSettings(GroupName = "Dashboard")]
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] QueryRequest.request request)
        {
            //query后dashboard返回的结果
            QueryRequest.request requests = request;

            List<QueryRequest.content> contents =request.targets;
            List<object> objectList = new List<object>();
            for (int i=0;i<contents.Count;i++) {
                for (int j=0;j< Job.tricolor_Tag_Statuses.Count;j++) {
                    if (contents[i].target == Job.tricolor_Tag_Statuses[j].machine_name) {
                        int status = 0;
                        QueryResponse.Timeseries obj = new QueryResponse.Timeseries();
                        obj.target = Job.tricolor_Tag_Statuses[j].machine_name;//webaccess中某一Tag点
                        List<double> objdouble = new List<double>();
                        switch (Job.tricolor_Tag_Statuses[j].status_name) {
                            case "run": status=1; break;
                            case "error ": status = 2; break;
                            case "idle": status = 3; break;
                            case "off": status = 4; break;
                            case "Linechange ": status =5; break;
                            default: status = 6; break;
                        }

                        objdouble.Add(status);//
                        objdouble.Add(QueryResponse.GetTimeStamp(Job.tricolor_Tag_Statuses[j].insert_time));

                        List<List<double>> objDoubleList = new List<List<double>>();

                        objDoubleList.Add(objdouble);
                        obj.datapoints = objDoubleList;
                        objectList.Add(obj);
                        return JsonConvert.SerializeObject(objectList);
                    }
                }
            }
            return JsonConvert.SerializeObject(objectList);
        }
    }
}