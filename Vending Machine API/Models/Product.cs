using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vending_Machine_API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int AmountAvailable { get; set; }
        public int Cost { get; set; } // in cents
        public int SellerId { get; set; }
    }

}