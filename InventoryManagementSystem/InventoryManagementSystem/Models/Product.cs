using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string SKU { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)]")]
        [Range(0.01, 1000000)]
        public decimal PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)]")]
        [Range(0.01, 1000000)]
        public decimal SellingPrice { get; set; }

        [Range(0, 1000000)]
        public int StockQuantity { get; set; }

        [Range(0, 10000)]
        public int ReorderLevel { get; set; } = 10;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
    }
}