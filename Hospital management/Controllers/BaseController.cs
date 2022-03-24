using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_management.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}
