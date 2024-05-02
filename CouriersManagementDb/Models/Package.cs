using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouriersManagementDb.Models
{
    public class Package
    {
        [Key]
        [Required]
        public int PackageID { get; set; }

        [Required(ErrorMessage = "Dimensions are required.")]
        [StringLength(30, ErrorMessage = "Dimensions must not exceed 30 characters.")]
        [RegularExpression(@"^\d{1,3}cm x \d{1,3}cm x \d{1,3}cm$", ErrorMessage = "Dimensions must be in the format 'Lcm x Wcm x Hcm' where L, W, and H are the length, width, and height in centimeters.")]
        [ValidDimensions(ErrorMessage = "Each dimension must be between 1cm and 200cm.")]
        public string Dimensions { get; set; }

        [Required(ErrorMessage = "Contents description is required.")]
        [StringLength(500, ErrorMessage = "Contents description must not exceed 500 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9,.\s]+$", ErrorMessage = "Contents can only include alphanumeric characters, commas, periods, and spaces.")]
        public string Contents { get; set; }

        [Required(ErrorMessage = "Delivery status is required")]
        public string DeliveryStatus { get; set; }

        // Foreign keys
        public int ShipmentID { get; set; }
        public int PalletID { get; set; }

        // Navigation properties
        [ForeignKey("ShipmentID")]

        public virtual Shipment Shipments { get; set; }
        public virtual Pallet Pallet { get; set; }
    }
}
public class ValidDimensionsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string dimensions)
        {
            var parts = dimensions.ToLower().Replace("cm", "").Split('x').Select(p => p.Trim());
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int dimension))
                {
                    if (dimension < 1 || dimension > 200)
                    {
                        return new ValidationResult("Each dimension must be between 1cm and 200cm.");
                    }
                }
                else
                {
                    return new ValidationResult("All parts of the dimensions must be valid integers.");
                }
            }
        }
        return ValidationResult.Success;
    }
}

