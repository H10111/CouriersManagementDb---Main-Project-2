using System.ComponentModel.DataAnnotations;

namespace CouriersManagementDb.Models
{
    public class Customer
    {
        [Key] // Marks CustomerID as the primary key
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "First name is required.")] // Ensures the first name is not left blank
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters long.")] // Limits the length of the first name
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "First name must start with a capital letter and contain only letters.")] // Ensures the first name starts with a capital letter and only contains alphabetic characters
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")] // Ensures the last name is not left blank
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters long.")] // Limits the length of the last name
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Last name must start with a capital letter and contain only letters.")] // Ensures the last name starts with a capital letter and only contains alphabetic characters
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required.")] // Ensures the email address is not left blank
        [EmailAddress(ErrorMessage = "Email address is invalid.")] // Validates that the email address is in a correct format
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")] // Ensures the address is not left blank
        [StringLength(100, ErrorMessage = "Address must not exceed 100 characters.")] // Limits the length of the address to 100 characters
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")] // Ensures the phone number is not left blank
        [Phone(ErrorMessage = "Invalid phone number.")] // Validates that the phone number is in a correct format
        [RegularExpression(@"^[1-9]{1}[0-9]{3,14}$", ErrorMessage = "Phone number must be in international format without starting with '+'.")]
        // Ensures the phone number is in international format
        public string PhoneNumber { get; set; }
        
        //Navgation properties relationships to other models
        public virtual ICollection<Shipment> Shipments { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
