using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ProjectDescription
    {
        public int Id { get; set; }

        [Required]
        public required int ProjectId { get; set; }

        [Required]
        public required string Description { get; set; }
    }
}
