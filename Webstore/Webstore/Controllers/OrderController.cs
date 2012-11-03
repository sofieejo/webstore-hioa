using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Webstore.Models;

namespace Webstore.Controllers
{
    public class OrderController : Controller
    {
        [HttpPost]
        public JsonResult newOrder(ShoppingCart cart)
        {
            JsonResult result = new JsonResult();

            result.Data += cart;
                       
            result.Data += "hei";
            return result;
        }

        public ActionResult newOrder()
        {
            return View();
        }

    }
}
