using mpm_web_api.Common;
using mpm_web_api.db;
using mpm_web_api.model.m_common;
using MySql.Data.MySqlClient.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wise_Paas.models;
using Wise_Pass;
using Client = Wise_Paas.models.Client;

namespace mpm_web_api.DAL
{
    public class ClientService: SqlSugarBase
    {
        /// <summary>
        /// 保存或者新增数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Save(client entity)
        {
           return DB.Saveable(entity).ExecuteCommand() > 0;
        }
        /// <summary>
        /// 查询已存放的client信息
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="cluster"></param>
        /// <param name="workspace"></param>
        /// <param name="namespace"></param>
        /// <param name="datacenter"></param>
        /// <returns></returns>
        public client QuerytoSingle(string serviceName,string cluster,string workspace,string @namespace,string datacenter)
        {
            return DB.Queryable<client>().Where(x => x.serviceName == serviceName &&
                                            x.cluster == cluster &&
                                            x.workspace == workspace &&
                                            x.@namespace == @namespace &&
                                            x.datacenter == datacenter)?.First();
        }
        //public static client register()
        //{
        //    EnvironmentInfo environmentInfo = EnvironmentVariable.Get();
        //    Client client = SSO.CreateClient("", "Metalworks");           
        //    //创建失败 原因:应该是已存在
        //    if (client == null)
        //    {
        //        GlobalVar.client = QuerytoSingle("Metalworks", environmentInfo.cluster, environmentInfo.workspace, environmentInfo.@namespace, environmentInfo.datacenter);
        //    }
        //    else
        //    {
        //        GlobalVar.client = common.AutoCopy<client, Client>(client);
        //        Save(GlobalVar.client);
        //    }

        //    return GlobalVar.client;
        //}

        public client GetClient()
        {
            EnvironmentInfo environmentInfo = EnvironmentVariable.Get();
            GlobalVar.client = QuerytoSingle("Metalworks", environmentInfo.cluster, environmentInfo.workspace, environmentInfo.@namespace, environmentInfo.datacenter);
            return GlobalVar.client;
        }
    }
}
