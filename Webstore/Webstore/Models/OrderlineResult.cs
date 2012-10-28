using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webstore
{
    public class OrderlineResult
    {
        public DateTime date { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public double price { get; set; } 
    }
}