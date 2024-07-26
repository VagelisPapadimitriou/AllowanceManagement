using System;
using System.ComponentModel.DataAnnotations;

namespace AllowanceManagement.Models
{
    public class UploadedFile
    {
        [Key]
        public int FileId { get; set; }

        [Required]
        [StringLength(200)]
        public string FileName { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        [Required]
        public byte[] FileContent { get; set; }
    }
}
