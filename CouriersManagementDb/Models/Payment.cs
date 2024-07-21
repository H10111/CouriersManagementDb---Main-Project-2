using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouriersManagementDb.Models
{
    public class Payment
    {
        [Key]
        // Primary key for the Payment entity
        public int PaymentID { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "The timestamp of the payment is required.")]
        // Custom validation to ensure the payment date is within a valid range.
        [ValidPaymentDate(ErrorMessage = "The payment date must be within valid range and cannot be set in the distant past or future.")]
        [Display(Name = "Payment Timestamp")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, 10000.00, ErrorMessage = "Amount must be between $0.01 and $10,000.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Amount Paid")]
        // Ensures the amount is stored with two decimal places, suitable for currency.
        public decimal Amount { get; set; }

        // Foreign keys
        [Required]
        public int ShipmentID { get; set; }
        [Required]
        public int PackageID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }

        // Navigation properties
        [ForeignKey("ShipmentID")]
        public virtual Shipment Shipments { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customers { get; set; }
        [ForeignKey("PackageID")]
        public virtual Package Packages { get; set; }

        public virtual Employee Employee { get; set; }

    }
}
public class ValidPaymentDateAttribute : ValidationAttribute
{
    public ValidPaymentDateAttribute()
    {
        // Setting a default error message indicating the the date must be the present day
        ErrorMessage = "The payment date must be today's date.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime date)
        {
            if (date.Date != DateTime.Today)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid data type");
    }
}

