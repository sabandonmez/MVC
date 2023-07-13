using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services.Contracts;

namespace StoreApp.Components
{
    public class ProductSummary : ViewComponent
    {
        private readonly IServiceManager manager;

        public ProductSummary(IServiceManager manager)
        {
            this.manager = manager;
        }
        public string Invoke()
        {
            return manager.ProductService.GetAllProducts(false).Count().ToString();
        }
    }
} 