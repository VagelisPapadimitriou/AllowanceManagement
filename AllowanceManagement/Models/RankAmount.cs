using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.Extensions.Hosting;

namespace AllowanceManagement.Models
{
    public class RankAmount
    {
        [Key]
        public int RankAmountId { get; set; }

        [Required(ErrorMessage = "Ο Βαθμός είναι υποχρεωτικός")]
        [DisplayName("Βαθμός")]
        public string Rank { get; set; }

        [DisplayName("Καθήκον")]
        public string Duty { get; set; }

        [Required(ErrorMessage = "Το αρχικό ποσό επιδόματος είναι υποχρεωτικό")]
        [DisplayName("Αρχικό Ποσό Επιδόματος")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BaseAmount { get; set; }


        public ICollection<Employee> Employees { get; } = new List<Employee>();

    }
}
