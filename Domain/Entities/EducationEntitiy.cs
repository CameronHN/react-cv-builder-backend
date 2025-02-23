using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class EducationEntitiy
    {
        public int Id { get; set; }

        [Required]
        public int QualificationTypeId { get; set; }

        [Required]
        public string FieldOfStudy { get; set; }

        [Required]
        public string Institution {  get; set; }

        [Required]
        public string StartDate { get; set; }

        public string EndDate { get; set; } = "Present";

        public string Major {  get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }

    }
}
