using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Webstore.Models;

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
                customer userInformation = (customer)Session["loggedIn"];
                ViewBag.username = userInformation.firstname;
                ViewBag.logLink = "<a href='../customer/logOut'>(log out?)</a>";
                ViewBag.optionLink = "<a href='../customer/orders' class='option-link'>Show my orders</a>";
                ViewBag.editInfoLink = "<a href='../customer/edit/" + "' class='option-link'>Edit my information</a>";
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

            var pr = db.getProduct(id);
            product.Data += pr.Id + ":" + pr.name + ":" + pr.price + ":";
                         
            return product;
        }
      
    }
}
