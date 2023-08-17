using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController:ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public ProductsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(serviceManager.ProductService.GetAllProducts(false));
        }
    }
    

}