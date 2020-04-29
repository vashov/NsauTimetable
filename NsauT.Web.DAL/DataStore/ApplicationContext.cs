using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NsauT.Web.DAL.Models;

namespace NsauT.Web.DAL.DataStore
{
    public class ApplicationContext : IdentityDbContext<UserEntity>
    {
        public DbSet<TimetableEntity> Timetables { get; set; }
        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<SchoolDayEntity> SchoolDays { get; set; }
        public DbSet<PeriodEntity> Periods { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TimetableEntity>().HasAlternateKey(t => t.Key);

            modelBuilder.Entity<TimetableEntity>()
                .HasMany(t => t.Subjects)
                .WithOne(s => s.Timetable)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubjectEntity>()
                .HasMany(s => s.Days)
                .WithOne(d => d.Subject)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SchoolDayEntity>()
                .HasMany(d => d.Periods)
                .WithOne(p => p.SchoolDay)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TimetableEntity>()
                .HasMany(t => t.Groups)
                .WithOne(g => g.Timetable)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
