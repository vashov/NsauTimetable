using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NsauT.Web.BLL.Infrastructure;
using NsauT.Web.BLL.Services.Period;
using NsauT.Web.BLL.Services.SchoolDay;
using NsauT.Web.BLL.Services.Subject;
using NsauT.Web.BLL.Services.Timetable;
using NsauT.Web.BLL.Services.User;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;

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

            services.AddTransient<ITimetableService, TimetableService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<ISchoolDayService, SchoolDayService>();
            services.AddTransient<IPeriodService, PeriodService>();
            services.AddTransient<IApproverFacade, ApproverFacade>();
            services.AddTransient<IUserService, UserService>();

            string connectionString = Configuration.GetConnectionString("TimetableDatabase");
            services.AddDbContext<ApplicationContext>(option => option.UseNpgsql(connectionString));

            services.AddAutoMapper(typeof(Startup));

            services.AddIdentity<UserEntity, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = new Microsoft.AspNetCore.Http.PathString("/account/login");
                });
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

            app.UseAuthentication();    // аутентификация
            app.UseAuthorization();     // авторизация

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "manage_area",
                    areaName: "manage",
                    pattern: "manage/{controller=timetable}/{action=timetables}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}/{id?}"
                    );
                //endpoints.MapControllers();
            });
        }
    }
}
