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
            ViewBag.Categories= GetCategoriesSelectedList();
            return View();
        }

        private SelectList GetCategoriesSelectedList()
        {
            return new SelectList(serviceManager.CategoryService.GetAllCategories(false),"CategoryId","CategoryName","1");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDtoForInsertion,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images",file.FileName);
                
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDtoForInsertion.ImageUrl=String.Concat("/images/",file.FileName);
                serviceManager.ProductService.CreateProduct(productDtoForInsertion);
            return RedirectToAction("Index");
            }
            return View();
            
        }
         public IActionResult Update([FromRoute(Name ="id")] int id)
        {
            ViewBag.Categories=GetCategoriesSelectedList();
            var model=serviceManager.ProductService.GetOneProductForUpdate(id,false);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Update(ProductDtoForUpdate productDto,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                 string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images",file.FileName);
                
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl=String.Concat("/images/",file.FileName);
                serviceManager.ProductService.UpdateOneProduct(productDto);
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