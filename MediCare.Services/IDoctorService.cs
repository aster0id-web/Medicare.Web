using MediCare.Data.Entities;

namespace MediCare.Services;

public interface IDoctorService
{
    Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
}
