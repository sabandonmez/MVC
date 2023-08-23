using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class CategorySummaryViewComponent : ViewComponent
    {
        private readonly IServiceManager manager;

        public CategorySummaryViewComponent(IServiceManager manager)
        {
            this.manager = manager;
        }

        public string Invoke()
        {
            return manager.CategoryService.GetAllCategories(false).Count().ToString();
        }
    }
}