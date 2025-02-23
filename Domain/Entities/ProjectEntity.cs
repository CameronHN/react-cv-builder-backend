using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ProjectEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string StartDate { get; set; }

        public string EndDate { get; set; } = "Present";
        public string Client { get; set; } = string.Empty;
        public string GithubUrl { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }
    }
}
