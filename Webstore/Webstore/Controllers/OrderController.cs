using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webstore.Models;
using System.Collections;

namespace Webstore.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/

        public ActionResult showOrderDetails(Webstore.Models.customer customer)
        {
            customer.Id = 10;
            var db = new DataClassesDataContext();
            var orderline = from o in db.orders
                            join od in db.orderdetails on o.Id equals od.orderID
                            join p in db.products on od.productID equals p.Id
                            select new OrderlineResults{ date = o.date, name = p.name, description = p.description, 
                                                        quantity = od.quantity, price = p.price };

            //List<Models.order> orderList = orderline;
            //ViewData.Model = orderList;
            
            return View(orderline);

        }

    }
}
