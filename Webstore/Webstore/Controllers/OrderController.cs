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

            for (int i = 0; i <= values.Count() - 2; i += 2)
            {
                int productId = Convert.ToInt32(values[i]);
                int amount = Convert.ToInt32(values[i + 1]);
                products.Add(db.getProduct(productId), amount);
            }
            ViewBag.products = products;


            return View();
        }

        public ActionResult confirmOrder()
        {
            List<string> values = (List<string>)Session["orderInfo"];
            
            ViewBag.confirmOrderMessage = "You order is paid for!";

            order orderIn;
            if (Session["loggedIn"] != null)
            {
                var customer = (customer)Session["loggedIn"];
                orderIn = db.insertOrder(customer.Id);
                ViewBag.optionLink = "<a href='../customer/orders' class='option-link'>Show my orders</a>";
                ViewBag.customer = customer;
            }
            else
            {
                if (Session["anonymous"] != null)
                {
                    orderIn = db.insertOrder(DB.anonymousId);
                    ViewBag.anonymousInfo = (customer)Session["anonymous"];
                    
                }
                else
                {
                    return RedirectToAction("anonymousOrder");  
                }
            }
            
            for (int i = 0; i <= values.Count() - 2; i += 2)
            {
                int productId = Convert.ToInt32(values[i]);
                int amount = Convert.ToInt32(values[i + 1]);
                
                bool orderDetailIn = db.insertOrderDetails(orderIn.Id, productId, amount);
                if (!orderDetailIn)
                {
                    ViewBag.confirmOrderMessage = "Something went wrong, try placing your order later";
                }
               
            }  

            ViewBag.logLink = "<a href='../customer/logOut'>Log out</a>";
            ViewBag.backToStoreLink = "<a href ='../product/showallproducts'>Back to the store</a>";
            return View();

        }

        public ActionResult anonymousOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult anonymousOrder(customer customer)
        {
            Session["anonymous"] = customer;
            return RedirectToAction("confirmOrder");
        }

    }
}
