using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AllowanceManagement.Models
{
    public class RankAmount
    {
        [Key]
        public int RankAmountId { get; set; }

        [Required]
        [DisplayName("Βαθμός")]
        public string Rank { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BaseAmount { get; set; }

    }
}
