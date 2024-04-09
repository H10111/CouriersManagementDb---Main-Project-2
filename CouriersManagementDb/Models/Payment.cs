using System.ComponentModel.DataAnnotations;

namespace CouriersManagementDb.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Timestamp is required")]
        public DateTime Timestamp { get; set; }
        public decimal Amount { get; set; }

        // Foreign keys
        [Required]
        public int ShipmentID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        // Navigation properties
        public virtual Shipment Shipment { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
