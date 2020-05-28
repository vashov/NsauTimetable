using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NsauT.Web.DAL.Models;
using System;
using System.Threading.Tasks;

namespace NsauT.Web.Tools
{
    public static class AdminInitializer
    {
        public async static Task InitializeAsync(UserManager<UserEntity> userManager, IConfigurationSection config)
        {
            string adminName = Environment.GetEnvironmentVariable("TIMETABLE_ADMIN_NAME");
            string adminEmail = Environment.GetEnvironmentVariable("TIMETABLE_ADMIN_EMAIL");
            string password = Environment.GetEnvironmentVariable("TIMETABLE_ADMIN_PASSWORD");

            if (!AllAdminVariablesInitialized(adminName, password, adminEmail))
            {
                adminName = config["Name"];
                adminEmail = config["Email"];
                password = config["Password"];
            }

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

        private static bool AllAdminVariablesInitialized(string name, string password, string email)
            => !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email);
    }                                                    
}                                                        
