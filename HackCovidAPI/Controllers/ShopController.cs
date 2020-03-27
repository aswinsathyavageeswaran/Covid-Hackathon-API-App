using HackCovidAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FaceRecognitionAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ShopController : ApiController
    {
        ShopContext shopContext;
        public ShopController()
        {
            shopContext = new ShopContext();
        }

        [HttpPost]
        [Route("covid/shop/ChangeShopStatus")]
        public bool ChangeShopStatus(int shopId, int status)
        {
            return shopContext.ChangeShopStatus(shopId, status);
        }
    }
}