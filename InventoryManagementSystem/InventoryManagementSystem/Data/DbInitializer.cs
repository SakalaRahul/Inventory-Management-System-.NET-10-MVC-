using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Create Roles
            string[] roles = { "Admin", "Manager", "Staff" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create Admin User
            var adminEmail = "admin@inventory.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Administrator",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed Categories
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Electronics", Description = "Electronic devices and accessories" },
                    new Category { Name = "Office Supplies", Description = "Paper, pens, staplers, and desks" },
                    new Category { Name = "Hardware", Description = "Tools and building supplies" }
                };
                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }

            // Seed Suppliers
            if (!context.Suppliers.Any())
            {
                var suppliers = new List<Supplier>
                {
                    new Supplier { Name = "TechGlobal Inc.", Email = "contact@techglobal.com", Phone = "+1-555-0199", Address = "123 Silicon Way, CA" },
                    new Supplier { Name = "OfficeMart Wholesale", Email = "sales@officemart.com", Phone = "+1-555-0144", Address = "456 Paper Rd, NY" }
                };
                context.Suppliers.AddRange(suppliers);
                await context.SaveChangesAsync();
            }

            // Seed Products
            if (!context.Products.Any())
            {
                var catElec = context.Categories.First(c => c.Name == "Electronics").Id;
                var catOffice = context.Categories.First(c => c.Name == "Office Supplies").Id;

                var products = new List<Product>
                {
                    new Product { Name = "Wireless Ergonomic Mouse", SKU = "EL-MS-001", Description = "Bluetooth wireless mouse with scroll wheel", PurchasePrice = 25.00m, SellingPrice = 49.99m, StockQuantity = 85, ReorderLevel = 15, CategoryId = catElec },
                    new Product { Name = "Mechanical Gaming Keyboard", SKU = "EL-KB-002", Description = "RGB backlit mechanical keyboard", PurchasePrice = 45.00m, SellingPrice = 89.99m, StockQuantity = 40, ReorderLevel = 10, CategoryId = catElec },
                    new Product { Name = "Premium Copy Paper (500 sheets)", SKU = "OF-PP-101", Description = "A4 80gsm white printer paper", PurchasePrice = 4.50m, SellingPrice = 8.99m, StockQuantity = 200, ReorderLevel = 50, CategoryId = catOffice }
                };
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
}