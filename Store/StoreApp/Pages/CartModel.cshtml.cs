using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;
using StoreApp.Infrastructe.Extensions;

namespace StoreApp.Pages
{
    public class CartModel : PageModel
    {
        private readonly IServiceManager serviceManager;
        public Cart Cart { get; set; }
        
        public string ReturnUrl { get; set; }="/";

        public CartModel(IServiceManager serviceManager, Cart cartService)
        {
            this.serviceManager = serviceManager;
            this.Cart=cartService;
            
        }

        public void OnGet(string returnUrl)
        {
            ReturnUrl=returnUrl ?? "/";
            //Cart=HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }
        public IActionResult OnPost(int productId , string returnUrl)
        {
            Product? product=serviceManager.ProductService.GetOneProduct(productId,false);
            if (product is not null)
            {
               // Cart=HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product,1);
               // HttpContext.Session.SetJson<Cart>("cart" , Cart);
            }
            return RedirectToPage(new { returnUrl = returnUrl});
        }

        public IActionResult OnPostRemove(int productId , string returnUrl)
        {
           // Cart=HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.RemoveLine(Cart.Lines.First(cl=>cl.Product.ProductId.Equals(productId)).Product);
           // HttpContext.Session.SetJson<Cart>("cart",Cart);
            return Page();
        }
    }
}