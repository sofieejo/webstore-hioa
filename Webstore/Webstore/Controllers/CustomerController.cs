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
        //
        // GET: /Customer/

        public ActionResult registerCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult registerCustomer(Models.customer customer)
        {
            if (ModelState.IsValid) {
                var db = new DB();
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


    }
}
