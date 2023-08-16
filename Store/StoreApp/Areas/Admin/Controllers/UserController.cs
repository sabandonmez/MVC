using Entities.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using SQLitePCL;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IServiceManager serviceManager;

        public UserController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        public IActionResult Index()
        {
            var users = serviceManager.AuthService.GetAllUser();
            return View(users);
        }
        public IActionResult Create()
        {
            return View(new UserDtoForCreation()
            {
                Roles = new HashSet<string>(serviceManager.AuthService.Roles.Select(n => n.Name).ToList())
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserDtoForCreation userDto)
        {
            var result= await serviceManager.AuthService.CreateUser(userDto);
            return result.Succeeded
                ? RedirectToAction("Index")
                : View();
        }

        public async Task<IActionResult> Update([FromRoute(Name ="id")] string id)
        {
            var result = await serviceManager.AuthService.GetUserForUpdate(id);
            return View(result);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] UserDtoForUpdate userDto)
        {
            if (ModelState.IsValid)
            {
                await serviceManager.AuthService.Update(userDto);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}