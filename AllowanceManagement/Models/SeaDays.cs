using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllowanceManagement.Models
{
    public class SeaDays
    {
        [Key]
        public int SeaDayId { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required]
        public int TotalDays { get; set; }

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
    }
}
