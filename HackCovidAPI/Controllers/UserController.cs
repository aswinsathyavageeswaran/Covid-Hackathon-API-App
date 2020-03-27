using System.Web.Http;
using System.Web.Http.Cors;
using HackCovidAPI.Models;
using HackCovidAPI.Services;

namespace HackCovidAPI.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class UserController : ApiController
	{
		private IDBService DBService;
		public UserController(IDBService IDBService)
		{
			DBService = IDBService;
		}

		[HttpGet]
		[Route("covid/user/GetUserData")]
		public dynamic GetUserData(string email)
		{
			return DBService.GetUserData(email);
		}

		[HttpPost]
		[Route("covid/user/RegisterUser")]
		public int RegisterUser(RegistrationModel registrationDetails)
		{
			return DBService.RegisterUser(registrationDetails);
		}
	}
}