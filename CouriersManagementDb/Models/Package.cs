using NuGet.Packaging.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouriersManagementDb.Models
{
    public enum PackageType
    {
        Standard,
        Fragile,
        Liquid,
        Perishable
    }

    public class Package
    {
        
        [Key] // Indicates that PackageID is the primary key in the database
        public int PackageID { get; set; }

        [Required(ErrorMessage = "Package dimensions are required.")] // Ensures this field is not empty
        [StringLength(100, ErrorMessage = "Dimensions specification cannot exceed 100 characters.")] // Limits the string length to 100 characters
        [RegularExpression(@"^\d{1,3}x\d{1,3}x\d{1,3}cm$", ErrorMessage = "Dimensions must be in the format 'LxWxH cm' and each dimension must be between 1 and 999.")]
        // Ensures dimensions follow a specific pattern and size range
        public string Dimensions { get; set; }

        [Required(ErrorMessage = "Contents description is required.")] // Field must not be empty
        [StringLength(500, ErrorMessage = "Contents description must not exceed 500 characters.")] // Limits the string length to 500 characters
        public string Contents { get; set; }

        [Required(ErrorMessage = "Weight is required and must be a positive number.")] // Ensures the weight is provided
        [Range(0.01, 10000.00, ErrorMessage = "Weight must be between 0.01 and 10,000 kilograms.")] // Validates weight is within the specified range
        public double Weight { get; set; }

        [Required(ErrorMessage = "Package type is required.")] // Ensures package type is selected
        public PackageType Type { get; set; } // Uses enum to list acceptable package types

        // Foreign keys and navigation properties for database relationships
        public int ShipmentID { get; set; } // Foreign key relating package to a specific shipment

        // Navigation properties
        [ForeignKey("ShipmentID")]
        public virtual Shipment Shipments { get; set; } // Navigation property to the Shipment entity
    }
}


