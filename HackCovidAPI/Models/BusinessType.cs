using HackCovidAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace FaceRecognitionAPI.Models
{
    public class BusinessType
    {
        public object _id { get; set; }
        public int TypeId { get; set; }
        public string Description{ get; set; }
    }

    public class BusinessTypeContext
    {
        public const string collectionName = "BusinessType";
        public List<BusinessType> GetAllBusinessTypes()
        {
           var businessList = new List<BusinessType>();
			var businessTypeRec = DBService.NoSqldb.GetCollection<BusinessType>(collectionName);
			businessList = businessTypeRec.AsQueryable<BusinessType>().ToList();
			return businessList;
        }
    }
}