using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace CouriersManagementDb.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }  // Primary key for the Location entity

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }  // Foreign key linking to the Customer table

        [NotMapped]
        public string CustomerName => Customer?.FirstName;  // Automatically gets the name from the associated Customer

        [Required(ErrorMessage = "Address line is required.")]
        [StringLength(200, ErrorMessage = "Address line cannot exceed 200 characters.")]
        public string AddressLine1 { get; set; }  // Primary address line

        [StringLength(200, ErrorMessage = "Secondary address line should not exceed 200 characters.")]
        public string AddressLine2 { get; set; }  // Secondary address line (optional)

        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s\-]+$", ErrorMessage = "City must only contain letters, spaces, or dashes.")]
        public string City { get; set; }  // City name

        [Required(ErrorMessage = "State or province is required.")]
        [StringLength(100, ErrorMessage = "State or province cannot exceed 100 characters.")]
        public string StateOrProvince { get; set; }  // State or province

        [Required(ErrorMessage = "Postal code is required.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Postal code must be numeric.")]
        public string PostalCode { get; set; }  // Postal code


        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, ErrorMessage = "Country name cannot exceed 100 characters.")]
        public string Country { get; set; }  // Country name

        public virtual Customer Customer { get; set; }  // Navigation property to the Customer


    }
}
