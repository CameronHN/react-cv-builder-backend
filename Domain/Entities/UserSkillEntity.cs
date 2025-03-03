using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("UserSkill")]
    public class UserSkillEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int SkillId { get; set; }

        public UserEntity User { get; set; } = null!;

        public SkillEntity Skill { get; set; } = null!;
    }
}
