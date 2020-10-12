using mpm_web_api.Common;
using mpm_web_api.db;
using mpm_web_api.model.m_common;
using MySql.Data.MySqlClient.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        /// <returns></returns>
        public client QuerytoSingle()
        {
            //return DB.Queryable<client>().Where(x => x.serviceName == serviceName &&
            //                                x.cluster == cluster &&
            //                                x.workspace == workspace &&
            //                                x.@namespace == @namespace &&
            //                                x.datacenter == datacenter)?.First();
            return DB.Queryable<client>()?.First();
        }

        public client GetClient()
        {
            GlobalVar.client = QuerytoSingle();
            return GlobalVar.client;
        }
    }
}
