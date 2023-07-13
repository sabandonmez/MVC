using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class CategoriesMenuViewComponent :ViewComponent
    {
        private readonly IServiceManager manager;

        public CategoriesMenuViewComponent(IServiceManager manager)
        {
            this.manager = manager;
        }

        public IViewComponentResult Invoke()
        {
            var categories= manager.CategoryService.GetAllCategories(false);
            return View(categories);
        }
    }
}