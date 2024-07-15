using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllowanceManagement.Models
{
    public class RankAllowances
    {
        [Key]
        public int RankAllowancesId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BaseAmount { get; set; }
    }
}
