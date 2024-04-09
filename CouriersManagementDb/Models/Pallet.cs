using System.ComponentModel.DataAnnotations;

namespace CouriersManagementDb.Models
{
    public class Pallet
    {
        [Key]
        public int PalletID { get; set; }

        // Assuming that a Pallet can exist without a Courier
        public int? CourierID { get; set; }

        // Navigation properties
        public virtual Courier Courier { get; set; }
        public virtual ICollection<Package> Packages { get; set; }

        // Computed property for PackagesCount, not mapped to database
        [Display(Name = "Number of Packages")]
        public int PackagesCount => Packages?.Count ?? 0;
    }
}
