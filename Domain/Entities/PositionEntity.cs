using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Position")]
    public class PositionEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Role { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string StartDate { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string EndDate { get; set; } = string.Empty;

        // Foreign key
        [Required]
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        // Navigation property
        public UserEntity User { get; set; } = null!;

        // Navigation property
        public ICollection<PositionResponsibilityEntity>? Responsibilities { get; set; }
    }
}
