using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace mpm_web_api.DB
{
    public class MongoHelper
    {
        public static string connectionstring = "";
        public static string databaseName = "";
        //public static string connectionstring = "mongodb://590f6990-f451-4266-a762-790b94b5cff4:AkoJVH1ysyuwaR97Dc6qWX7wv@42.159.119.214:27017/f76eca8b-4c83-4b07-8541-7efa3401ca3f";
        //public static string databaseName = "f76eca8b-4c83-4b07-8541-7efa3401ca3f";

        private readonly IMongoDatabase _database = null;
        public MongoHelper()
        {
            var client = new MongoClient(connectionstring);
            if (client != null)
            {
                _database = client.GetDatabase(databaseName);
            }
        }

        public MongoHelper(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            if (client != null)
            {
                _database = client.GetDatabase(databaseName);
            }
        }
        /// <summary>
        /// 根据查询条件，获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TableName">数据表名称</param>
        /// <param name="conditions">查询条件，查询条件编写按照Lamada表达式形式</param>
        /// <returns></returns>
        public List<T> GetList<T>(string TableName, Expression<Func<T, bool>> expression)
        {
            try
            {
                var collection = _database.GetCollection<T>(TableName);
                if (expression != null)
                {
                    return collection.Find(expression).ToList();
                }
                return collection.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //public void Where<T>(this Expression<Func<T, bool>> expression)
        //{

        //}

        /// <summary>
        /// 根据查询条件，获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TableName">数据表名称</param>
        /// <param name="Filter">查询条件</param>
        /// <param name="LimitNum">默认的限制条数，默认为100</param>
        /// <returns></returns>
        public List<T> GetList<T>(string TableName, FilterDefinition<T> Filter)
        {
            try
            {
                var collection = _database.GetCollection<T>(TableName);
                List<T> searchResult = collection.Find(Filter).ToList();//Limit(LimitNum)
                return searchResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public bool InsertOne<T>(T entity)
        {
            var collection = _database.GetCollection<T>("scada_HistRawData");
            collection.InsertOne(entity);
            return true;
        }
    }

    /// <summary>
    /// 云端的MongoDB数据库Tag类
    /// </summary>
    public class MongoDbTag
    {
        [BsonId]
        public ObjectId ID { get; set; }
        /// <summary>
        /// s云端为scada id 信息
        /// </summary>
        [BsonElement("s")]
        public string s { get; set; }
        /// <summary>
        /// t在云端为device/tag的组合
        /// </summary>
        [BsonElement("t")]
        public string t { get; set; }
        [BsonElement("v")]
        public int v { get; set; }
        [BsonElement("ts")]
        public DateTime ts { get; set; }


    }
}
