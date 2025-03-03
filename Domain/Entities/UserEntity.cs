using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("User")]
    public class UserEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string? MiddleName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "varchar(12)")]
        public string PhoneNumber { get; set; } = string.Empty;

        // Navigation property
        // A user can have many positions
        public ICollection<PositionEntity> Positions { get; set; } = new List<PositionEntity>();

        public ICollection<UserSkillEntity> UserSkills { get; set; } = new List<UserSkillEntity>();

        public ICollection<EducationEntity> Educations { get; set; } = new List<EducationEntity>();

        public ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();

        public ICollection<ProfessionalStatementEntity> ProfessionalStatements { get; set; } = new List<ProfessionalStatementEntity>();

        public ICollection<SocialMediaEntity> SocialMedias { get; set; } = new List<SocialMediaEntity>();
        
        public ICollection<HobbyEntity> Hobbies { get; set; } = new List<HobbyEntity>();
    }
}
