using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vending_Machine_API.Models;

namespace Vending_Machine_API.Data
{
    public class UserData
    {
        // Static list to simulate a persistent store of users
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Username = "JohnDoe", Role = true, Deposit = 100 },
            new User { Id = 2, Username = "JaneDoe", Role = false, Deposit = 0 },
            new User { Id = 3, Username = "SamSmith", Role = true, Deposit = 150 }
        };

        // Method to get all users
        public List<User> GetAllUsers()
        {
            return users;
        }

        // Method to find a user by ID
        public User GetUserById(int id)
        {
            return users.SingleOrDefault(u => u.Id == id);
        }

        // Method to update a user's deposit
        public void UpdateUserDeposit(int userId, int amount)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                user.Deposit += amount;
            }
        }
    }
}