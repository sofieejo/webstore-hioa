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
        [HttpPost]
        public ActionResult registerCustomer(FormCollection input)
        {
            try
            {
                var newCustomer = new Models.customer();
                newCustomer.firstname = input["firstname"];
                newCustomer.lastname = input["lastname"];
                newCustomer.address = input["address"];
                newCustomer.zipcode = input["zipcode"];
                newCustomer.telephone = input["telephone"];
                newCustomer.email = input.["email"];
                newCustomer.password = input["password"];
            }

            return View();
        }


    }
}
