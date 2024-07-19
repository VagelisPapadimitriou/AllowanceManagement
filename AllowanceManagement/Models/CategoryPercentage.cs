using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllowanceManagement.Models
{
    public class CategoryPercentage
    {
        [Key]
        [StringLength(1)]
        public string Category { get; set; }

        [Required]
        [Range(0.0, 1.0)]
        [Column(TypeName = "decimal(5, 2)")]  // Καθορισμός του τύπου της στήλης
        public decimal Percentage { get; set; }

        [DisplayName("Περιγραφή")]
        public string Description { get; set; }
    }
}
