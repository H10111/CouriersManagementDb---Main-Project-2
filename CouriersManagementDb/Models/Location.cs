using System.ComponentModel.DataAnnotations;

namespace CouriersManagementDb.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters")]
        public string Address { get; set; }
    }
}
