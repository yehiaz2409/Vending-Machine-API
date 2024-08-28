using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vending_Machine_API.Models;
using Vending_Machine_API.Data;

namespace Vending_Machine_API.Controllers
{
    public class UsersController : Controller
    {
        // Static list to simulate a persistent store
        private UserData userData = new UserData();

        // GET: Users
        public ActionResult Index()
        {
            var users = userData.GetAllUsers();
            return View(users);
        }

        // GET: Users/Details/id
        public ActionResult Details(int id)
        {
            var user = userData.GetAllUsers().SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }
        // GET: Users/Deposit/id
        public ActionResult Deposit(int id)
        {
            var user = userData.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users/Deposit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit(int id, int amount)
        {
            var user = userData.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (!new int[] { 5, 10, 20, 50, 100 }.Contains(amount))
            {
                ModelState.AddModelError("", "Invalid deposit amount. Please deposit 5, 10, 20, 50, or 100 cents.");
                return View(user);
            }

            userData.UpdateUserDeposit(id, amount); // Update the user's deposit

            ViewBag.Message = $"Successfully deposited {amount} cents. Your new balance is {user.Deposit} cents.";

            return View(user); // Return to the same view to show the updated balance
        }
    }
}