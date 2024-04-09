using System.ComponentModel.DataAnnotations;

namespace CouriersManagementDb.Models
{
    public class Shipment
    {
        [Key]
        public int ShipmentID { get; set; }

        [Required(ErrorMessage = "Delivery status is required")]
        public string DeliveryStatus { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Arrival date is required")]
        public DateTime ArrivalDate { get; set; }

        // Foreign keys
        [Required]
        public int CourierID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        // Navigation properties
        public virtual Courier Courier { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
