using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using Webstore.Models;
using System.Data.Linq;
using System.Threading;

namespace Webstore
{
    public class CustomerController : Controller
    {
        private DB db = new DB();

        public ActionResult registerCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult registerCustomer(Models.customer customer, FormCollection collection)
        {
            if(ModelState.IsValid){
                string pass = collection["pass"];
                        
                if (passwordOK(pass))
                {
                    customer.password = db.generateHash(pass);
                    ViewBag.registrationConfirmation = db.insertCustomer(customer);
                    Thread.Sleep(1000);
                    return RedirectToAction("logIn");
                }
                else
                {
                    ViewBag.registrationConfirmation = "You are not registered.";
                    return View();
                }
            }
            else { return View(); }
                            
        }

        public ActionResult logIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult logIn(FormCollection form)
        {
            var email = form["email"];
            var password = form["password"];

            Session["loggedIn"] = db.LogIn(email, password);

            Dictionary<int, List<string>> userInformation = (Dictionary<int, List<string>>)Session["loggedIn"];
            if ( userInformation != null)
            {
                ViewBag.logInMessage = "You are now logged in " + userInformation.First().Value + ".";
                System.Threading.Thread.Sleep(2000);
                return RedirectToAction("showallproducts", "product",null);
            }
            else
            {
                ViewBag.logInMessage = "You are not logged in.";
                return View();
            }
            
        }

        public ActionResult orders()
        {
            Dictionary<int, List<string>> userInformation = (Dictionary<int, List<string>>)Session["loggedIn"];
            ViewBag.text = "";
            
            if (userInformation != null)
            {
                try
                {
                    Dictionary<int, Dictionary<string,string>> orders = db.getCustomerOrderDetails(userInformation.First().Key);

                    foreach (var item in orders)
                    {
                        ViewBag.text += "orderdetailId >> " + item.Key;
                        foreach (var detail in item.Value)
                        {
                            ViewBag.text += detail.Key + " --- " + detail.Value;
                        }
                    }
                }
                catch (ObjectDisposedException o)
                {
                    Console.Write(o.Message);
                }
            }
            else
            {
                ViewBag.errorMessage = "You must log in to view orders.";
            }

            return View();


        }

        private bool passwordOK(string psw)
        {
            return psw != null && psw.Length < 50;
        }
    }
}
