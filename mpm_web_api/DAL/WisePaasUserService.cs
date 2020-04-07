using mpm_web_api.db;
using mpm_web_api.model.m_common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class WisePaasUserService: SqlSugarBase
    {
        BaseService<wise_paas_user> baseService = new BaseService<wise_paas_user>();
        public bool InsertInfo(wise_paas_user t)
        {
            //权限字符串卡关
            if (t.role != "Editor" && t.role != "Viewer")
                return false;
            else
                return DB.Saveable<wise_paas_user>(t).ExecuteCommand() > 0;
        }

        public bool DeleteUser(string user)
        {
            return DB.Deleteable<wise_paas_user>(user).ExecuteCommand() > 0;
        }

        public bool UpdateUser(wise_paas_user t)
        {
            //权限字符串卡关
            if (t.role != "Editor" && t.role != "Viewer")
                return false;
            else
                return DB.Updateable<wise_paas_user>(t).IgnoreColumns(ignoreAllNullColumns:true).ExecuteCommand() > 0;
        }

        public object GetUser()
        {
            object obj;
            List<wise_paas_user> lty = baseService.QueryableToList();
            if (lty.Count > 0)
            {
                foreach (wise_paas_user ob in lty)
                {
                    ob.password = "";
                }
            }
            obj = common.ResponseStr((int)httpStatus.succes, "调用成功", lty);
            return obj;
        }

        public object Check(string user, string password)
        {
            List<wise_paas_user> list = DB.Queryable<wise_paas_user>().ToList();
            List<wise_paas_user> wpus = new List<wise_paas_user>();
            if (list.Count > 0)
            {
                wpus = list.Where(x => x.name == user && x.password == password).ToList();
                if(wpus.Count> 0)
                {
                    object obj = common.ResponseStr((int)httpStatus.succes, "调用成功", wpus);
                    return obj;
                }
            }
            return common.ResponseStr(401, "No Authority");



        }
    }
}
