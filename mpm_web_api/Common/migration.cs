using mpm_web_api.db;
using mpm_web_api.model.m_common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.Common
{
    public class migration: SqlSugarBase
    {
        public static bool Create()
        {
            List<migration_log> migration_Logs = new List<migration_log>();
            DirectoryInfo root = new DirectoryInfo("sql");
            FileInfo[] fileInfos = root.GetFiles();
            migration_Logs = Query();
            foreach (FileInfo fileInfo in fileInfos)
            {
                // 查看日志 如果没有更新 则需要更新

                if (migration_Logs == null || !migration_Logs.Exists(x => x.migration_id == fileInfo.Name))
                {
                    string text = File.ReadAllText("sql/" + fileInfo.Name);
                    string[] tp = text.Split(';');
                    foreach (string str in tp)
                    {
                        CreateOne(str.Replace("\n", "").Replace("\t", "").Replace("\r", ""));
                    }
                    string cmd = string.Format("INSERT INTO fimp.migration_log( migration_id) VALUES ('{0}')", fileInfo.Name);
                    CreateOne(cmd);
                }

            }
            return true;
        }

        public static bool CreateOne(string  cmd)
        {
            try
            {
                DB.SqlQueryable<migration_log>(cmd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<migration_log> Query()
        {
            try
            {
                List<migration_log> list = DB.Queryable<migration_log>().ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
