using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllowanceManagement.Models
{
    public class CategoryPercentage
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Η Κατηγορία είναι υποχρεωτική")]
        [DisplayName("Κατηγορία")]
        [StringLength(1)]
        public string Category { get; set; }

        [Required(ErrorMessage = "Το Ποσοστό είναι υποχρεωτικό")]
        [DisplayName("Ποσοστό")]
        [Range(0.0, 1.0)]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Percentage { get; set; }

        [Required(ErrorMessage = "Η Περιγραφή είναι υποχρεωτική")]
        [DisplayName("Περιγραφή")]
        public string Description { get; set; }


        public ICollection<Employee> Employees { get; } = new List<Employee>();
    }
}
