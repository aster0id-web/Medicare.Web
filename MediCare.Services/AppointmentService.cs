using Microsoft.EntityFrameworkCore;
using MediCare.Data;
using MediCare.Data.Entities;

namespace MediCare.Services;

public class AppointmentService : IAppointmentService
{
    private readonly MediCareDbContext _dbContext;

    public AppointmentService(MediCareDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // 1. For the Patient to book an appointment (This was missing)
    public async Task BookAppointmentAsync(Appointment appointment)
    {
        _dbContext.Appointments.Add(appointment);
        await _dbContext.SaveChangesAsync();
    }

    // 2. For the Doctor to view their schedule
    public async Task<IEnumerable<Appointment>> GetAppointmentsForDoctorAsync(int doctorId)
    {
        return await _dbContext.Appointments
            .Include(appointment => appointment.Patient)
            .Where(appointment => appointment.DoctorId == doctorId)
            .OrderBy(appointment => appointment.AppointmentDateTime)
            .ToListAsync();
    }
}