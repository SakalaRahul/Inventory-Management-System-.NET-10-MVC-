using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }

    public class PurchaseOrder
    {
        public int Id { get; set; }

        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Column(TypeName = "decimal(18,2)]")]
        public decimal TotalAmount { get; set; }

        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
    }

    public class PurchaseOrderItem
    {
        public int Id { get; set; }

        public int PurchaseOrderId { get; set; }
        public PurchaseOrder? PurchaseOrder { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Range(1, 1000000)]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)]")]
        public decimal UnitPrice { get; set; }
    }
}