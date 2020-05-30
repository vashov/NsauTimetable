using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NsauT.Web.DAL.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NsauT.Web.Tools
{
    public static class DbPopulator
    {
        public async static Task PopulateDbWithRolesAndAdminAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    IConfiguration configuration = services.GetRequiredService<IConfiguration>();

                    bool needRoleInitialize = configuration.GetValue<bool>("NeedRoleInitialize");
                    if (needRoleInitialize)
                    {
                        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                        await RoleInitializer.InitializeAsync(rolesManager);
                    }

                    bool needAdminInitialize = configuration.GetValue<bool>("NeedAdminInitialize");
                    if (needAdminInitialize)
                    {
                        var userManager = services.GetRequiredService<UserManager<UserEntity>>();
                        IConfigurationSection debugAdminConfig = configuration.GetSection("DebugAdmin");
                        await AdminInitializer.InitializeAsync(userManager, debugAdminConfig);
                    }
                }
                catch (Exception)
                {
                    Debug.WriteLine("An error occurred while seeding the database.");
                }
            }
        }
    }
}
