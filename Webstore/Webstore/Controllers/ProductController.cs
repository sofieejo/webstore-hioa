using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Webstore.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        DB db = new DB();

        public PartialViewResult showAllProducts()
        {
            ViewData.Model = db.getAllProducts();

            if (Session["loggedIn"] != null)
            {
                Dictionary<int, List<string>> userInformation = (Dictionary<int, List<string>>)Session["loggedIn"];
                ViewBag.username = userInformation.First().Value.First();
                ViewBag.logLink = "<a href='../customer/logOut'>(log out?)</a>";
                ViewBag.optionLink = "<a href='../customer/orders' class='option-link'>Show my orders</a>";
            }
            else
            {
                ViewBag.logLink = "<a href='../customer/logIn'>(log in?)</a>";
                ViewBag.optionLink = "<a href='../customer/registerCustomer' class='option-link'>Become a customer</a>";
            }
            return PartialView();
        }

        public JsonResult get(int id)
        {
            JsonResult product = new JsonResult();
            product.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            foreach(var p in db.getProduct(id)){
                product.Data += p.Key + ": " + p.Value + ",";
            }
            return product;
        }
      
    }
}
