using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouriersManagementDb.Models
{
    public class Tracking
    {
        [Key]
        public int TrackingID { get; set; } // Unique identifier for the tracking record

        [Required(ErrorMessage = "Status is required")]
        public DeliveryStatusEnum DeliveryStatus { get; set; } // Enum to store the delivery status

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Timestamp is required")]
        public DateTime Timestamp { get; set; } // The exact date and time of the tracking update

        // Foreign keys
        [Required]
        public int PackageID { get; set; } // Reference to the related package

        [Required]
        public int LocationID { get; set; } // Reference to the location related to the tracking update

        // Navigation properties to associate this tracking record with its corresponding package and location
        [ForeignKey("PackageID")]
        public virtual Package Package { get; set; } // Navigation property for the related package

        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; } // Navigation property for the related location

        // Custom validation attribute to ensure that timestamps are in the reasonable past to prevent erroneous data entry
        public class ReasonablePastDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                DateTime dt = Convert.ToDateTime(value);
                if (dt > DateTime.Now)
                {
                    return new ValidationResult("The date cannot be in the future.");
                }
                if (dt < DateTime.Now.AddYears(-5)) // Assuming 5 years is the maximum reasonable age for a tracking record
                {
                    return new ValidationResult("The date is too far in the past.");
                }
                return ValidationResult.Success;
            }
        }
    }
}
