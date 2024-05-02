using System.ComponentModel.DataAnnotations;

namespace CouriersManagementDb.Models
{
    public class Tracking
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
        public int TrackingID { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string DeliveryStatus { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Arrival date is required")]
        [FutureDate(ErrorMessage = "Arrival date must be in the future")]
        public DateTime Timestamp { get; set; }

        // Foreign keys
        [Required]
        public int PackageID { get; set; }

        [Required]
        public int LocationID { get; set; }

        // Navigation properties
        public virtual Package Package { get; set; }
        public virtual Location Location { get; set; }
    }
}
