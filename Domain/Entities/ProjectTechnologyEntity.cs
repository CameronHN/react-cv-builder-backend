using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ProjectTechnologyEntity
    {
        public int Id { get; set; }

        [Required]
        public required int ProjectId { get; set; }

        [Required]
        public required int TechnologyId { get; set; }

    }
}
