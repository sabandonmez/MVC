using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class ShowcaseViewComponent : ViewComponent
    {
        private readonly IServiceManager manager;

        public ShowcaseViewComponent(IServiceManager manager)
        {
            this.manager = manager;
        }

        public IViewComponentResult Invoke()
        {
             var products = manager.ProductService.GetShowcaseProducts(false);
             return View(products);
        }
    }
}