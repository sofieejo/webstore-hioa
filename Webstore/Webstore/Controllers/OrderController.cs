using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Webstore.Controllers
{
    public class OrderController : Controller
    {
        [HttpPost]
        public JsonResult newOrder(Object[][] somestuff)
        {
            JsonResult result = new JsonResult();

            if (somestuff != null)
            {
                for (int i = 0; i < somestuff.Length; i++)
                {
                    result.Data += somestuff[i] + " ";
                }
           
            }
            
            result.Data += "hei";
            return result;
        }

        public ActionResult newOrder()
        {
            return View();
        }

    }
}
