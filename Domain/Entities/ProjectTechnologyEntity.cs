using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("ProjectTechnology")]
    public class ProjectTechnologyEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int ProjectId { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int TechnologyId { get; set; }

        public ProjectEntity Project { get; set; } = null!;

        public TechnologyEntity Technology { get; set; } = null!;

    }
}
