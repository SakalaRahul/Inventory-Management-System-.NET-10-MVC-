📦 Inventory Management System (.NET 10 MVC)A modern, enterprise-grade Inventory Management System built using C#, .NET 10, ASP.NET Core MVC, Entity Framework Core, and SQL Server. This application features automated stock tracking, sales order processing, category and supplier management, real-time analytics, and role-based access control (RBAC).📋 Table of ContentsKey FeaturesTechnology StackArchitecture & System DesignDatabase Schema & Data ModelApplication UI & Screenshots LayoutGetting Started & InstallationTesting & VerificationRole-Based Access ControlFuture Enhancements🚀 Key FeaturesInteractive Dashboard:Real-time metrics: Total Products, Total Valuation, Low Stock Alerts, Total Sales.Recent activity feed and stock movement logs.Product Management:Complete CRUD operations with SKU tracking, dynamic pricing, reorder thresholds, and category linkages.Low-stock badge indicators and stock level visualizers.Stock & Inventory Tracking:Automated audit trails recording every inbound purchase and outbound sale via StockTransaction logs.Sales & Order Processing:Multi-item order creation with real-time stock deduction, price computation, and invoice reference tracking.Supplier & Category Management:Centralized management of vendors and product categorization.Analytics & Reporting:Sales revenue summaries, top-selling products, and stock status reports.Security & Authentication:Identity framework integration with secure password hashing and role-based authorization (Admin, Manager, User).🛠 Technology StackLayerTechnologyFramework.NET 10 / ASP.NET Core MVCLanguageC# 13ORM / DatabaseEntity Framework Core 10 / SQL Server (LocalDB/Express)AuthenticationASP.NET Core IdentityFrontend UIBootstrap 5, FontAwesome Icons, Custom CSSTestingxUnit, Moq, EF Core In-Memory Database📐 Architecture & System DesignThe application follows the MVC (Model-View-Controller) pattern adhering to SOLID principles and clean architectural separation.       +-------------------------------------------------------+
       |                   ASP.NET Core MVC                    |
       |                                                       |
       |  +------------------+         +--------------------+  |
       |  |  Views (Razor)   |<------->|    Controllers     |  |
       |  +------------------+         +--------------------+  |
       |                                         |             |
       +-----------------------------------------|-------------+
                                                 |
                                                 v
                                  +-----------------------------+
                                  |   DbContext & Data Layer    |
                                  +-----------------------------+
                                                 |
                                                 v
                                  +-----------------------------+
                                  |  SQL Server Database        |
                                  +-----------------------------+
💾 Database Schema & Data Model+--------------------+        +--------------------+        +--------------------+
|     Categories     |        |      Products      |        |     Suppliers      |
+--------------------+        +--------------------+        +--------------------+
| PK CategoryId      |<-------| PK ProductId       |------->| PK SupplierId      |
|    Name            | 1    * |    Name            | *    1 |    Name            |
|    Description     |        |    SKU             |        |    ContactName     |
+--------------------+        |    Price           |        |    Email           |
                              |    QuantityInStock |        |    Phone           |
                              |    ReorderLevel    |        +--------------------+
                              | FK CategoryId      |
                              | FK SupplierId      |
                              +--------------------+
                                      |     |
                                1     |     |     1
                                      |     |
                                *     v     v     *
                    +-------------------+ +-------------------+
                    | StockTransactions | |     SalesItems    |
                    +-------------------+ +-------------------+
                    | PK TransactionId  | | PK SaleItemId     |
                    | FK ProductId      | | FK SaleId         |
                    |    Quantity       | | FK ProductId      |
                    |    Type (In/Out)  | |    UnitPrice      |
                    |    Timestamp      | |    Quantity       |
                    +-------------------+ +-------------------+
                                                  |
                                                  *
                                                  |
                                                  1
                                          +---------------+
                                          |     Sales     |
                                          +---------------+
                                          | PK SaleId     |
                                          |    InvoiceNo  |
                                          |    TotalAmt   |
                                          |    SaleDate   |
                                          +---------------+
🖼 Application UI & Screenshots LayoutBelow are visual representations of key user interfaces implemented in the application:1. Main Executive DashboardThe main dashboard provides operational oversight at a glance:+-----------------------------------------------------------------------------------+
|  [Logo] Inventory Manager          [Products]  [Categories]  [Sales]    (Admin v) |
+-----------------------------------------------------------------------------------+
|  Dashboard Overview                                                               |
|                                                                                   |
|  +-------------------+  +-------------------+  +-------------------+  +---------+ |
|  |  Total Products   |  | Inventory Value   |  | Low Stock Alerts  |  | Sales   | |
|  |       124         |  |    $45,230.00     |  |    3 Items        |  |  $12k   | |
|  +-------------------+  +-------------------+  +-------------------+  +---------+ |
|                                                                                   |
|  +------------------------------------------------------------------------------+ |
|  | Recent Stock Movements                                                       | |
|  | - Item #104 (Wireless Mouse) - Restocked (+50) - 2 hours ago                  | |
|  | - Item #88 (Mechanical Keyboard) - Sold (-2) - 4 hours ago                     | |
|  +------------------------------------------------------------------------------+ |
+-----------------------------------------------------------------------------------+
2. Product Management ViewDisplays inventory levels with color-coded stock alerts:+-----------------------------------------------------------------------------------+
|  Product Inventory                                            [+ Add New Product] |
+-----------------------------------------------------------------------------------+
|  [Search product...  ]  [Filter Category v]                                       |
+-----+-----------------------+---------------+---------+-------+--------+----------+
| SKU | Name                  | Category      | Price   | Stock | Status | Actions  |
+-----+-----------------------+---------------+---------+-------+--------+----------+
| P01 | Mechanical Keyboard   | Electronics   | $89.99  | 14    | OK     | [Edit]   |
| P02 | Ergonomic Mouse       | Peripherals   | $45.00  | 2     | LOW    | [Edit]   |
| P03 | 27" 4K Monitor        | Displays      | $329.00 | 8     | OK     | [Edit]   |
+-----+-----------------------+---------------+---------+-------+--------+----------+
3. Sales & Order Creation UIAllows interactive cart selection and checkout with instant stock verification:+-----------------------------------------------------------------------------------+
|  Create New Sales Order                                                           |
+-----------------------------------------------------------------------------------+
|  Customer Name: [ John Doe               ]   Invoice #: [ INV-2026-0042        ] |
|                                                                                   |
|  +-------------------------------------+--------+-----------+-------------------+ |
|  | Item                                | Qty    | Price     | Subtotal          | |
|  +-------------------------------------+--------+-----------+-------------------+ |
|  | Mechanical Keyboard                 | [ 2 ]  | $89.99    | $179.98           | |
|  | Ergonomic Mouse                     | [ 1 ]  | $45.00    | $45.00            | |
|  +-------------------------------------+--------+-----------+-------------------+ |
|                                                    Total Amount:  $224.98         |
|                                                                                   |
|  [ + Add Line Item ]                                       [ Cancel ] [ Complete ]|
+-----------------------------------------------------------------------------------+
⚙️ Getting Started & InstallationPrerequisites.NET 10 SDKSQL Server 2019+ or LocalDBStep-by-Step SetupExtract Project Archive:
Unzip Inventory-Management-System.zip to your target directory.Database Configuration:
Inspect appsettings.json inside the InventoryManagementSystem folder:"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InventoryManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
Restore & Build:
Open a terminal in the solution directory and run:dotnet restore
dotnet build
Run Application:dotnet run --project InventoryManagementSystem/InventoryManagementSystem.csproj
Navigate to https://localhost:7001 or http://localhost:5001.🧪 Testing & VerificationRun the test suite using the xUnit runner:dotnet test
Test Coverage Includes:Product Controller Unit Tests: Validates list retrieval, creation validation, and details mapping.Stock Logic Verification: Ensures stock decreases upon sale and increases upon restocking.Identity Authorization: Confirms restricted access for unauthenticated requests.🔐 Role-Based Access ControlThe database seeds default credentials on startup:RoleEmailPasswordPermissionsAdminadmin@inventory.comAdmin123!Full Access (Products, Suppliers, Sales, Reports, User Management)Useruser@inventory.comUser123!Read-only Access & Basic Sales Entry🔮 Future EnhancementsAutomated PDF Invoice Generation for Sales Orders.Low Stock Email Notifications via SMTP / SendGrid.Barcode scanner integration for checkout interfaces.
The detailed README file has been updated with architecture details, database models, setup instructions, and interface layouts. Let me know when you're ready to proceed to **Project 2: Employee Management System**!
