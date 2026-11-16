using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MediCare.Data.Entities;

namespace MediCare.Data
{
    public class MediCareDbContext : IdentityDbContext<IdentityUser>
    {
        public MediCareDbContext(DbContextOptions<MediCareDbContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalSpecialization> MedicalSpecializations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Link Doctor to MedicalSpecialization
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialization)
                .WithMany()
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Appointment Relationships
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed MedicalSpecializations
            modelBuilder.Entity<MedicalSpecialization>().HasData(
                new MedicalSpecialization { Id = 1, Name = "Cardiology", Description = "Heart and vascular system" },
                new MedicalSpecialization { Id = 2, Name = "Dermatology", Description = "Skin, hair, and nails" },
                new MedicalSpecialization { Id = 3, Name = "Neurology", Description = "Nervous system and brain" },
                new MedicalSpecialization { Id = 4, Name = "Pediatrics", Description = "Medical care for children" },
                new MedicalSpecialization { Id = 5, Name = "Orthopedics", Description = "Musculoskeletal system" }
            );

            // Seed Identity roles
            var adminRole = new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" };
            var doctorRole = new IdentityRole { Id = "2", Name = "Doctor", NormalizedName = "DOCTOR" };
            var patientRole = new IdentityRole { Id = "3", Name = "Patient", NormalizedName = "PATIENT" };

            modelBuilder.Entity<IdentityRole>().HasData(adminRole, doctorRole, patientRole);

            // Seed default admin user
            var adminUser = new IdentityUser
            {
                Id = "admin-uid",
                UserName = "admin@medicare.com",
                NormalizedUserName = "ADMIN@MEDICARE.COM",
                Email = "admin@medicare.com",
                NormalizedEmail = "ADMIN@MEDICARE.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEMOCK_PASSWORD_HASH_PLACEHOLDER",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            };

            modelBuilder.Entity<IdentityUser>().HasData(adminUser);

            // Assign Admin role to default admin user
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = adminUser.Id,
                RoleId = adminRole.Id
            });
        }
    }
}