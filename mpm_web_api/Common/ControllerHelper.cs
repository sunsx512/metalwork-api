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
            //try
            //{
                List<T> lty = baseService.QueryableToList();
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<T>((int)httpStatus.succes, "调用成功", lty);
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
            return obj;
        }

        public object Post(T obj)
        {
            object str;
            //try
            //{
                bool re = baseService.Insert(obj);
                if (re)
                    str = common.ResponseStr((int)httpStatus.succes, "调用成功");
                else
                    str = common.ResponseStr((int)httpStatus.dbError, "新增失败");
            //}
            //catch (Exception ex)
            //{
            //    str = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
            return str;
        }


        public object Put(T obj)
        {
            object ob;
            //try
            //{
                bool re = baseService.Update(obj, x => x.id == obj.id);
                if (re)
                    ob = common.ResponseStr((int)httpStatus.succes, "调用成功");
                else
                    ob = common.ResponseStr((int)httpStatus.dbError, "更新失败");
            //}
            //catch (Exception ex)
            //{
            //    ob = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
            return ob;
        }


        public object Delete(int id)
        {
            object obj;
            //try
            //{
                bool re = baseService.Delete(id);
                if (re)
                    obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                else
                    obj = common.ResponseStr((int)httpStatus.dbError, "删除失败");
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
            return obj;
        }
    }
}
