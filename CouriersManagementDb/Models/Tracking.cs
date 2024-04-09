using System.ComponentModel.DataAnnotations;

namespace CouriersManagementDb.Models
{
    public class Tracking
    {
        [Key]
        public int TrackingID { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "TimeStamp is required")]
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
