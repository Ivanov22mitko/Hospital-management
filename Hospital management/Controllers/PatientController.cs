using HM.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_management.Controllers
{
    [Authorize(Roles = UserConstants.Roles.Patient)]
    public class PatientController : Controller
    {
        public IActionResult Appointments()
        {
            return Ok();
        }

        public IActionResult SetAppointment()
        {
            return Ok();
        }

        public IActionResult Referrals()
        {
            return Ok();
        }
    }
}
