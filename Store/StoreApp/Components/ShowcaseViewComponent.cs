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

        public IViewComponentResult Invoke(string page="default")
        {
             var products = manager.ProductService.GetShowcaseProducts(false);
             return page.Equals("default") 
                    ? View(products)
                    : View("List",products);


        }
    }
}