using HM.Core.Constants;
using HM.Core.Contracts;
using HM.Core.Models.Appointment;
using HM.Core.Models.Referral;
using HM.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_management.Controllers
{
    [Authorize(Roles = UserConstants.Roles.Doctor)]
    public class DoctorController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IAppointmentService appointmentService;

        private readonly IReferralService referralService;
        public DoctorController(UserManager<ApplicationUser> _userManager,
            IAppointmentService _appointmentService,
            IReferralService _referralService)
        {
            userManager = _userManager;
            appointmentService = _appointmentService;
            referralService = _referralService;
        }

        public async Task<IActionResult> Schedule()
        {
            var doctor = await userManager.FindByNameAsync(User.Identity?.Name);

            var appointments = await appointmentService.GetDoctorAppointments(doctor.Id);

            return View(appointments);
        }

        public async Task<IActionResult> Referral(string id)
        {
            var doctor = await userManager.FindByNameAsync(User.Identity?.Name);

            var model = referralService.GetReferral(id, doctor.Id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReferral(AddReferralViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData[MessageConstant.ErrorMessage] = "Something went wrong.";

                return Redirect("/Doctor/Schedule");
            }

            await referralService.GiveReferralTo(model);

            ViewData[MessageConstant.SuccessMessage] = "Referral given.";
            
            return Redirect("/");
        }

        public async Task<IActionResult> Complete(string id)
        {
            await appointmentService.CompleteAppointment(id);

            ViewData[MessageConstant.SuccessMessage] = "Appointment completed.";

            return Redirect("/Doctor/Schedule");
        }

        public async Task<IActionResult> Remove(string id)
        {
            await appointmentService.RemoveAppointment(id);

            ViewData[MessageConstant.SuccessMessage] = "Appointment removed.";

            return Redirect("/Doctor/Schedule");
        }

        public async Task<IActionResult> Diagnose(string id)
        {
            var model = await appointmentService.GetDiagnosePatient(id);

            return View(model);
        }

        public async Task<IActionResult> SetDiagnose(DiagnosePatientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData[MessageConstant.ErrorMessage] = "Something went wrong.";

                return Redirect("/Doctor/Schedule");
            }

            await appointmentService.SetDiagnose(model);

            ViewData[MessageConstant.SuccessMessage] = "Patient diagnosed.";

            return Redirect("/Doctor/Schedule");
        }
    }
}
