using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouriersManagementDb.Models
{
    public class Courier
    {
        [Key]
        [Required]
        [Display(Name = "Courier ID")]
        // Primary key for the Courier entity.
        public int CourierID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "First name must start with a capital letter and contain only alphabetic characters.")]
        [Display(Name = "First Name")]
        // First name of the courier, starting with a capital letter.
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Last name must start with a capital letter and contain only alphabetic characters.")]
        [Display(Name = "Last Name")]
        // Last name of the courier, also starting with a capital letter.
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Display(Name = "Email Address")]
        // Email address for contacting the courier, validated to ensure it follows the correct email format.
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Phone number must be a valid international phone number.")]
        [Display(Name = "Phone Number")]
        // International format phone number of the courier, validated for global use.
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Base location is required.")]
        [StringLength(100, ErrorMessage = "Base location must not exceed 100 characters.")]
        // Base operational location of the courier which helps in assigning regional logistics tasks.
        public string BaseLocation { get; set; }

        // Navigation properties
        public virtual ICollection<Shipment> Shipments { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }


        [NotMapped]
        // Computed property that combines first and last name for easier access to the courier's full name.
        public string FullName => $"{FirstName} {LastName}";

        internal static object AsNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}
