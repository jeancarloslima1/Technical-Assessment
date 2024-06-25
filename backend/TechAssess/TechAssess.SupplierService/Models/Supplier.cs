using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TechAssess.SupplierService.Models
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s&\-_\.!,]+$", ErrorMessage = "Only alphanumeric characters are allowed for LegalName.")]
        public string LegalName { get; set; } = string.Empty;

        [RegularExpression(@"^[a-zA-Z0-9\s&\-_\.!,]+$", ErrorMessage = "Only alphanumeric characters are allowed for TradeName.")]
        public string TradeName { get; set; } = string.Empty;

        [StringLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Invalid Tax Identification Number.")]
        public string TaxIdentificationNumber { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Url]
        public string Website { get; set; } = string.Empty;

        [RegularExpression(@"^[a-zA-Z0-9\s&\-_\.,]+$", ErrorMessage = "Only alphanumeric characters are allowed for PhysicalAddress.")]
        public string PhysicalAddress { get; set; } = string.Empty;

        public int CountryId { get; set; }
        [JsonIgnore]
        public Country? Country { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AnnualRevenueUSD { get; set; }

        public DateTime LastEdited { get; set; }
    }
}
