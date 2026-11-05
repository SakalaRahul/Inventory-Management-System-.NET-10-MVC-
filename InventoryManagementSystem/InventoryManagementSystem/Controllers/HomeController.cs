using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalProducts = await _context.Products.CountAsync();
            ViewBag.TotalCategories = await _context.Categories.CountAsync();
            ViewBag.TotalSuppliers = await _context.Suppliers.CountAsync();
            ViewBag.TotalSales = await _context.Sales.SumAsync(s => (decimal?)s.TotalAmount) ?? 0m;
            
            ViewBag.LowStockProducts = await _context.Products
                .Where(p => p.StockQuantity <= p.ReorderLevel)
                .Include(p => p.Category)
                .ToListAsync();

            ViewBag.RecentSales = await _context.Sales
                .OrderByDescending(s => s.SaleDate)
                .Take(5)
                .Include(s => s.User)
                .ToListAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}