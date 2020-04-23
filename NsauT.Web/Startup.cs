using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NsauT.Web.DAL.DataStore;

namespace NsauT.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //services.AddControllers();

            string connectionString = Configuration.GetConnectionString("TimetableDatabase");
            services.AddDbContext<TimetableContext>(option => option.UseNpgsql(connectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "manage_area",
                    areaName: "manage",
                    pattern: "manage/{controller=timetable}/{action=timetables}/{id?}"
                    );

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=approver}/{action=timetables}/{id?}"
                //    );
                //endpoints.MapControllers();
            });
        }
    }
}