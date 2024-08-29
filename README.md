# Vending Machine API

## Overview

This project is a simple Vending Machine API built using ASP.NET MVC. The application allows users with different roles (`seller` and `buyer`) to interact with the vending machine in various ways:

- **Sellers** can add, update, or delete products from the vending machine.
- **Buyers** can deposit money into the machine and purchase products.

The application supports basic CRUD operations for products and users, and it ensures that all interactions follow the specified rules, such as checking for sufficient deposits and product availability.

## Features

- **User Roles**:
  - `Seller`: Can manage products (add, edit, delete).
  - `Buyer`: Can deposit money and purchase products.
  
- **Product Management**:
  - Add new products (only available on the products page).
  - Edit existing products.
  - Delete products directly from the products list.

- **Purchasing**:
  - Buyers can purchase products using their deposited money.
  - Validation ensures that buyers cannot purchase products if they do not have sufficient funds or if the product is out of stock.

## Setup and Installation

### Prerequisites

- [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48) or later
- Visual Studio 2019 or later

### Getting Started

1. **Clone the repository**:
    ```bash
    git clone https://github.com/yourusername/vending-machine-api.git
    cd vending-machine-api
    ```

2. **Open the project in Visual Studio**:
    - Double-click the `Vending Machine API.sln` file to open the project in Visual Studio.

3. **Build the project**:
    - In Visual Studio, go to `Build > Build Solution` or press `Ctrl+Shift+B`.

4. **Run the project**:
    - Press `F5` to run the project. The application will start, and the default browser will open the application, and make sure to use the `/Users` payload.

## Project Structure

- **Controllers**:
  - `ProductsController`: Handles all product-related operations, including listing, adding, editing, and deleting products.
  - `UsersController`: Manages user-related actions such as deposits and user-specific views.

- **Models**:
  - `Product`: Represents a product in the vending machine, with properties like `ProductName`, `AmountAvailable`, `Cost`, and `SellerId`.
  - `User`: Represents a user with properties such as `Username`, `Password`, `Deposit`, and `Role`.

- **Views**:
  - `Products`: Contains views for listing products (`Index`), adding products (`Create`), editing products (`Edit`), and deleting products.
  - `Users`: Contains views for managing user interactions like depositing money.

- **Data**:
  - `ProductData`: Simulates a data store for products. Provides methods to get, add, update, and delete products.
  - `UserData`: Simulates a data store for users. Provides methods to get, update, and manage users.

## Usage

### Sellers

1. **Add a New Product**:
    - Navigate to the `Products` page and click `Create New Product`.
    - Fill out the form with the product details and submit.

2. **Edit a Product**:
    - On the `Products` page, click `Edit` next to the product you want to modify.
    - Update the product details and save changes.

3. **Delete a Product**:
    - On the `Products` page, click `Delete` next to the product you want to remove.

### Buyers

1. **Deposit Money**:
    - Navigate to the `Users` page and click `Deposit Money`.
    - Select the amount you want to deposit and submit.

2. **Purchase a Product**:
    - On the `Products` page, select the quantity of the product you want to purchase and click `Purchase`.
    - Ensure you have sufficient funds and the product is in stock.

## Known Issues

- **Anti-Forgery Token Errors**: If you encounter issues with the anti-forgery token, ensure that cookies are enabled and that the forms are not cached.

## Future Work

Authentication and use of database instead of initial declaration in the models classes is expected to be done
