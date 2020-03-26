using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HackCovidAPI.Services
{
    public class DBService 
    {

        private static IMongoDatabase db;
        public static IMongoDatabase NoSqldb
        {
            get
            {
                if (db == null)
                {
                    var datasource = ConfigurationManager.AppSettings["DataSource"];
                    var mongoUrl = ConfigurationManager.AppSettings["MongoUrl"];
                    MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(mongoUrl));
                    MongoClient client = new MongoClient(settings);
                    db = client.GetDatabase(datasource);
                }

                return db;
            }
        }
    }
}