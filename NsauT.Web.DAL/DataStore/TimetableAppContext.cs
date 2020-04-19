using Microsoft.EntityFrameworkCore;
using NsauT.Web.DAL.Models;

namespace NsauT.Web.DAL.DataStore
{
    public class TimetableAppContext : DbContext
    {
        public DbSet<TimetableEntity> Timetables { get; set; }

        public TimetableAppContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=password");
        //}
    }
}
