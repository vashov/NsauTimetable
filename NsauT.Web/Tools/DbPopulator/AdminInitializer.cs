using Microsoft.AspNetCore.Identity;
using NsauT.Web.DAL.Models;
using System.Threading.Tasks;

namespace NsauT.Web.Tools
{
    public static class AdminInitializer
    {
        public async static Task InitializeAsync(UserManager<UserEntity> userManager)
        {
            string adminName = "admin";
            string adminEmail = "vashovway@gmail.com";
            string password = "ADMIN_010203_admin";

            if (await userManager.FindByNameAsync(adminName) == null)
            {
                UserEntity admin = new UserEntity 
                { 
                    Email = adminEmail, 
                    UserName = adminName 
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
