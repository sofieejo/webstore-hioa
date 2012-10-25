using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

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

            if (db.doLogIn(email, password))
            {
                Session["currentUser"] = true;
                ViewBag.logInMessage = "You are now logged inn,";
            }
            else
            {
                ViewBag.logInMessage = "Fail";
            }

            return View();
        }

    }
}
