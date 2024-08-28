using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vending_Machine_API.Models;
using Vending_Machine_API.Data;

namespace Vending_Machine_API.Controllers
{
    public class ProductsController : Controller
    {
        private ProductData productData = new ProductData();
        private UserData userData = new UserData();

        // GET: Products
        public ActionResult Index(int? sellerId)
        {
            var products = productData.GetAllProducts().Where(p => p.SellerId == sellerId || sellerId == null);
            ViewBag.SellerId = sellerId;
            return View(products);
        }

        // GET: Products/Details/id
        public ActionResult Details(int id)
        {
            var product = productData.GetAllProducts().SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // GET: Products/Create
        public ActionResult Create(int sellerId)
        {
            ViewBag.SellerId = sellerId;
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductName, AmountAvailable, Cost")] Product product, int sellerId)
        {
            if (ModelState.IsValid)
            {
                product.SellerId = sellerId;
                productData.AddProduct(product);
                return RedirectToAction("Index", new { sellerId = sellerId });
            }

            ViewBag.SellerId = sellerId;
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var product = productData.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, ProductName, AmountAvailable, Cost, SellerId")] Product product)
        {
            if (ModelState.IsValid)
            {
                productData.UpdateProduct(product);
                return RedirectToAction("Index", new { sellerId = product.SellerId });
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            var product = productData.GetProductById(id);   
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int sellerId)
        {
            var product = productData.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            productData.DeleteProduct(id);
            return RedirectToAction("Index", new { sellerId = sellerId });
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Purchase(int userId)
        {
            var availableProducts = productData.GetAllProducts().Where(p => p.AmountAvailable > 0).ToList();
            ViewBag.UserId = userId;
            return View(availableProducts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchase(int productId, int quantity, int userId)
        {
            var product = productData.GetProductById(productId);
            var user = userData.GetUserById(userId);

            if (product == null || user == null)
            {
                return HttpNotFound();
            }

            int totalCost = product.Cost * quantity;

            // Check if the user has enough deposit
            if (user.Deposit < totalCost)
            {
                ModelState.AddModelError("", "Insufficient deposit. Please deposit more money.");
            }

            // Check if the product has enough available quantity
            if (product.AmountAvailable < quantity)
            {
                ModelState.AddModelError("", "Not enough product available. Please reduce the quantity or choose another product.");
            }

            if (!ModelState.IsValid)
            {
                // Reload the products and return to the view with errors
                var availableProducts = productData.GetAllProducts().Where(p => p.AmountAvailable > 0).ToList();
                ViewBag.UserId = userId; // Ensure userId is passed back to the view
                return View("Purchase", availableProducts);
            }

            // Deduct the total cost from the user's deposit
            userData.UpdateUserDeposit(userId, -totalCost);

            // Reduce the product's available amount
            productData.UpdateProductAmount(productId, product.AmountAvailable - quantity);

            ViewBag.Message = $"Successfully purchased {quantity} unit(s) of {product.ProductName}. Your new balance is {user.Deposit} cents.";
            return RedirectToAction("PurchaseConfirmation", new { productId = product.Id, quantity = quantity, userId = userId });
        }



        // GET: Products/PurchaseConfirmation
        public ActionResult PurchaseConfirmation(int productId, int quantity, int userId)
        {
            var product = productData.GetProductById(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProductName = product.ProductName;
            ViewBag.Quantity = quantity;
            ViewBag.TotalCost = product.Cost * quantity;    
            ViewBag.UserId = userId; 

            return View();
        }
    }
}