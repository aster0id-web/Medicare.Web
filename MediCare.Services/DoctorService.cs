using MediCare.Data;
using MediCare.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediCare.Services;

public class DoctorService : IDoctorService
{
    private readonly MediCareDbContext _dbContext;

    public DoctorService(MediCareDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
    {
        return await _dbContext.Doctors.ToListAsync();
    }
}
