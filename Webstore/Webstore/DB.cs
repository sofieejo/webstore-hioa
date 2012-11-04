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
        public static readonly int anonymousId = 1;
        private DataClassesDataContext db = new DataClassesDataContext();

        public byte[] generateHash(string password)
        {
            if (password != null)
            {
                var algorithm = SHA256.Create();
                return algorithm.ComputeHash(Encoding.ASCII.GetBytes(password));
            }
            else
                return null;
        }

        public string insertCustomer(Webstore.Models.customer customer)
        {
            try
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
            catch
            {
                return "Something went wrong. Try again later.";
            }
        }

        public customer LogIn(string email, string password)
        {
            byte[] hashedPassword = generateHash(password);

            var user = (from c in db.customers
                           where email == c.email && hashedPassword == c.password
                           select c).Single();


            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        
        }

        public string editCustomer(int id, string firstname, string lastname, string address, string zipcode, string phonenumber, string email)
        {
            var cust = db.customers.Single(c => c.Id == id);
            cust.firstname = firstname;
            cust.lastname = lastname;
            cust.address = address;
            cust.zipcode = zipcode;
            cust.telephone = phonenumber;
            cust.email = email;

            try
            {
                db.SubmitChanges();
                return "Info updated";
            }
            catch
            {
                return "Info was not updated";
            }
        }

        public customer getCustomer(int id)
        {
            var m = db.customers.Where(c => id == c.Id).Single();
            return m;
        }

        private List<order> getCustomerOrders(int customerId)
        {
            var orderList = (from o in db.orders
                                where customerId == o.customerID
                                select o).ToList();

            return orderList;
        }

        public Dictionary<order, List<orderdetail>> getCustomerOrderDetails(int customerId)
        {
           
            Dictionary<order, List<orderdetail>> orderDetailList = new Dictionary<order, List<orderdetail>>();

            foreach (var item in getCustomerOrders(customerId))
            {
                    orderDetailList.Add(item, item.orderdetails.ToList()); 
            }
            return orderDetailList;
        }

        public order insertOrder(int customerId)
        {
            order o = new order
            {
                date = DateTime.Now,
                customerID = customerId
            };
            db.orders.InsertOnSubmit(o);
            try
            {
                db.SubmitChanges();
                return o;
            }
            catch
            {
                return null;
            }

        }

        public bool insertOrderDetails(int orderId, int pId, int amount)
        {
            orderdetail od = new orderdetail { 
                orderID = orderId,
                productID = pId,
                quantity = amount
            };

            db.orderdetails.InsertOnSubmit(od);
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
           
        }
        public List<Models.product> getAllProducts()
        {
            return db.products.ToList();
        }

        public Dictionary<int, string> getAllCategories()
        {
            Dictionary<int, string> categories = new Dictionary<int, string>();

            var cat = from c in db.categories
                      select c;

            foreach (var c in cat)
            {
                categories.Add(c.Id, c.name);
            }
            return categories;
        }

        public product getProduct(int id)
        {
           return db.products.Where(p => id == p.Id).Single();
        }
    }
}