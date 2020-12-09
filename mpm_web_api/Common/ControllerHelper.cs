using mpm_web_api.DAL;
using mpm_web_api.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.Common
{
    public class ControllerHelper<T> where T : base_model, new()
    {
        BaseService<T> baseService = new BaseService<T>();
        public object Get()
        {
            object obj;
            List<T> lty = baseService.QueryableToList();
            lty = lty.OrderBy(x => x.id).ToList();
            string strJson = JsonConvert.SerializeObject(lty);
            obj = common.ResponseStr<T>((int)httpStatus.succes, "调用成功", lty);
            return obj;
        }

        public object Post(T obj)
        {
            object str;
            bool re = baseService.Insert(obj);
            if (re)
                str = common.ResponseStr((int)httpStatus.succes, "调用成功");
            else
                str = common.ResponseStr((int)httpStatus.dbError, "新增失败");
            return str;
        }
        public object Put(T obj)
        {
            object ob;
            bool re = baseService.Update(obj, x => x.id == obj.id);
            if (re)
                ob = common.ResponseStr((int)httpStatus.succes, "调用成功");
            else
                ob = common.ResponseStr((int)httpStatus.dbError, "更新失败");
            return ob;
        }

        public object Delete(int id)
        {
            object obj;
            bool re = baseService.Delete(id);
            if (re)
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
            else
                obj = common.ResponseStr((int)httpStatus.dbError, "删除失败");
            return obj;
        }
    }
}
