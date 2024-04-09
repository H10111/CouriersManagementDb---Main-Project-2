using System.ComponentModel.DataAnnotations;

namespace CouriersManagementDb.Models
{
    public class Courier
    {
        [Key]
        public int CourierID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^\+?$", ErrorMessage = "Invalid contact format")]
        public string PhoneNumber { get; set; }

        // Navigation properties
        public virtual ICollection<Shipment> Shipments { get; set; }
        public virtual ICollection<Pallet> Pallets { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
