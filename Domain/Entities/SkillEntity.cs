using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Skill")]
    public class SkillEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string SkillName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "int")]
        public int SkillTypeId { get; set; }

        public SkillTypeEntity SkillType { get; set; } = null!;
    }
}
