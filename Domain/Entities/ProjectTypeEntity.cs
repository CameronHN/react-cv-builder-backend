using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("ProjectType")]
    public class ProjectTypeEntity
    {
        [Column(TypeName = "int")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string ProjectTypeName { get; set; } = string.Empty;

        public ProjectEntity ProjectEntity { get; set; } = null!;
    }
}
