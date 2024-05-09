using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouriersManagementDb.Models
{
    public class Shipment
    {
        public enum DeliveryStatusEnum
        {
            Pending,
            InTransit,
            Delivered,
            Delayed,
            Cancelled
        }

        [Key]
        [Required]
        public int ShipmentID { get; set; }

        [Required(ErrorMessage = "Delivery status is required")]
        public DeliveryStatusEnum DeliveryStatus { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Arrival date is required")]
        [FutureDate(ErrorMessage = "Arrival date must be in the future")]
        public DateTime ArrivalDate { get; set; }

        // Foreign keys
        [Required]
        public int CourierID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        // Navigation properties
        [ForeignKey("CourierID")]
        public virtual Courier Courier { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

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
    }
}
