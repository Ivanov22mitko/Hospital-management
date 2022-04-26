using HM.Core.Contracts;
using HM.Core.Models.Laboratory;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_management.Areas.Admin.Controllers
{
    public class LaboratoryController : BaseController
    {
        private readonly ILaboratoryService service;

        private readonly IDoctorService doctorService;

        public LaboratoryController(ILaboratoryService _service,
            IDoctorService _doctorService)
        {
            service = _service;
            doctorService = _doctorService;
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
            await service.AddLaboratoryToDb(model);

            return Redirect("/Admin/Laboratory/");
        }

        public async Task<IActionResult> Manage(string id)
        {
            var model = await service.ManageLaboratory(id);

            var operators = await doctorService.GetDoctors();

            ViewBag.Operators = operators
                .Select(o => o.Specialization == model.Name)
                .ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(string id, LaboratoryManageViewModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return View(model);
            }

            if (await service.UpdateLaboratory(model))
            {
                return Redirect("/Admin/Laboratory");
            }

            return RedirectToAction(nameof(Manage));
        }
    }
}
