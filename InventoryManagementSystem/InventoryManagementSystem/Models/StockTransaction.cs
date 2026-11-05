using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public enum TransactionType
    {
        In,
        Out,
        Adjustment
    }

    public class StockTransaction
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public TransactionType Type { get; set; }

        [Range(1, 1000000)]
        public int Quantity { get; set; }

        [StringLength(250)]
        public string Reference { get; set; } = string.Empty; // e.g., "Purchase #101", "Sale #202"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [StringLength(450)]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
    }
}