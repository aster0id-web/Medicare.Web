using MediCare.Services;
using MediCare.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCare.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentService _appointmentService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MediCareDbContext _dbContext;

        public DashboardController(
            IDoctorService doctorService,
            IAppointmentService appointmentService,
            UserManager<IdentityUser> userManager,
            MediCareDbContext dbContext)
        {
            _doctorService = doctorService;
            _appointmentService = appointmentService;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Patient()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return View(doctors);
        }

        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Doctor()
        {
            var userId = _userManager.GetUserId(User);
            var doctor = await _dbContext.Doctors
                .FirstOrDefaultAsync(profile => profile.UserId == userId);

            if (doctor is null)
            {
                return NotFound();
            }

            var appointments = await _appointmentService.GetAppointmentsForDoctorAsync(doctor.Id);
            return View(appointments);
        }
    }
}
