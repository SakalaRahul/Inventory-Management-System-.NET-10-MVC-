using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string CustomerName { get; set; } = "Walk-in Customer";

        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)]")]
        public decimal TotalAmount { get; set; }

        [StringLength(450)]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }

    public class SaleItem
    {
        public int Id { get; set; }

        public int SaleId { get; set; }
        public Sale? Sale { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Range(1, 1000000)]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)]")]
        public decimal UnitPrice { get; set; }
    }
}