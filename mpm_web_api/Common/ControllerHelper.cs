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
        public string Get()
        {
            string str;
            try
            {
                List<T> lty = baseService.QueryableToList();
                string strJson = JsonConvert.SerializeObject(lty);
                str = common.ResponseStr((int)httpStatus.succes, "调用成功", strJson);
            }
            catch (Exception ex)
            {
                str = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            }
            return str;
        }

        public string Post(T obj)
        {
            string str;
            try
            {
                bool re = baseService.Insert(obj);
                if (re)
                    str = common.ResponseStr((int)httpStatus.succes, "调用成功");
                else
                    str = common.ResponseStr((int)httpStatus.dbError, "新增失败");
            }
            catch (Exception ex)
            {
                str = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            }
            return str;
        }


        public string Put(T obj)
        {
            string str;
            try
            {
                bool re = baseService.Update(obj, x => x.id == obj.id);
                if (re)
                    str = common.ResponseStr((int)httpStatus.succes, "调用成功");
                else
                    str = common.ResponseStr((int)httpStatus.dbError, "更新失败");
            }
            catch (Exception ex)
            {
                str = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            }
            return str;
        }


        public string Delete(int id)
        {
            string str;
            try
            {
                bool re = baseService.Delete(id);
                if (re)
                    str = common.ResponseStr((int)httpStatus.succes, "调用成功");
                else
                    str = common.ResponseStr((int)httpStatus.dbError, "删除失败");
            }
            catch (Exception ex)
            {
                str = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            }
            return str;
        }
    }
}
