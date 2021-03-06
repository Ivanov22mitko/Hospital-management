using HM.Core.Constants;
using HM.Core.Contracts;
using HM.Core.Models.Laboratory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital_management.Areas.Admin.Controllers
{
    public class LaboratoryController : BaseController
    {
        private readonly ILaboratoryService service;

        private readonly IPeopleService peopleService;

        public LaboratoryController(ILaboratoryService _service,
            IPeopleService _peopleService)
        {
            service = _service;
            peopleService = _peopleService;
        }

        public async Task<IActionResult> Index()
        {
            var laboratories = await service.GetLaboratories();

            return View(laboratories);
        }

        public IActionResult AddLaboratory(AddLaboratoryViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLaboratoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData[MessageConstant.ErrorMessage] = "Something went wrong.";

                return View(model);
            }

            await service.AddLaboratoryToDb(model);

            ViewData[MessageConstant.SuccessMessage] = "Laboratory created.";

            return Redirect("/Admin/Laboratory/");
        }

        public async Task<IActionResult> Manage(string id)
        {
            var model = await service.ManageLaboratory(id);

            var operators = await peopleService.GetDoctors();

            ViewBag.Operators = operators
                .Where(o => o.Specialization == model.Name)
                .Select(o => new SelectListItem()
                {
                    Text = $"{o.FirstName} {o.LastName}",
                    Value = o.Id,
                    Selected = o.LaboratoryId == model.Id
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(string id, LaboratoryManageViewModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                ViewData[MessageConstant.ErrorMessage] = "Something went wrong.";

                return View(model);
            }

            if (await service.UpdateLaboratory(model))
            {
                ViewData[MessageConstant.SuccessMessage] = "Laboratory updated.";

                return Redirect("/Admin/Laboratory");
            }

            ViewData[MessageConstant.ErrorMessage] = "Something went wrong.";

            return RedirectToAction(nameof(Manage));
        }
    }
}
