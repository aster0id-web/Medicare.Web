using MediCare.Data;
using MediCare.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCare.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly MediCareDbContext _dbContext;

    public AdminController(MediCareDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Dashboard()
    {
        ViewBag.TotalDoctors = await _dbContext.Doctors.CountAsync();
        ViewBag.TotalPatients = await _dbContext.Patients.CountAsync();
        ViewBag.TotalAppointments = await _dbContext.Appointments.CountAsync();

        var appointments = _dbContext.Appointments
            .Include(appointment => appointment.Doctor)
            .Include(appointment => appointment.Patient)
            .OrderByDescending(appointment => appointment.AppointmentDateTime)
            .ToList();

        return View(appointments);
    }

    // 1. Loads the Specializations Page
    [HttpGet]
    public IActionResult Specializations()
    {
        var specializations = _dbContext.MedicalSpecializations.ToList();
        return View(specializations);
    }

    // 2. Saves a New Specialization to the Database
    [HttpPost]
    public async Task<IActionResult> AddSpecialization(string specializationName)
    {
        if (!string.IsNullOrWhiteSpace(specializationName))
        {
            var newSpecialization = new MedicalSpecialization
            {
                Name = specializationName
            };

            _dbContext.MedicalSpecializations.Add(newSpecialization);
            await _dbContext.SaveChangesAsync();
        }

        return RedirectToAction("Specializations");
    }
    [HttpGet]
    public IActionResult Users()
    {
        // Send both lists to the view using ViewBag
        ViewBag.Doctors = _dbContext.Doctors.ToList();
        ViewBag.Patients = _dbContext.Patients.ToList();

        return View();
    }
}