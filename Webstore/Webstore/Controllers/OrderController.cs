using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Webstore.Models;

namespace Webstore.Controllers
{
    public class OrderController : Controller
    {
        [HttpPost]
        public JsonResult newOrder(List<string> values)
        {
            return Json(new { Result = values[0]});
        }

        public ActionResult newOrder()
        {
            return View();
        }

    }
}
