using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StoreApp.Infrastructe.TagHelpers
{
    [HtmlTargetElement("td",Attributes ="user-roles")]
    public class UserRoleTagHelper:TagHelper
    {
        private readonly UserManager<IdentityUser> userManager;

        private readonly RoleManager<IdentityRole> roleManager;
        [HtmlAttributeName("user-name")]
        public String? UserName { get; set; }
        public UserRoleTagHelper(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await userManager.FindByNameAsync(UserName);
            TagBuilder ul=new TagBuilder("ul");

            var roles = roleManager.Roles.ToList().Select(n=>n.Name);

            foreach (var role in roles)
            {
                TagBuilder li = new TagBuilder("li");
                li.InnerHtml.Append($"{role} : {await userManager.IsInRoleAsync(user,role)}");
                ul.InnerHtml.AppendHtml(li);
            }
            output.Content.AppendHtml(ul);
        }

    }
}