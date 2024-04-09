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

        [StringLength(100)]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid contact format")]
        public string Contact { get; set; }

        // Navigation properties
        public virtual ICollection<Shipment> Shipments { get; set; }
        public virtual ICollection<Pallet> Pallets { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
