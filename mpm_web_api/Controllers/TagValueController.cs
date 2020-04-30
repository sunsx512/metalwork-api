using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using mpm_web_api.Common;
using mpm_web_api.DB;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers
{
    [ApiExplorerSettings(GroupName = "Common")]
    [Produces(("application/json"))]
    [Route("api/v1/external/tag_value")]
    [SwaggerTag("获取标签点的值")]
    [ApiController]
    public class TagValueController : Controller
    {
        MongoHelper mh = new MongoHelper();

        /// <summary>
        /// 获取标签点的值
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<MongoDbTag>> Get(string scada_id,string tag_name,DateTime start_time, DateTime end_time)
        {
            object obj;
            //云端环境
            if (GlobalVar.IsCloud)
            {
                var filterBuilder1 = Builders<CloudMongoDbTag>.Filter;
                var filter1 = filterBuilder1.And(filterBuilder1.Gt(x => x.ts, start_time),
                                                    filterBuilder1.Lte(x => x.ts, end_time),
                                                    filterBuilder1.Eq(x => x.t, tag_name));
                List<CloudMongoDbTag> list = mh.GetList<CloudMongoDbTag>("datahub_HistRawData_" + scada_id, filter1);
                List<MongoDbTag> list_mongo = new List<MongoDbTag>();
                foreach(CloudMongoDbTag cloudMongoDbTag in list)
                {
                    MongoDbTag mongoDbTag = new MongoDbTag();
                    mongoDbTag.ID = cloudMongoDbTag.ID;
                    mongoDbTag.s = scada_id;
                    mongoDbTag.t = cloudMongoDbTag.t;
                    mongoDbTag.ts = cloudMongoDbTag.ts;
                    mongoDbTag.v = cloudMongoDbTag.v;
                    list_mongo.Add(mongoDbTag);
                }
                obj = common.ResponseStr<MongoDbTag>((int)httpStatus.succes, "调用成功", list_mongo);
            }
            else
            {
                var filterBuilder1 = Builders<MongoDbTag>.Filter;
                var filter1 = filterBuilder1.And(filterBuilder1.Gt(x => x.ts, start_time),
                                                    filterBuilder1.Lte(x => x.ts, end_time),
                                                    filterBuilder1.Eq(x => x.s, scada_id),
                                                    filterBuilder1.Eq(x => x.t, tag_name));
                List<MongoDbTag> list = mh.GetList<MongoDbTag>("scada_HistRawData", filter1);
                obj = common.ResponseStr<MongoDbTag>((int)httpStatus.succes, "调用成功", list);
            }
            return Json(obj);
        }
    }
}