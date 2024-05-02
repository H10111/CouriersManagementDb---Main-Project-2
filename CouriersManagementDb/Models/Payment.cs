using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouriersManagementDb.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Arrival date is required")]
        [FutureDate(ErrorMessage = "Arrival date must be in the future")]
        public DateTime Timestamp { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, 10000.00, ErrorMessage = "Amount must be between $0.01 and $10,000.00.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must have up to two decimal places.")]
        public decimal Amount { get; set; }

        // Foreign keys
        [Required]
        public int ShipmentID { get; set; }
        [Required]
        public int PackageID { get; set; }
        [Required]
        public int CustomerID { get; set; }

        // Navigation properties
        [ForeignKey("ShipmentID")]
        public virtual Shipment Shipments { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customers { get; set; }
        [ForeignKey("PackageID")]
        public virtual Package Packages { get; set; }
    }
}
public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateTimeValue)
        {
            if (dateTimeValue < DateTime.Today)
            {
                return new ValidationResult(GetErrorMessage(validationContext.DisplayName));
            }
        }
        return ValidationResult.Success;
    }

    private string GetErrorMessage(string fieldName)
    {
        return $"{fieldName} must be in the future.";
    }
}
