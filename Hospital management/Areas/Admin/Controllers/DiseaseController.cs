using HM.Core.Contracts;
using HM.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_management.Areas.Admin.Controllers
{
    public class DiseaseController : BaseController
    {
        private readonly IDiseaseService service;

        public DiseaseController(IDiseaseService _service)
        {
            service = _service;
        }

        public async Task<IActionResult> Index()
        {
            var diseases = await service.GetDiseases();

            return View(diseases);
        }

        public IActionResult AddDisease(AddDiseaseViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDiseaseViewModel model)
        {
            await service.AddDiseaseToDb(model);

            return Redirect("/Admin/Disease/");
        }

        public async Task<IActionResult> Remove(string id)
        {
            await service.RemoveDiseaseFromDb(id);

            return Redirect("/Admin/Disease/");
        }
    }
}
