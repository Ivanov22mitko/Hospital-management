using HM.Core.Constants;
using HM.Core.Contracts;
using HM.Core.Models;
using HM.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Hospital_management.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService service;

        private readonly IPeopleService peopleService;

        public UserController(
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IUserService _service,
            IPeopleService _peopleService)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            service = _service;
            peopleService = _peopleService;
        }

        public async Task<IActionResult> Roles(string id)
        {
            var user = await service.GetUserById(id);
            var model = new UserRolesViewModel()
            {
                UserId = user.Id,
                Name = $"{user.FirstName} {user.LastName}"
            };

            ViewBag.RoleItems = roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = userManager.IsInRoleAsync(user, r.Name).Result
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Roles(UserRolesViewModel model)
        {
            var user = await service.GetUserById(model.UserId);
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);

            if (model.RoleNames?.Length > 0)
            {
                await userManager.AddToRolesAsync(user, model.RoleNames);
            }

            ViewData[MessageConstant.SuccessMessage] = "User roles updated.";

            return RedirectToAction(nameof(ManageUsers));
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await service.GetUsers();

            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await service.UserEdit(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserEditViewModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                ViewData[MessageConstant.ErrorMessage] = "Something went wrong.";

                return View(model);
            }

            if (await service.UpdateUser(model))
            {
                if (model.Role == "Doctor")
                {
                    await peopleService.UpdateDoctor(model);
                }
                else
                {
                    await peopleService.UpdatePatient(model);
                }

                ViewData[MessageConstant.SuccessMessage] = "User updated.";

                return Redirect("/Admin/User/ManageUsers");
            }

            return View(model);
        }
        
        public async Task<IActionResult> Save(string id)
        {
            var userData = await service.GetUserById(id);

            var fileName = $"{userData.FirstName}_{userData.LastName}.json";

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(userData));

            var content = new MemoryStream(bytes);

            return File(content, "application/json", fileName);
        }

        //Create role
        public async Task<IActionResult> CreateRole()
        {
            //await roleManager.CreateAsync(new IdentityRole
            //{
            //    Name = "Patient"
            //});

            return Ok();
        }
    }
}
