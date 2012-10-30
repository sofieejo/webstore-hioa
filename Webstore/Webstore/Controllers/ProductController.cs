using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webstore.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public PartialViewResult showAllProducts()
        {
            var db = new Models.DataClassesDataContext();
            List<Models.product> productList = db.products.ToList();
            ViewData.Model = productList;

            return PartialView("showAllProducts");
        }



    }
}
