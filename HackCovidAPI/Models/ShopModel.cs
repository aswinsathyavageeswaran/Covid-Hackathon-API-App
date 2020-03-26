using HackCovidAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace FaceRecognitionAPI.Models
{
    public class ShopModel
    {
        public object _id { get; set; }
        public int ShopId { get; set; }
        public int TypeOfBusiness { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public Int64 DeliveryNumber { get; set; }
        public string WorkingHours { get; set; }
    }

    public class ShopContext
    {
        public const string collectionName = "ShopInfo";
        public bool ChangeShopStatus(int shopId, int status)
        {
            var success = false;
            try
            {
				var shopRec = DBService.NoSqldb.GetCollection<ShopModel>(collectionName);
				var filter = Builders<ShopModel>.Filter.Eq("ShopId", shopId);
				var update = Builders<ShopModel>.Update.Set("Status", status);
				shopRec.UpdateOne(filter, update);
				success = true;
            }
            catch { }
            return success;
        }
    }
}