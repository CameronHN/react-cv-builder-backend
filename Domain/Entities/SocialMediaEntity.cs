using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("SocialMedia")]
    public class SocialMediaEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string SocialMediaName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string SocialMediaUrl { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        public required UserEntity User { get; set; } = null!;
    }
}
