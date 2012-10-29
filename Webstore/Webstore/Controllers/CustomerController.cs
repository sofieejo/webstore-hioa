using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using Webstore.Models;
using System.Data.Linq;

namespace Webstore.Controllers
{
    public class CustomerController : Controller
    {
        private DB db = new DB();

        public ActionResult registerCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult registerCustomer(Models.customer customer)
        {
            if (ModelState.IsValid) {
                var newCustomer = new Models.customer();
                newCustomer.firstname = customer.firstname;
                newCustomer.lastname = customer.lastname;
                newCustomer.address = customer.address;
                newCustomer.zipcode = customer.zipcode;
                newCustomer.telephone = customer.telephone;
                newCustomer.email = customer.email;
                newCustomer.password = db.generateHash(customer.password.ToString());

                ViewBag.registrationConfirmation = db.insertCustomer(newCustomer);
               
            }
            return View();
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
                ViewBag.logInMessage = "You are now logged in ";
                foreach (var property in userInformation.First().Value)
                {
                    ViewBag.logInMessage += property + " .";
                }
            }
            else
            {
                ViewBag.logInMessage = "You are not logged in.";
            }
            return View();
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
    }
}
