using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using SqlSugar;

namespace mpm_web_api.db
{
    /// <summary>
    /// 数据库连接属性
    /// </summary>
    public class DbConnectionString
    {
        public string DbType { set; get; }
        public string Database { set; get; }
        public string Port { set; get; }
        public string Host { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
    }

    public abstract class PostgreBase
    {
        //public static string connString = "";
        //public static string connString = "Server=42.159.86.191;Port=5432;Database=b1d520bd-cebc-4839-9a75-ec6b7709029d;User Id=af5c2b50-621d-4e7e-8e69-c16c964fe3c0;Password=emhhcsu8odva3s9auujb7nloi6;";
        public static string connString = "Server=42.159.86.191;Port=5432;Database=372484b9-76a7-459e-94b3-ed9e12986881;User Id=8826c350-2c45-4ba1-bdfd-5b2dad14d67c;Password=pkn754frevr1ick8vt20pjo6i0;";

        //public static string connString = "";

    }
    public class SqlSugarBase
    {
        public static string DB_ConnectionString { get; set; }

        public static SqlSugarClient DB
        {
            get => new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = PostgreBase.connString,
                DbType = SqlSugar.DbType.PostgreSQL,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                IsShardSameThread = true
            }
            );
        }
    }

}
