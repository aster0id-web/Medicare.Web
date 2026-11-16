using MediCare.Data.Entities;

namespace MediCare.Services;

public interface IAppointmentService
{
    Task BookAppointmentAsync(Appointment appointment);
    Task<IEnumerable<Appointment>> GetAppointmentsForDoctorAsync(int doctorId);
}