using mpm_web_api.Common;
using mpm_web_api.db;
using mpm_web_api.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class BaseService<T>: SqlSugarBase where T : class, new()
    {
        /// <summary>
        /// 获取表内所有数据
        /// </summary>
        /// <returns></returns>
        public List<T> QueryableToList() 
        {
            return DB.Queryable<T>().ToList();
        }
        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public List<T> QueryableToList(Expression<Func<T, bool>> expression) 
        {
            return DB.Queryable<T>().Where(expression).ToList();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Update(T entity, Expression<Func<T, bool>> expression) 
        {
            //foreach (PropertyInfo p in entity.GetType().GetProperties())
            //{
            //    if (p.PropertyType.Name == "DateTime")
            //    {
            //        DateTime dt = ((DateTime)p.GetValue(entity)).AddHours(GlobalVar.time_zone);
            //        p.SetValue(p, dt);
            //    }
            //}
            return DB.Updateable(entity).Where(expression).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommand() > 0;

        }


        /// <summary>
        /// 按id删除一行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return DB.Deleteable<T>(id).ExecuteCommand() > 0;
        }
        /// <summary>
        /// 新增一行数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Insert(T t)
        {
            //foreach (PropertyInfo p in t.GetType().GetProperties())
            //{
            //    if (p.PropertyType.Name == "DateTime")
            //    {
            //        DateTime dt = ((DateTime)p.GetValue(t)).AddHours(GlobalVar.time_zone);
            //        p.SetValue(p, dt);
            //    }
            //}
            return DB.Insertable(t).ExecuteCommandIdentityIntoEntity();
        }


    }
}
