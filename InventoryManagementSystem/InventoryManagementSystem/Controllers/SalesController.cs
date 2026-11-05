using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sales = await _context.Sales
                .Include(s => s.User)
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
            return View(sales);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var sale = await _context.Sales
                .Include(s => s.User)
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (sale == null) return NotFound();

            return View(sale);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await _context.Products.Where(p => p.StockQuantity > 0).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string customerName, int[] productIds, int[] quantities)
        {
            if (productIds == null || quantities == null || productIds.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Please select at least one product for sale.");
                ViewBag.Products = await _context.Products.Where(p => p.StockQuantity > 0).ToListAsync();
                return View();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var sale = new Sale
            {
                CustomerName = string.IsNullOrEmpty(customerName) ? "Walk-in Customer" : customerName,
                UserId = userId,
                SaleDate = DateTime.UtcNow,
                SaleItems = new List<SaleItem>()
            };

            decimal totalAmount = 0;

            for (int i = 0; i < productIds.Length; i++)
            {
                var productId = productIds[i];
                var qty = quantities[i];
                if (qty <= 0) continue;

                var product = await _context.Products.FindAsync(productId);
                if (product == null) continue;

                if (product.StockQuantity < qty)
                {
                    ModelState.AddModelError(string.Empty, $"Insufficient stock for product {product.Name}. Available: {product.StockQuantity}");
                    ViewBag.Products = await _context.Products.Where(p => p.StockQuantity > 0).ToListAsync();
                    return View();
                }

                // Deduct stock
                product.StockQuantity -= qty;

                var itemTotal = product.SellingPrice * qty;
                totalAmount += itemTotal;

                sale.SaleItems.Add(new SaleItem
                {
                    ProductId = productId,
                    Quantity = qty,
                    UnitPrice = product.SellingPrice
                });

                // Record stock transaction
                _context.StockTransactions.Add(new StockTransaction
                {
                    ProductId = productId,
                    Type = TransactionType.Out,
                    Quantity = qty,
                    Reference = $"Sale Order",
                    UserId = userId
                });
            }

            if (!sale.SaleItems.Any())
            {
                ModelState.AddModelError(string.Empty, "Invalid quantities selected.");
                ViewBag.Products = await _context.Products.Where(p => p.StockQuantity > 0).ToListAsync();
                return View();
            }

            sale.TotalAmount = totalAmount;
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}