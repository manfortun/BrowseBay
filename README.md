# BrowseBay
This is an e-commerce web application that allows users to browse, search for, and purchase products. Users can create accounts, log in, etc.
In this application, there are 3 types of users
#### 1. Buyer - this type of user can purchase items
#### 2. Seller - sellers can create their own products for listing in the market, and also buy products in the market
#### 3. Admin - admins can add/modify/remove categories. Admins can also do what sellers and buyers do.

## Database Setup
#### 1. Configure Server Connection:
* Open the `appsettings.json` file in your project.
* Locate the `"DefaultConnection"` property and set its value to match your server name. For example: `"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=EMarket;Trusted_Connection=True;TrustServerCertificate=True;"`
Make sure that your machine has a local server instance.

#### 2. Open Package Manager Console
* In Visual Studio, go to `Tools`>`NuGet Package Manager`>`Package Manager Console`.
* Make sure that the selected `Default Project` is set to `EMarket.DataAccess`

#### 3. Run Database Migration
* In the Package Manager Console, enter the following command: `update-database`

  Your database setup should now be complete. You can now proceed with running your application.
