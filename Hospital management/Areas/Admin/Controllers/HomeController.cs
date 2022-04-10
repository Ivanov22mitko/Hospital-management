using Microsoft.AspNetCore.Mvc;

namespace Hospital_management.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
