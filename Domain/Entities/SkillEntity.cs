using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class SkillEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int SkillTypeId { get; set; }
    }
}
