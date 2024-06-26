using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouriersManagementDb.Models
{
    public class Employee
    {
        [Key]
        [Required]
        [Display(Name = "Employee ID")]
        // Unique identifier for the employee.
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "First name must start with a capital letter and contain only alphabetic characters.")]
        [Display(Name = "First Name")]
        // First name of the employee, must start with a capital letter.
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Last name must start with a capital letter and contain only alphabetic characters.")]
        [Display(Name = "Last Name")]
        // Last name of the employee, also starting with a capital letter.
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Display(Name = "Email Address")]
        // Email address for the employee, validated for proper email format.
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Phone number must be in international format.")]
        [Display(Name = "Phone Number")]
        // Phone number of the employee, must be in international format.
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [StringLength(100, ErrorMessage = "Role description must not exceed 100 characters.")]
        [Display(Name = "Role")]
        // Specific role of the employee within the company.
        public string Role { get; set; }

        public ICollection<Payment> payments { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        // Computed property to get the full name of the employee.
        public string FullName => $"{FirstName} {LastName}";
    }
}
