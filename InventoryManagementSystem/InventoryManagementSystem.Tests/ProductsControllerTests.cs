using InventoryManagementSystem.Controllers;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InventoryManagementSystem.Tests
{
    public class ProductsControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Categories.Add(new Category { Id = 1, Name = "Test Category" });
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithListOfProducts()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            context.Products.Add(new Product { Id = 1, Name = "Test Product", SKU = "TST-001", PurchasePrice = 10, SellingPrice = 20, StockQuantity = 50, CategoryId = 1 });
            await context.SaveChangesAsync();

            var controller = new ProductsController(context);

            // Act
            var result = await controller.Index(null, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.ViewData.Model);
            Assert.Single(model);
        }
    }
}