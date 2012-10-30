using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Data;
using Webstore.Models;
using System.Data.Linq;

namespace Webstore
{
    public class DB
    {
        public byte[] generateHash(string password)
        {
            var algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.ASCII.GetBytes(password));
        }

        public string insertCustomer(Webstore.Models.customer customer)
        {
            try
            {
                using (var db = new DataClassesDataContext())
                {
                    var zip = from z in db.cities
                              where z.zipcode == customer.zipcode
                              select z;

                    if (zip.Count() == 0)
                    {
                        return "This is not a norwegian zipcode";
                    }
                    db.customers.InsertOnSubmit(customer);
                    db.SubmitChanges();
                    return "You've been registered! You will now be redirected back to the store";
                }
            }
            catch
            {
                return "Something went wrong. Try again later.";
            }
        }

        public Dictionary<int, List<string>> LogIn(string email, string password)
        {
            byte[] hashedPassword = generateHash(password);

            using (var db = new DataClassesDataContext())
            {

                var user = from c in db.customers
                           where email == c.email
                           select c;


                if (user.Count() > 0)
                {
                    Dictionary<int, List<string>> customer = new Dictionary<int, List<string>>();
                    List<string> properties = new List<string>();

                    properties.Add(user.First().firstname);
                    properties.Add(user.First().lastname);
                    properties.Add(user.First().email);
                    customer.Add(user.First().Id, properties);

                    return customer;
                }
                else
                {
                    return null;
                }
            }
        }

        
        private List<EntitySet<orderdetail>> getCustomerOrders(int customerId)
        {
            List<EntitySet<orderdetail>> orders = new List<EntitySet<orderdetail>>();

            using (var db = new DataClassesDataContext())
            {
                var orderList = from o in db.orders
                                where customerId == o.customerID
                                select o.orderdetails;

                foreach (var item in orderList)
                {
                    orders.Add(item);
                }

            }

            return orders;
        }

        public Dictionary<int, Dictionary<string, string>> getCustomerOrderDetails(int customerId)
        {
           
            Dictionary<int, Dictionary<string, string>> orderDetailList = new Dictionary<int, Dictionary<string, string>>();

            using(var db = new DataClassesDataContext())
            {
                
                decimal priceTotal;
                foreach (var item in getCustomerOrders(customerId))
                {
                    
                    foreach (var property in item)
                    {
                        Dictionary<string, string> details = new Dictionary<string, string>();
                        var product = from p in db.products
                                          where property.productID == p.Id
                                          select new {p.name, p.price};

                        var date = from o in db.orders
                                   where o.Id == property.orderID
                                   select new { o.date };

                        priceTotal = product.First().price * property.quantity;
                        details.Add("productName", product.First().name);
                        details.Add("priceTotal", Convert.ToString(priceTotal));
                        details.Add("date", Convert.ToString(date.First().date));

                        orderDetailList.Add(property.Id, details);
                    }
               
                }
                return orderDetailList;
            }
            
        }



        public object product { get; set; }
    }
}