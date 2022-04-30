using HM.Core.Constants;
using HM.Core.Contracts;
using HM.Core.Models.Appointment;
using HM.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital_management.Controllers
{
    [Authorize(Roles = UserConstants.Roles.Patient)]
    public class PatientController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IAppointmentService appointmentService;

        private readonly IReferralService referralService;

        public PatientController(UserManager<ApplicationUser> _userManager,
            IAppointmentService _appointmentService,
            IReferralService _referralService)
        {
            userManager = _userManager;
            appointmentService = _appointmentService;
            referralService = _referralService;
        }

        public async Task<IActionResult> SetAppointment()
        {
            var user = await userManager.FindByNameAsync(User.Identity?.Name);

            var model = appointmentService.SetAppointment(user.Id);

            var doctors = await appointmentService.GetDoctorsGP();

            ViewBag.Doctors = doctors.Select(d => new SelectListItem()
            {
                Text = $"{d.FirstName} {d.LastName}",
                Value = d.Id
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment(AddAppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData[MessageConstant.ErrorMessage] = "Something went wrong.";

                return Redirect("/Patient/SetAppointment"); ;
            }

            await appointmentService.AddAppointmentToDb(model);

            ViewData[MessageConstant.SuccessMessage] = "Appointment added.";

            return Redirect("/");
        }

        public async Task<IActionResult> Referrals()
        {
            var user = await userManager.FindByNameAsync(User.Identity?.Name);

            var model = await referralService.GetReferrals(user.Id);

            return View(model);
        }

        public async Task<IActionResult> Appointments()
        {
            var user = await userManager.FindByNameAsync(User.Identity?.Name);

            var model = await appointmentService.GetPatientAppointments(user.Id);

            return View(model);
        }
    }
}
