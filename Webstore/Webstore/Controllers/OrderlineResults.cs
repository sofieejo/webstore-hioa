using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Webstore.Controllers
{
    class OrderlineResults
    {
        public DateTime date { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; } 
    }
}
