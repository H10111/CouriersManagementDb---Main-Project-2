using System.ComponentModel.DataAnnotations;

namespace CouriersManagementDb.Models
{
    public class Courier
    {
        [Key]
        [Required]
        public int CourierID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Driver name must be between 3 and 50 characters long.")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Driver name must contain only letters and spaces.")]
        public string DriverName { get; set; }

        [Required(ErrorMessage = "Diver number is required.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Customer number must be between 10 and 15 digits long.")]
        [RegularExpression(@"^\+?[0-9]\d{1,14}$", ErrorMessage = "Customer number must be a valid phone number.")]
        public string DriverNumber { get; set; }

        // Navigation properties
        public virtual ICollection<Shipment> Shipments { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Pallet> Pallets { get; set; }

    }
}
