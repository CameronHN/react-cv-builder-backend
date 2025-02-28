using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Education")]
    public class EducationEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Qualification { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string FieldOfStudy { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Institution { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string StartDate { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string EndDate { get; set; } = string.Empty;

        [Column(TypeName = "varchar(20)")]
        public string Major { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        public UserEntity User { get; set; } = null!;
    }
}
