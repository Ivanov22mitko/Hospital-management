using HM.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_management.Controllers
{
    [Authorize(Roles = UserConstants.Roles.Doctor)]
    public class DoctorController : Controller
    {
        public IActionResult Schedule()
        {
            return Ok();
        }

        public IActionResult Referrals()
        {
            return Ok();
        }
    }
}
