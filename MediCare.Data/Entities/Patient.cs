using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace MediCare.Data.Entities
{
    public class Patient
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
        public DateTime DateOfBirth { get; set; }
        public string? MedicalHistory { get; set; }
        public string? Allergies { get; set; }
        // Navigation property – one patient has many appointments
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}