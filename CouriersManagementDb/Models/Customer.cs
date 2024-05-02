using System.ComponentModel.DataAnnotations;

namespace CouriersManagementDb.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Customer name must be between 3 and 50 characters long.")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Customer name must contain only letters and spaces.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Customer address is required.")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Customer address must be between 10 and 100 characters long.")]
        [RegularExpression(@"^[a-zA-Z0-9\s,.-]+$", ErrorMessage = "Customer address can only include alphanumeric characters, spaces, commas, periods, and hyphens.")]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "Customer number is required.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Customer number must be between 10 and 15 digits long.")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Customer number must be a valid international phone number.")]
        public string CustomerNumber { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
