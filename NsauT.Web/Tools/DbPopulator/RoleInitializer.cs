using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace NsauT.Web.Tools
{
    public static class RoleInitializer
    {
        public async static Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("manager") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("manager"));
            }
        }
    }
}
