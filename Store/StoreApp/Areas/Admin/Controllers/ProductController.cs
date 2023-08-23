using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;
using StoreApp.Models;

namespace Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="admin")]
    public class ProductController : Controller
    {
        private readonly IServiceManager serviceManager;

        public ProductController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        public IActionResult Index([FromQuery]ProductRequestParameters p)
        {
            ViewData["Title"]="Products";
           var products = serviceManager.ProductService.GetAllProductsWithDetails(p);
            var pagination=new Pagination()
            {
                CurrentPage=p.PageNumber,
                ItemPerPage=p.PageSize,
                TotalItems= serviceManager.ProductService.GetAllProducts(false).Count()
            };
            return View(new ProductListViewModel()
            {
                Products=products,
                Pagination=pagination
            });
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
                TempData["success"]=$"{productDtoForInsertion.ProductName} has been created.";
            return RedirectToAction("Index");
            }
            return View();
            
        }
         public IActionResult Update([FromRoute(Name ="id")] int id)
        {
            ViewBag.Categories=GetCategoriesSelectedList();
            var model=serviceManager.ProductService.GetOneProductForUpdate(id,false);
            ViewData["Title"]=model?.ProductName;
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
        TempData["danger"]="Product has been removed.";
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