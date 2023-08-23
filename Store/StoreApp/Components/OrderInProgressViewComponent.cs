using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class OrderInProgressViewComponent : ViewComponent
    {
        private readonly IServiceManager manager;

        public OrderInProgressViewComponent(IServiceManager manager)
        {
            this.manager = manager;
        }

        public string Invoke()
        {
            return manager.OrderService.NumberOfInProcess.ToString();
        }
    }
}