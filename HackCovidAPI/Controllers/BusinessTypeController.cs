using HackCovidAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HackCovidAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BusinessTypeController : ApiController
    {
        BusinessTypeContext businessTypeContext;
        public BusinessTypeController()
        {
            businessTypeContext = new BusinessTypeContext();
        }

        [HttpGet]
        [Route("covid/GetAllBusinessTypes")]
        public List<BusinessType> GetAllBusinessTypes()
        {
            return businessTypeContext.GetAllBusinessTypes();
        }

    }
}