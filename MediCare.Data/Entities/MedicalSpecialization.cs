using System.ComponentModel.DataAnnotations;
namespace MediCare.Data.Entities
{
    public class MedicalSpecialization
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}