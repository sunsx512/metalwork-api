using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using Newtonsoft.Json;
using OnsiteStatusWorker.Models;

namespace OnsiteStatusWorker.Controllers
{
    [ApiExplorerSettings(GroupName = "Dashboard")]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        
        [HttpPost]
        public ActionResult<string> Post()
        {
            
            List<string> machineList = new List<string>();

            for (int i=0;i< Job.tricolor_Tag_Statuses.Count; i++) {
                machineList.Add(Job.tricolor_Tag_Statuses[i].machine_name);
            }

            return JsonConvert.SerializeObject(machineList);
        }
    }
}