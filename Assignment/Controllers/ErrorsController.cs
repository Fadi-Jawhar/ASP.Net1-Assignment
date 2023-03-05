using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
