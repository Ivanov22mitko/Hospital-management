using HM.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_management.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public UserProfileController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }

        //[Authorize(Roles = UserConstants.Roles.Administrator)]
        //public async Task<IActionResult> ManageUsers()
        //{

        //}

        public IActionResult Settings()
        {
            return View();
        }

        //Create role
        public async Task<IActionResult> CreateRole()
        {
            //await roleManager.CreateAsync(new IdentityRole
            //{
            //    Name = "Administrator"
            //});

            return Ok();
        }
    }
}
