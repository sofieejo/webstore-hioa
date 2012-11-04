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

        public ActionResult confirmOrder()
        {
            List<string> values = (List<string>)Session["orderInfo"];
            var customer = (customer)Session["loggedIn"];

            order orderIn = db.insertOrder(customer.Id);
            ViewBag.confirmOrderMessage = "You order is paid for!";
            for (int i = 0; i < values.Count(); i += 2){
                var orderdIn = db.insertOrderDetails(orderIn.Id, Convert.ToInt32(values[i]), Convert.ToInt32(values[i + 1]));
                if (!orderdIn)
                {
                    ViewBag.confirmOrderMessage = "Something went wrong, try placing your order later";
                }
            }

            ViewBag.logLink = "<a href='../customer/logOut'>(log out?)</a>";

            if(Session["loggedIn"] != null){
                ViewBag.optionLink = "<a href='../customer/orders' class='option-link'>Show my orders</a>";
            }

            ViewBag.backToStoreLink = "<a href ='../product/showallproduts'>Back to the store</a>";
            return View();

        }

    }
}
