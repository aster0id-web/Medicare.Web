
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace MediCare.Data.Entities
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = null!; // FK to IdentityUser
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = null!;
        [Required]
        public int SpecializationId { get; set; }
        // Navigation property to specialization
        public MedicalSpecialization? Specialization { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ConsultationFee { get; set; }
        public string? ProfileImagePath { get; set; }
        // Navigation property – one doctor has many appointments
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}