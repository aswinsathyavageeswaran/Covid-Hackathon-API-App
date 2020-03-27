using System;

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
}