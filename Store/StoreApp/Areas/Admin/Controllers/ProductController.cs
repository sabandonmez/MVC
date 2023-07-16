using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;

namespace Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IServiceManager serviceManager;

        public ProductController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        public IActionResult Index()
        {
            var model= serviceManager.ProductService.GetAllProducts(false);
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Categories=
            new SelectList(serviceManager.CategoryService.GetAllCategories(false),"CategoryId","CategoryName","1");
            
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] ProductDtoForInsertion productDtoForInsertion)
        {
            if (ModelState.IsValid)
            {
                serviceManager.ProductService.CreateProduct(productDtoForInsertion);
            return RedirectToAction("Index");
            }
            return View();
            
        }
         public IActionResult Update([FromRoute(Name ="id")] int id)
        {
            var model=serviceManager.ProductService.GetOneProduct(id,false);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                serviceManager.ProductService.UpdateOneProduct(product);
                return RedirectToAction("Index");
            }
            return View();
        }
       public IActionResult Delete([FromRoute(Name ="id")] int id)
       {
        serviceManager.ProductService.DeleteOneProduct(id);     
        return RedirectToAction("Index");
       }
       [HttpPost]
       [ValidateAntiForgeryToken]
       public IActionResult Delete(Product product)
       {
        return View();
       }
}
}