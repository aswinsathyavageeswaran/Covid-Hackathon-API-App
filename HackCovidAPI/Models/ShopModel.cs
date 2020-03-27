using HackCovidAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace HackCovidAPI.Models
{
	public class ShopModel
	{
        public ObjectId _id { get; set; }
        public string ShopId { get; set; }
        public int TypeOfBusiness { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public Int64 DeliveryNumber { get; set; }
        public string WorkingHours { get; set; }
        [Required]        
        public string ShopName { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public Int64 Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}