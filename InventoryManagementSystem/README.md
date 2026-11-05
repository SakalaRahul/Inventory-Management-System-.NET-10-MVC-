# Inventory Management System (IMS Enterprise)

A complete, production-grade Inventory Management System built with **C#, .NET 10, ASP.NET Core MVC, Entity Framework Core**, and **SQL Server**. Designed with a professional, modern, responsive UI using Bootstrap 5 and FontAwesome.

## Features
- **Secure Authentication & Role-Based Authorization**: ASP.NET Core Identity with Admin, Manager, and Staff roles.
- **Executive Dashboard**: Real-time KPI metrics, low-stock alerts, and recent sales overview.
- **Product Management**: Full CRUD operations with SKU tracking, purchase/selling prices, reorder levels, and stock tracking.
- **Category & Supplier Management**: Organize inventory and vendor relationships.
- **Sales & Purchase Workflow**: Process customer sales with automatic stock deduction and transaction auditing.
- **Reporting & Analytics**: Comprehensive valuation summaries and stock movement audit trails.
- **Unit & Integration Tests**: xUnit test suite with InMemory Entity Framework testing.

---

## Database Setup & Execution Instructions

### Prerequisites
1. [.NET 10 SDK](https://dotnet.microsoft.com/) installed on your computer.
2. SQL Server (LocalDB or SQL Server Express / Developer edition).

### 1. Configure Connection String
Verify `appsettings.json` in the `InventoryManagementSystem` project:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\mssqllocaldb;Database=InventoryManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### 2. Restore Dependencies
Open your terminal in the `InventoryManagementSystem` root folder and run:
```bash
dotnet restore
```

### 3. Run Database Migrations & Seed Data
The application automatically runs migrations and seeds default admin credentials (`admin@inventory.com` / `Admin123!`) upon startup. You can also apply migrations explicitly:
```bash
dotnet ef database update --project InventoryManagementSystem
```

### 4. Build and Run the Application
```bash
dotnet run --project InventoryManagementSystem/InventoryManagementSystem.csproj
```
Open your browser and navigate to `https://localhost:7001` or `http://localhost:5001`.

### 5. Run Tests
```bash
dotnet test
```

---

## Default Login Credentials
- **Email**: `admin@inventory.com`
- **Password**: `Admin123!`