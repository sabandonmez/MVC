using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="admin")]
    public class OrderController:Controller
    {
        private readonly IServiceManager serviceManager;

        public OrderController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        public IActionResult Index()
        {
            var orders= serviceManager.OrderService.Orders;
            return View(orders);
        }
        [HttpPost]
        public IActionResult Complete([FromForm] int id)
        {
            serviceManager.OrderService.Complete(id);
            return RedirectToAction("Index");
        }
    }
}