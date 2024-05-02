using System.ComponentModel.DataAnnotations;

namespace CouriersManagementDb.Models
{
    public class Employee
    {
        [Key]
        [Required]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Driver name must be between 3 and 50 characters long.")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Driver name must contain only letters and spaces.")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Employee number is required.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Employee number must be between 10 and 15 digits long.")]
        [RegularExpression(@"^\+?[0-9]\d{1,14}$", ErrorMessage = "Employee number must be a valid international phone number.")]
        public string EmployeeNumber { get; set; }

        public ICollection<Payment> payments { get; set; } 
    }
}
