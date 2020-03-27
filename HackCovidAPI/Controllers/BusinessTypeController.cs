using System.Web.Http;
using System.Web.Http.Cors;
using HackCovidAPI.Services;

namespace HackCovidAPI.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class BusinessTypeController : ApiController
	{
		private IDBService DBService;
		public BusinessTypeController(IDBService IDBService)
		{
			DBService = IDBService;
		}

		[HttpGet]
		[Route("covid/GetAllBusinessTypes")]
		public dynamic GetAllBusinessTypes()
		{
			return DBService.GetBussinessTypeCollection();
		}

	}
}