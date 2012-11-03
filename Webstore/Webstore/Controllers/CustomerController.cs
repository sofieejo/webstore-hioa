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
        public ActionResult logIn(FormCollection form, Models.customer customer)
        {
            
            string password = form["pass"];
            

            Session["loggedIn"] = db.LogIn(customer.email, password);

            customer userInformation = (customer)Session["loggedIn"];
            if ( userInformation != null)
            {
                ViewBag.logInMessage = "You are now logged in " + userInformation.firstname + ".";
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
            customer userInformation = (customer)Session["loggedIn"];
            ViewBag.text = "";
            
            if (userInformation != null)
            {
                try
                {
                    Dictionary<int, Dictionary<string,string>> orders = db.getCustomerOrderDetails(userInformation.Id);

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

        public ActionResult edit()
        {
            customer userInformation = (customer)Session["loggedIn"]; 
            ViewBag.username = userInformation.firstname;
            ViewBag.logLink = "<a href='../customer/logOut'>(log out?)</a>";
            ViewBag.optionLink = "<a href='../customer/orders' class='option-link'>Show my orders</a>";
            ViewBag.editInfoLink = "<a href='../customer/edit/" + userInformation.Id + "' class='option-link'>Edit my information</a>";
            

            var model = new customer
            {
                firstname = userInformation.firstname,
                lastname = userInformation.lastname,
                address = userInformation.address,
                telephone = userInformation.telephone,
                zipcode = userInformation.zipcode,
                email = userInformation.email
            };
            
            return View(model);
        }

        [HttpPost]
        public ActionResult edit(Models.customer customer, FormCollection form)
        {
            customer userInformation = (customer)Session["loggedIn"];
            ViewBag.username = userInformation.firstname;
            ViewBag.logLink = "<a href='../customer/logOut'>(log out?)</a>";
            ViewBag.optionLink = "<a href='../customer/orders' class='option-link'>Show my orders</a>";
            ViewBag.editInfoLink = "<a href='../customer/edit/" + userInformation.Id + "' class='option-link'>Edit my information</a>";
            ViewBag.backToStoreLink = "<a href='../product/showallproducts'>Back to the store</a>";

            ViewBag.confirmation = db.editCustomer(userInformation.Id, customer.firstname, customer.lastname, customer.address, customer.zipcode, customer.telephone, customer.email);
            return View();
        }

        private bool passwordOK(string psw)
        {
            return psw != null && psw.Length < 50;
        }

        public ActionResult logOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("showallproducts", "product", null);
        }

    }
}
