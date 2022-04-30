using HM.Core.Constants;
using HM.Core.Contracts;
using HM.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_management.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Settings()
        {
            return View();
        }
    }
}
