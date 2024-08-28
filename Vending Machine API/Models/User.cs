using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vending_Machine_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Deposit { get; set; } // in cents
        public bool Role { get; set; } // "seller: 0" or "buyer: 1"
    }

}