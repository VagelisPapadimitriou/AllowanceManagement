using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AllowanceManagement.Models
{
    public class Employee
    {
        [Key]
        public int AM { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        public int Rank { get; set; }

        [Required]
        [StringLength(1)]
        public string Category { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Allowance { get; set; }
    }
}
