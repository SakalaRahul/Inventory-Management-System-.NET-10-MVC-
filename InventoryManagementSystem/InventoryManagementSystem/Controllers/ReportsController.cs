using InventoryManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalInventoryValue = await _context.Products.SumAsync(p => p.StockQuantity * p.PurchasePrice);
            ViewBag.TotalPotentialSales = await _context.Products.SumAsync(p => p.StockQuantity * p.SellingPrice);
            ViewBag.LowStockCount = await _context.Products.CountAsync(p => p.StockQuantity <= p.ReorderLevel);

            var topSellingProducts = await _context.SaleItems
                .GroupBy(si => si.ProductId)
                .Select(g => new { ProductId = g.Key, TotalSold = g.Sum(si => si.Quantity) })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToListAsync();

            ViewBag.StockTransactions = await _context.StockTransactions
                .Include(st => st.Product)
                .Include(st => st.User)
                .OrderByDescending(st => st.CreatedAt)
                .Take(20)
                .ToListAsync();

            return View();
        }
    }
}