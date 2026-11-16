using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MediCare.Data.Entities
{
    public enum AppointmentStatus
    {
        Scheduled = 0,
        Completed = 1,
        Cancelled = 2,
        NoShow = 3
    }
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        // Foreign keys
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        // Date and time of the appointment
        public DateTime AppointmentDateTime { get; set; }
        // Current status of the appointment
        public AppointmentStatus Status { get; set; }
        public string? Notes { get; set; }
        // Navigation properties (optional for eager loading)
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
    }
}