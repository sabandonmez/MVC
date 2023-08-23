using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="admin")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            TempData["info"] = $"Welcome back!{DateTime.Now.ToShortTimeString()}";
            return View();
        }
    }
}