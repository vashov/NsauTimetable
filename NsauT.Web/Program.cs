using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NsauT.Web.Tools;
using System.Threading.Tasks;

namespace NsauT.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            await DbPopulator.PopulateDbWithRolesAndAdminAsync(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
