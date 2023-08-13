using Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.Contracts;

namespace StoreApp.Infrastructe.TagHelpers
{
    [HtmlTargetElement("div", Attributes="products")]
    public class LastestProductTagHelper:TagHelper
    {
        private readonly IServiceManager serviceManager;

        public LastestProductTagHelper(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("class" , "my-3");

            TagBuilder h6 = new TagBuilder("h6");
            h6.Attributes.Add("class" , "lead");

            TagBuilder icon = new TagBuilder("i");
            icon.Attributes.Add("class" , "fa fa-box text-secondary");

            h6.InnerHtml.AppendHtml(icon);
            
            h6.InnerHtml.AppendHtml(" Lastest Products");

            TagBuilder ul = new TagBuilder("ul");
            var products=serviceManager.ProductService.GetLastestProducts(5,false);
            foreach (Product item in products)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.Attributes.Add("href",$"/product/get/{item.ProductId}");
                a.InnerHtml.AppendHtml(item.ProductName);
                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);

            }
            div.InnerHtml.AppendHtml(h6);
            div.InnerHtml.AppendHtml(ul);
            output.Content.AppendHtml(div);
        }
    }
}