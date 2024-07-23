using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace AllowanceManagement.Models
{
    public class Employee
    {
        [Key]
        [Required(ErrorMessage = "Ο Αριθμός Μητρώου είναι υποχρεωτικός")]
        [DisplayName("Αριθμός Μητρώου")]
        public string AM { get; set; }

        [Required(ErrorMessage = "Το Όνομα είναι υποχρεωτικό")]
        [StringLength(100)]
        [DisplayName("Όνομα")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Το Επώνυμο είναι υποχρεωτικό")]
        [StringLength(100)]
        [DisplayName("Επώνυμο")]
        public string LastName { get; set; }

        [DisplayName("Πλεύσιμες Ημέρες")]
        public int SeaDay { get; set; } = 0;


        // Foreign key for RankAmount
        [Required(ErrorMessage = "Ο Βαθμός είναι υποχρεωτικός")]
        [ForeignKey("RankAmount")]
        public int? RankAmountId { get; set; }
        public RankAmount? RankAmount { get; set; }

        // Foreign key for CategoryPercentage
        [Required(ErrorMessage = "Η Κατηγορία είναι υποχρεωτική")]
        [ForeignKey("CategoryPercentage")]
        public int? CategoryId { get; set; }
        public CategoryPercentage? CategoryPercentage { get; set; }
    }
}
