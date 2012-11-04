using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Webstore.Models;

namespace Webstore.Controllers
{
    public class OrderController : Controller
    {
        private DB db = new DB();

        [HttpPost]
        public JsonResult newOrder(List<string> values)
        {
            JsonResult results = new JsonResult();
            results.Data = values;
            Session["orderInfo"] = values;
            return results;
        }

        public ActionResult showOrder()
        {
            Dictionary<product, int> products = new Dictionary<product, int>();
            List<string> values = (List<string>)Session["orderInfo"];

            for (int i = 0; i < values.Count(); i += 2)
            {
                products.Add(db.getProduct(Convert.ToInt32(values[i])), Convert.ToInt32(values[i + 1]));
            }
            ViewBag.products = products;


            return View();
        }

    }
}
