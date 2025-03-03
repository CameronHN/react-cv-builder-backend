using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("ProjectOutcome")]
    public class ProjectOutcomeEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int ProjectId { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Outcome { get; set; } = string.Empty;

        public ProjectEntity Project { get; set; } = null!;
    }
}
