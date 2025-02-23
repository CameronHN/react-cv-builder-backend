using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TechnologyEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
