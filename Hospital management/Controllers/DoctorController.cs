using HM.Core.Constants;
using HM.Core.Contracts;
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

            var model = new AddReferralViewModel()
            {
                Id = Guid.NewGuid().ToString(),
                DoctorId = doctor.Id,
                PatientId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReferral(AddReferralViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Doctor/Schedule");
            }

            await referralService.GiveReferralTo(model);
            
            return Redirect("/");
        }

        public async Task<IActionResult> Complete(string id)
        {
            await appointmentService.CompleteAppointment(id);

            return Redirect("/Doctor/Schedule");
        }

        public async Task<IActionResult> Remove(string id)
        {
            await appointmentService.RemoveAppointment(id);

            return Redirect("/Doctor/Schedule");
        }
    }
}
