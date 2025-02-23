using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserSkillEntity
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int SkillId { get; set; }
    }
}
