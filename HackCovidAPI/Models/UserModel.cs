using HackCovidAPI.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackCovidAPI.Models
{
    public class UserModel
    {
        public object _id { get; set; }
        public int UserId { get; set; }
        public int UserType { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public Int64 Phone { get; set; }
        public string Email { get; set; }
    }

    public class UserDataContext
    {
        public const string collectionName = "UserData";
        public UserModel GetUserData(string email)
        {
            var userData = new UserModel();

            var userRec = DBService.NoSqldb.GetCollection<UserModel>(collectionName);
            var builder = Builders<UserModel>.Filter;
            var filter = builder.Eq("Email", email);// &builder.Eq("Password", password);
            var doc = userRec.Find(filter).FirstOrDefault();
            if (doc != null)
            {
                userData.UserId = doc.UserId;
                userData.UserType = doc.UserType;
                userData.UserName = doc.UserName;
            }
            return userData;
        }
    }
}