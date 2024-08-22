using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    public class UserRoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
