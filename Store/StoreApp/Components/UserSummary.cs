using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class UserSummaryViewComponent : ViewComponent
    {
        private readonly IServiceManager manager;

        public UserSummaryViewComponent(IServiceManager manager)
        {
            this.manager = manager;
        }

        public string Invoke()
        {
            return manager.AuthService.GetAllUser().Count().ToString();
        }
    }
}