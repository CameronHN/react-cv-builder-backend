using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Project")]
    public class ProjectEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string ProjectName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(200)")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string StartDate { get; set; } = string.Empty;

        [Column(TypeName = "varchar(10)")]
        public string EndDate { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string GithubUrl { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "int")]
        public int ProjectTypeId { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        public required ProjectTypeEntity ProjectType { get; set; } = null!;

        public required UserEntity User { get; set; } = null!;
    }
}
