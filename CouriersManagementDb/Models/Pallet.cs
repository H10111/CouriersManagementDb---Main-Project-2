using System.ComponentModel.DataAnnotations;

namespace CouriersManagementDb.Models
{
    public class Pallet
    {
        [Key]
        [Required]
        public int PalletID { get; set; }

        [Required]
        public int? CourierID { get; set; }

        // Navigation properties
        public virtual Courier Courier { get; set; }
        public virtual ICollection<Package> Packages { get; set; }

        [Display(Name = "Number of Packages")]
        public int PackagesCount => Packages?.Count ?? 0;
    }
}
