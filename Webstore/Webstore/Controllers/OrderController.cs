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
            Session["cart"] = values;
            return Json(new { Result = values});
        }

        public ActionResult newOrder()
        {
            return View();
        }

    }
}
