using System.ComponentModel.DataAnnotations;

namespace CouriersManagementDb.Models
{
    public class Package
    {
        [Key]
        public int PackageID { get; set; }

        [Required(ErrorMessage = "Dimensions are required")]
        [StringLength(50, ErrorMessage = "Dimensions cannot be longer than 50 characters")]
        public string Dimensions { get; set; }

        [Required(ErrorMessage = "Contents description is required")]
        [StringLength(200, ErrorMessage = "Contents description cannot be longer than 200 characters")]
        public string Contents { get; set; }

        [Required(ErrorMessage = "Delivery status is required")]
        public string DeliveryStatus { get; set; }

        // Foreign keys
        public int? ShipmentID { get; set; }
        public int? PalletID { get; set; }

        // Navigation properties
        public virtual Shipment Shipment { get; set; }
        public virtual Pallet Pallet { get; set; }
    }
}
