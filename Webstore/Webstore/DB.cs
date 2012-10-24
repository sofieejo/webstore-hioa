using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Data;
using Webstore.Models;

namespace Webstore
{
    public class DB
    {
        public byte[] generateHash(string password)
        {
            var algorithm = SHA256.Create();
            byte[] hashPassword = algorithm.ComputeHash(Encoding.ASCII.GetBytes(password));
            return hashPassword;
        }

        public string insertCustomer(Webstore.Models.customer customer)
        {
            try
            {
                using (var db = new DataClassesDataContext())
                {
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

    }
}