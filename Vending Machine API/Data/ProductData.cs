using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vending_Machine_API.Models;

namespace Vending_Machine_API.Data
{
    public class ProductData
    {
        // Static list to simulate a persistent store of products
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, ProductName = "Coca Cola", AmountAvailable = 10, Cost = 50, SellerId = 2 },
            new Product { Id = 2, ProductName = "Pepsi", AmountAvailable = 15, Cost = 45, SellerId = 2 },
            new Product { Id = 3, ProductName = "Water Bottle", AmountAvailable = 20, Cost = 30, SellerId = 2 }
        };

        // Method to get all products
        public List<Product> GetAllProducts()
        {
            return products;
        }

        // Method to find a product by ID
        public Product GetProductById(int id)
        {
            return products.SingleOrDefault(p => p.Id == id);
        }

        // Method to update product availability after a purchase
        public bool PurchaseProduct(int productId, int quantity)
        {
            var product = GetProductById(productId);
            if (product != null && product.AmountAvailable >= quantity)
            {
                product.AmountAvailable -= quantity;
                return true;
            }
            return false;
        }

        // Method to add a new product
        public void AddProduct(Product product)
        {
            // Generate a new ID for the product
            int newId = products.Max(p => p.Id) + 1;
            product.Id = newId;

            // Add the product to the list
            products.Add(product);
        }

        // Method to update an existing product
        public void UpdateProduct(Product product)
        {
            var existingProduct = GetProductById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.AmountAvailable = product.AmountAvailable;
                existingProduct.Cost = product.Cost;
            }
        }

        // Method to delete a product by ID
        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                products.Remove(product);
            }
        }
        public void UpdateProductAmount(int productId, int newAmount)
        {
            var product = GetProductById(productId);
            if (product != null)
            {
                product.AmountAvailable = newAmount;
            }
        }
    }
}