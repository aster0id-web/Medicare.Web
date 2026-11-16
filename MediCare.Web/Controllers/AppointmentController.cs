using MediCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MediCare.Data;
using MediCare.Data.Entities;

namespace MediCare.Web.Controllers;

[Authorize(Roles = "Patient")]
public class AppointmentController : Controller
{
    private readonly MediCareDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public AppointmentController(MediCareDbContext dbContext, UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Book(int doctorId)
    {
        // Pass the Doctor ID to the form so we know who they are booking with
        ViewBag.DoctorId = doctorId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Book(int doctorId, DateTime appointmentDateTime, string notes)
    {
        // 1. TIME TRAVEL BLOCKER (NEW)
        if (appointmentDateTime < DateTime.Now)
        {
            TempData["ErrorMessage"] = "Appointments cannot be booked in the past.";
            return RedirectToAction("Book", new { doctorId = doctorId });
        }

        // 2. Get the logged-in Identity Account
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Challenge();

        // 3. Find their connected Patient medical profile
        var patient = _dbContext.Patients.FirstOrDefault(p => p.UserId == user.Id);
        if (patient == null) return NotFound("Patient profile not found.");

        // 4. Create the Appointment record
        var appointment = new Appointment
        {
            DoctorId = doctorId,
            PatientId = patient.Id,
            AppointmentDateTime = appointmentDateTime,
            Notes = notes
        };

        // 5. Save to SQL Server
        _dbContext.Appointments.Add(appointment);
        await _dbContext.SaveChangesAsync();

        // 6. Send them back to their dashboard
        return RedirectToAction("Patient", "Dashboard");
    }
}