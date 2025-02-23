using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class SkillTypeEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
