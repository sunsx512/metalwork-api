using mpm_web_api.db;
using mpm_web_api.model.m_common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace mpm_web_api.Common
{
    public class migration: SqlSugarBase
    {
        public static bool Create(string connectionString)
        {
            List<migration_log> migration_Logs = new List<migration_log>();
            DirectoryInfo root = new DirectoryInfo("sql");
            FileInfo[] fileInfos = root.GetFiles();
            migration_Logs = Query();
            foreach (FileInfo fileInfo in fileInfos)
            {
                // 查看日志 如果没有更新 则需要更新
                if (migration_Logs == null || !migration_Logs.Exists(x => x.migration_version == fileInfo.Name))
                {
                    CreateTable(connectionString, "mpm_web_api.model");
                    CreateSchema(connectionString,new List<string>() { "common","oee","andon","work_order","ehs" });
                    string text = "";
                    if (GlobalVar.IsCloud)
                        text = File.ReadAllText("sql\\" + fileInfo.Name);
                    else
                        text = File.ReadAllText("sql/" + fileInfo.Name);
                    //string[] tp = text.Split(';');
                    //foreach (string str in tp)
                    //{
                    //    CreateOne(str.Replace("\n", "").Replace("\t", "").Replace("\r", ""));
                    //}
                    CreateOne(text);
                    string cmd = string.Format("INSERT INTO common.migration_log(migration_version) VALUES ('{0}')", fileInfo.Name);
                    CreateOne(cmd);
                    //dockr版本需要自己新建账密
                    if (!GlobalVar.IsCloud)
                        InsertAdmin();
                }

            }
            

            return true;
        }

        public static bool CreateOne(string  cmd)
        {
            try
            {
                DB.Ado.ExecuteCommand(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(cmd + "：" + ex.Message);
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



        /// <summary>
        /// 新建初始账密
        /// </summary>
        /// <returns></returns>
        public static bool InsertAdmin()
        {
            try
            {
                wise_paas_user wise_Paas_User = new wise_paas_user();
                wise_Paas_User.name = "admin";
                wise_Paas_User.password = "admin";
                wise_Paas_User.role = "Admin";
                return DB.Saveable<wise_paas_user>(wise_Paas_User).ExecuteCommand() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static void CreateSchema(string connectionString, List<string> schemas)
        {
            try
            {
                ISqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = connectionString,
                    DbType = SqlSugar.DbType.PostgreSQL,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute,
                    IsShardSameThread = true
                });
                foreach (string schema in schemas)
                {
                    db.Ado.ExecuteCommand("CREATE SCHEMA " + schema);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("创建SCHEMA失败");
            }
        }

        /// <summary>
        /// 自动创建表格
        /// </summary>
        /// <param name="configuration">配置</param>
        /// <param name="entity_assembly">实体类所在的程序集</param>
        public static void CreateTable(string connectionString, string entity_assembly)
        {
            ISqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = SqlSugar.DbType.PostgreSQL,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                IsShardSameThread = true
            });

            //外键字典
            Dictionary<string, string> fkey_dic = new Dictionary<string, string>();
            //获取所有实体类
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            //Type[] types = Assembly.Load(entity_assembly).GetTypes();
            //创建基础表格
            for (int i = 0; i < types.Length; i++)
            {
                if (!types[i].FullName.Contains("mpm_web_api.model"))
                    continue;
                try
                {
                    db.CodeFirst.InitTables(types[i]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("创建表格" + types[i].FullName + "失败");
                }
            }

        }
    }
}
