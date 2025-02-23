using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class SocialMediaEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
