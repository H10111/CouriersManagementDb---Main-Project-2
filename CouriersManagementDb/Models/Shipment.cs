using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouriersManagementDb.Models
{
    // Enum to represent various states a shipment can be in during its lifecycle.
    public enum DeliveryStatusEnum
    {
        Pending,    // Not yet dispatched
        InTransit,  // On the way to destination
        Delivered,  // Successfully delivered
        Delayed,    // Delivery delayed beyond expected time
        Cancelled   // Delivery cancelled
    }

    public class Shipment
    {
       
        [Key]
        [Required]
        public int ShipmentID { get; set; } // Primary key for the shipment entity

        [Required(ErrorMessage = "Delivery status is required")]
        public DeliveryStatusEnum DeliveryStatus { get; set; } // Status of the shipment

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Arrival date and time is required")]
        [FutureDate(ErrorMessage = "Arrival date must be in the future")] // Ensures future dates
        public DateTime ArrivalDate { get; set; } // Expected date and time of arrival

        // Expanded to include the expected dispatch date which may help in scheduling
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Dispatch date is required")]
        [PastOrCurrentDate(ErrorMessage = "Dispatch date cannot be in the future")] // Ensures current or past dates
        public DateTime DispatchDate { get; set; } // Actual or expected dispatch date and time

        // Foreign keys
        [Required]
        public int CourierID { get; set; } // Reference to the courier handling the shipment

        [Required]
        public int CustomerID { get; set; } // Reference to the customer associated with the shipment

        // Navigation properties to link with related entities
        [ForeignKey("CourierID")]
        public virtual Courier Courier { get; set; } // Navigation property to the courier entity

        public virtual Customer Customer { get; set; } // Navigation property to the customer entity

        public virtual ICollection<Package> Packages { get; set; } // Collection of packages within the shipment
        public virtual ICollection<Payment> Payments { get; set; } // Collection of payments associated with the shipment

        // Custom validation attributes to enforce date rules
        public class FutureDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is DateTime dateTimeValue && dateTimeValue < DateTime.Today)
                {
                    return new ValidationResult(GetErrorMessage(validationContext.DisplayName));
                }
                return ValidationResult.Success;
            }

            private string GetErrorMessage(string fieldName)
            {
                return $"{fieldName} must be in the future.";
            }
        }

        public class PastOrCurrentDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is DateTime dateTimeValue && dateTimeValue > DateTime.Today)
                {
                    return new ValidationResult(GetErrorMessage(validationContext.DisplayName));
                }
                return ValidationResult.Success;
            }

            private string GetErrorMessage(string fieldName)
            {
                return $"{fieldName} cannot be in the future.";
            }
        }
    }
}
