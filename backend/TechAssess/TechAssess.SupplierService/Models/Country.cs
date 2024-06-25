using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechAssess.SupplierService.Models
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s&\-_\.]+$", ErrorMessage = "Only alphanumeric characters are allowed for Country Name.")]
        public string Name { get; set; } = string.Empty;
    }
}
